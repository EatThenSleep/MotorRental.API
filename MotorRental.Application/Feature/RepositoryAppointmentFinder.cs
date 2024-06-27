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
            var creteriasProcessed = Helper.ValidationAppointment.ProcessCreterias(creterias);

            // get appointment
            var res = await _appointmentRepository.GetAllAsync(userId, role, creteriasProcessed, sortBy);

            foreach( var item in res )
            {
                item.Surcharges = (ICollection<Surcharge>?)await _appointmentRepository.getSurcharges(item.Id);
            }

            // get surcharge

            return res;
        }

        public async Task<object> GetSpecificAppointment(Guid appointmentId)
        {
            var res = await _appointmentRepository.GetInformation(appointmentId);

            return res;
        }
    }
}
