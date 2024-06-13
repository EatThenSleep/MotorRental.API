using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.Data.Repository;
using MotorRental.UseCase.Repository;
using MotorRental.UseCase.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Infrastructure.SqlServer.Repository
{
    public class AppointmentUnitOfWork : IAppointmentUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public AppointmentUnitOfWork(ApplicationDbContext db)
        {
            _db = db;

            UserRepository = new UserRepository(_db);
            MotorRepository = new MotorRepository(_db);
            AppointmentRepository = new AppointmentRepository(_db);
        }

        public IUserRepository UserRepository { get; }

        public IMotorRepository MotorRepository { get; }

        public IAppointmentRepository AppointmentRepository { get; }

        public Task BeginTransaction()
        {
            return Task.CompletedTask;
        }

        public Task Cancel()
        {
            return Task.CompletedTask;
        }

        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
