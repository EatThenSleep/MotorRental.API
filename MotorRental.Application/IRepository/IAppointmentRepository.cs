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
        Appointment CreateAppoinment(Appointment appointment);
    }
}
