using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public interface IAppointmentService
    {
        Task<TransactionResult> CreateAppoitment(Appointment appointment);
        Task<IEnumerable<Appointment>> GetAllApointment(string userId,
                                                        string role,
                                                        AppointmentFindCreterias creterias,
                                                        AppointmentSortBy sortBy);
    }
}
