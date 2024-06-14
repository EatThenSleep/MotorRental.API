using MotorRental.Entities;
using MotorRental.UseCase.UnitOfWork;
using MotorRental.Utilities;
using MotorRental.UseCase.Helper;

namespace MotorRental.UseCase
{
    public class AppointmentStateManager : IAppointmentStateManager
    {
        private readonly IAppointmentUnitOfWork _appointmentUnitOfWork;

        public AppointmentStateManager(IAppointmentUnitOfWork appointmentUnitOfWork)
        {
            _appointmentUnitOfWork = appointmentUnitOfWork;
        }

        public async Task<TransactionResult> CreateAppoitment(Appointment appointment)
        {
            try
            {
                // begin transaction
                await _appointmentUnitOfWork.BeginTransaction();

                //check information user is valid
                var existingUser = await _appointmentUnitOfWork.UserRepository.GetById(appointment.CustomerId);
                if (!ValidationMotorbike.CheckIformationInvalid(existingUser))
                {
                    return TransactionResult.InforUserInvalid;
                }

                // check motorbike is free
                var existingMobike = await _appointmentUnitOfWork
                    .MotorRepository
                    .GetByIdAndUserId(appointment.MotorbikeId, appointment.OwnerId);

                if (!ValidationMotorbike.CheckMotorbikeFree(existingMobike))
                {
                    return TransactionResult.MotorbikeCanNotUseNow;
                }

                // create appoinment
                appointment.StatusAppointment = SD.Status_Payment_Not;
                appointment.StatusAppointment = SD.Status_Appointment_Process;
                appointment.CreatedAt = DateTime.Now;
                var res = await _appointmentUnitOfWork.AppointmentRepository
                                                .CreateAppoinment(appointment);

                // update status motorbike
                existingMobike.status = SD.Status_Busy;
                var motorUpdate = _appointmentUnitOfWork.MotorRepository
                                                .UpdateNotSave(existingMobike);

                if (motorUpdate.status != SD.Status_Busy)
                {
                    await _appointmentUnitOfWork.Cancel();
                    throw new Exception(message: "Error happening, Please try again");
                }
                else
                {
                    await _appointmentUnitOfWork.SaveChanges();
                }

                return TransactionResult.Success;
            }
            catch (Exception ex)
            {
                return new TransactionResult() { isSucess = false, ErrorMessage = ex.Message };
            }
        }

        public async Task<TransactionResult> Accept(Guid appointmentId, string userId)
        {
            // get appointment of owner
            var appointment = await _appointmentUnitOfWork
                                        .AppointmentRepository
                                        .GetById(appointmentId, userId);

            if (ValidationAppointment.checkAppointmentIsProcess(appointment) != TransactionResult.Success)
            {
                return ValidationAppointment.checkAppointmentIsProcess(appointment);
            }

            // update appointment to accepted
            var res = _appointmentUnitOfWork.AppointmentRepository
                                .UpdateAppointmentStatus(appointment, SD.Status_Appointment_Accepted);

            return TransactionResult.Success;

        }

        public async Task<TransactionResult> Reject(Guid appointmentId, string userId)
        {
            await _appointmentUnitOfWork.BeginTransaction();

            // get appointment of owner
            var appointment = await _appointmentUnitOfWork
                                        .AppointmentRepository
                                        .GetById(appointmentId, userId);

           if(ValidationAppointment.checkAppointmentIsProcess(appointment) != TransactionResult.Success)
           {
                return ValidationAppointment.checkAppointmentIsProcess(appointment);
           }

            // update appointment to reject
            var res = _appointmentUnitOfWork.AppointmentRepository
                                .UpdateAppointmentStatus(appointment, SD.Status_Appointment_Cancel, notSave: true);

            // var existing motorbike
            var existingMobike = await _appointmentUnitOfWork
                    .MotorRepository
                    .GetByIdAndUserId(appointment.MotorbikeId, userId);

            // update status motorbike
            existingMobike.status = SD.Status_Enable;
            var motorUpdate = _appointmentUnitOfWork.MotorRepository
                                            .UpdateNotSave(existingMobike);

            if (motorUpdate.status != SD.Status_Enable)
            {
                await _appointmentUnitOfWork.Cancel();
                throw new Exception(message: "Error happening, Please try again");
            }
            else
            {
                await _appointmentUnitOfWork.SaveChanges();
            }

            return TransactionResult.Success;
        }

        public Task<TransactionResult> ReturnWithoutPayment(Guid appointmentId)
        {
            throw new NotImplementedException();
        }
    }
}
