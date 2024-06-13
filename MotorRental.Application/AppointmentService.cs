using MotorRental.UseCase.Repository;
using MotorRental.Entities;
using MotorRental.UseCase.UnitOfWork;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MotorRental.UseCase
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentUnitOfWork _appointmentUnitOfWork;

        public AppointmentService(IAppointmentUnitOfWork appointmentUnitOfWork)
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
                if (!CheckIformationInvalid(existingUser))
                {
                    return TransactionResult.InforUserInvalid;
                }

                // check motorbike is free
                var existingMobike = await _appointmentUnitOfWork
                    .MotorRepository
                    .GetByIdAndUserId(appointment.MotorbikeId, appointment.OwnerId);

                if (!CheckMotorbikeFree(existingMobike))
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
                var motorUpdate =  _appointmentUnitOfWork.MotorRepository
                                                .UpdateNotSave(existingMobike);

                if(motorUpdate.status != SD.Status_Busy)
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
            catch(Exception ex) {
                return new TransactionResult() { isSucess = false, ErrorMessage = ex.Message};
            }
        }

        public async Task<IEnumerable<Appointment>> GetAllApointment(string userId, string role, AppointmentFindCreterias creterias, AppointmentSortBy sortBy)
        {
            var creteriasProcessed = ProcessCreterias(creterias);

            // get appointment
            var res = await _appointmentUnitOfWork.AppointmentRepository.GetAllAsync(userId, role, creteriasProcessed, sortBy);

            // get surcharge

            return res;
        }

        private static AppointmentFindCreterias ProcessCreterias(AppointmentFindCreterias creterias)
        {
            if(creterias.FilterStatusPayment == -1 &&
                creterias.FilterStatusAppointment == -1 &&
                creterias.Skip == 0 &&
                creterias.Take == int.MaxValue)
            {
                return AppointmentFindCreterias.Empty;
            }

            if (creterias.FilterStatusAppointment != SD.Status_Appointment_Process &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Accepted &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Cancel &&
                 creterias.FilterStatusAppointment != SD.Status_Appointment_Done)
            {
                creterias.FilterStatusAppointment = -1;
            }

            if (creterias.FilterStatusPayment != SD.Status_Payment_Not &&
                creterias.FilterStatusPayment != SD.Status_Payment_Payed)
            {
                creterias.FilterStatusPayment = -1;
            }

            if(creterias.FilterStatusPayment == -1 && creterias.FilterStatusAppointment == -1)
            {
                return AppointmentFindCreterias.Empty;
            }

             return creterias;
        }

        private bool CheckMotorbikeFree(Motorbike existingMobike)
        {
            if(existingMobike == null) return false;
            return existingMobike.status == SD.Status_Enable;
        }

        private bool CheckIformationInvalid(User existingUser)
        {
            if(existingUser == null) return false;
            if (string.IsNullOrEmpty(existingUser.Name))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.Email))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.PhoneNumber))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityNumber))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityImagePre))
            {
                return false;
            }
            if (string.IsNullOrEmpty(existingUser.IdentityImageBack))
            {
                return false;
            }
            return true;
        }

        
    }
}
