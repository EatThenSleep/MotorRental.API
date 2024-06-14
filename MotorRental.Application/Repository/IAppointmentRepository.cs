using MotorRental.Entities;
using MotorRental.UseCase.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Repository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppoinment(Appointment appointment);

        Task<IEnumerable<Appointment>> GetAllAsync(string userId, string role, AppointmentFindCreterias creterias, AppointmentSortBy sortBy);
        Task<Appointment> GetById(Guid appointmentId, string userId);
        Task<Appointment> UpdateAppointmentStatus(Appointment appointment, int statusAppointment);
    }
}
