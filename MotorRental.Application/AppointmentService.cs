using MotorRental.Application.IRepository;
using MotorRental.Entities;
using MotorRental.UseCase.IRepository;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public class AppointmentService : IAppointmentService
    {
        public IUserRepository _useRepository { get; }
        public IMotorRepository _motorRepository { get; }
        public IAppointmentRepository _appointmentRepository { get; }

        public AppointmentService(IMotorRepository motorRepository,
                                   IUserRepository useRepository,
                                   IAppointmentRepository appointmentRepository)
        {
            _motorRepository = motorRepository;
            _useRepository = useRepository;
            _appointmentRepository = appointmentRepository;
        }

        public TransactionResult CreateAppoitment(Appointment appointment)
        {
            //check information user is valid
           var existingUser = _useRepository.GetByIdNoAsync(appointment.CustomerId);
            if (!CheckIformationInvalid(existingUser))
            {
                return TransactionResult.InforUserInvalid;
            }

            // check motorbike is free
            var existingMobike = _motorRepository.GetStatus(appointment.MotorbikeId, appointment.OwnerId);
            if (!CheckMotorbikeFree(existingMobike))
            {
                return TransactionResult.MotorbikeCanNotUseNow;
            }

            // create appoinment
            appointment.StatusAppointment = SD.Status_Payment_Not;
            appointment.StatusAppointment = SD.Status_Appointment_Process;
            var res = _appointmentRepository.CreateAppoinment(appointment);

            return TransactionResult.Success;
        }

        private bool CheckMotorbikeFree(int existingMobike)
        {
            return existingMobike == SD.Status_Enable;
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
