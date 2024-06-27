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
        Task<Appointment?> GetByIdInclude(Guid appointmentId, string userId, string role);

        Task<object> GetInformation(Guid appointmentId);

        Task<Appointment> UpdateAppointmentStatus(Appointment appointment,
                                                    int statusAppointment,
                                                    bool notSave = false);
        Task<Appointment> UpdateNotPay(Appointment appointment, Surcharge[] surcharges);
        Task<Appointment> UpdatePayed(Appointment appointment);
        Task<Appointment> addSessionId(Guid id, string sessionId);

        Task<IEnumerable<Surcharge>> getSurcharges(Guid appointmentId);
    }
}
