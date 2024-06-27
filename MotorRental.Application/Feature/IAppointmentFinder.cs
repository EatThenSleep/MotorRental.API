using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public interface IAppointmentFinder
    {
        Task<IEnumerable<Appointment>> GetAllApointment(string userId,
                                                        string role,
                                                        AppointmentFindCreterias creterias,
                                                        AppointmentSortBy sortBy);
        Task<object> GetSpecificAppointment(Guid appointmentId);
    }
}
