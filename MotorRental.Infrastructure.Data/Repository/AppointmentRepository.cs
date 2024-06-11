using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.UseCase.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Infrastructure.SqlServer.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _db;

        public AppointmentRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public Appointment CreateAppoinment(Appointment appointment)
        {
            _db.Add(appointment);
            _db.SaveChanges();
            return appointment;
        }
    }
}
