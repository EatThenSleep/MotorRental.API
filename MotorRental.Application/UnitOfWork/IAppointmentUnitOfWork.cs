using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.UnitOfWork
{
    public interface IAppointmentUnitOfWork
    {
        IUserRepository UserRepository { get; }
        IMotorRepository MotorRepository { get; }
        IAppointmentRepository AppointmentRepository { get; }

        Task BeginTransaction();
        Task SaveChanges();
        Task Cancel();
    }
}
