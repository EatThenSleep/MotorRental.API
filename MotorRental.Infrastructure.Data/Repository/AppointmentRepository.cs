﻿using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.MotorRental.UseCase;
using MotorRental.UseCase;
using MotorRental.UseCase.Repository;
using MotorRental.Utilities;


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

        public async Task<IEnumerable<Appointment>> GetAllAsync(string userId,
                                        string role,
                                        AppointmentFindCreterias creterias,
                                        AppointmentSortBy sortBy)
        {
            var query = _db.Appointments.AsQueryable();


            if (role == SD.OWNER)
            {
                query = from a in _db.Appointments
                        join b in _db.Users on a.CustomerId equals b.Id
                        join c in _db.Motorbikes on a.MotorbikeId equals c.Id
                        select new Appointment()
                        {
                            Id = a.Id,
                            CreatedAt = a.CreatedAt,
                            RentalBegin = a.RentalBegin,
                            RentalEnd = a.RentalEnd,
                            RentalReturn = a.RentalReturn,
                            LocationReceive = a.LocationReceive,
                            StatusAppointment = a.StatusAppointment,
                            StatusPayment = a.StatusPayment,
                            RentalPrice = a.RentalPrice,
                            OwnerId = a.OwnerId,
                            MotorbikeId = a.MotorbikeId,
                            Customer = new User() { Id = b.Id, Name = b.Name, PhoneNumber = b.PhoneNumber },
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
                            CreatedAt = a.CreatedAt,
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

            if(creterias.FilterStatusPayment != -1)
            {
                query = query.Where(u => u.StatusPayment == creterias.FilterStatusPayment);
            }

            if(creterias.FilterStatusAppointment != -1)
            {
                query = query.Where(u => u.StatusAppointment == creterias.FilterStatusAppointment);
            }




            if (sortBy == AppointmentSortBy.DateAscending)
            {
                query = query.OrderBy(c => c.CreatedAt.Date)
                                .ThenBy(c => c.CreatedAt.TimeOfDay);
            }
            else if (sortBy == AppointmentSortBy.DateDescending)
            {
                query = query.OrderByDescending(c => c.CreatedAt.Date)
                                .ThenBy(c => c.CreatedAt.TimeOfDay);
            }

            if (creterias.Skip >= 0 && creterias.Take > 0)
            {
                query = query.Skip(creterias.Skip).Take(creterias.Take);
            }


            return await query.ToListAsync();
        }
    }
}
