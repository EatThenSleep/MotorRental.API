using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.UseCase.IRepository;
using MotorRental.Utilities;
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

        public async Task<Appointment> CreateAppoinment(Appointment appointment)
        {
            await _db.AddAsync(appointment);
            return appointment;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync(string userId, string role)
        {
            var query = _db.Appointments.AsQueryable();

            if(role == SD.OWNER)
            {
                query = from a in _db.Appointments
                            join b in _db.Users on a.CustomerId equals b.Id
                            join c in _db.Motorbikes on a.MotorbikeId equals c.Id
                            select new Appointment()
                            {
                                Id = a.Id,
                                RentalBegin = a.RentalBegin,
                                RentalEnd = a.RentalEnd,
                                RentalReturn = a.RentalReturn,
                                LocationReceive = a.LocationReceive,
                                StatusAppointment = a.StatusAppointment,
                                StatusPayment = a.StatusPayment,
                                RentalPrice = a.RentalPrice,
                                OwnerId = a.OwnerId,
                                MotorbikeId = a.MotorbikeId,
                                Customer = new User() { Id = b.Id, Name = b.Name, PhoneNumber = b.PhoneNumber},
                                Motorbike = new Motorbike() { Id = c.Id, Name = c.Name, LicensePlate = c.LicensePlate },
                            };

                query = query.Where(u => u.OwnerId == userId);
            }
            else
            {
                query = from a in _db.Appointments
                        join b in _db.Users on a.OwnerId equals b.Id
                        join c in _db.Motorbikes on a.MotorbikeId equals c.Id
                        select new Appointment()
                        {
                            Id = a.Id,
                            RentalBegin = a.RentalBegin,
                            RentalEnd = a.RentalEnd,
                            RentalReturn = a.RentalReturn,
                            LocationReceive = a.LocationReceive,
                            StatusAppointment = a.StatusAppointment,
                            StatusPayment = a.StatusPayment,
                            RentalPrice = a.RentalPrice,
                            CustomerId = a.CustomerId,
                            MotorbikeId = a.MotorbikeId,
                            Owner = new User() { Id = b.Id, Name = b.Name, PhoneNumber = b.PhoneNumber },
                            Motorbike = new Motorbike() { Id = c.Id, Name = c.Name, LicensePlate = c.LicensePlate },
                        };

                query = query.Where(u => u.CustomerId == userId);
            }

            


            return await query.ToListAsync();
        }
    }
}
