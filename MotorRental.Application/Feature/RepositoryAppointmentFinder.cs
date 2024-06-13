using MotorRental.Entities;
using MotorRental.UseCase.Repository;
using MotorRental.UseCase.UnitOfWork;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public class RepositoryAppointmentFinder : IAppointmentFinder
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public RepositoryAppointmentFinder(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public async Task<IEnumerable<Appointment>> GetAllApointment(string userId,
                                                        string role,
                                                        AppointmentFindCreterias creterias,
                                                        AppointmentSortBy sortBy
        )
        {
            var creteriasProcessed = ProcessCreterias(creterias);

            // get appointment
            var res = await _appointmentRepository.GetAllAsync(userId, role, creteriasProcessed, sortBy);

            // get surcharge

            return res;
        }

        private static AppointmentFindCreterias ProcessCreterias(AppointmentFindCreterias creterias)
        {
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

            if(creterias.Skip < 0)
            {
                creterias.Skip = 0;
            }
            if(creterias.Take < 0 && creterias.Take > int.MaxValue)
            {
                creterias.Take = int.MaxValue;
            }

            return creterias;
        }

    }
}
