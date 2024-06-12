using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.IRepository
{
    public interface IAppointmentRepository
    {
        Task<Appointment> CreateAppoinment(Appointment appointment);

        Task<IEnumerable<Appointment>> GetAllAsync(string userId, string role, AppointmentFindCreterias creterias, AppointmentSortBy sortBy);
    }
}
