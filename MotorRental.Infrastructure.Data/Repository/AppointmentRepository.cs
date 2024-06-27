using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.UseCase;
using MotorRental.UseCase.Feature;
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

        #region find
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
                query = query.Where(u => u.StatusPayment == creterias.FilterStatusPayment &&
                                        u.StatusAppointment != SD.Status_Appointment_Process);
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

        public async Task<Appointment> GetById(Guid appointmentId, string userId)
        {
            var res =   await _db.Appointments
                                    .FirstOrDefaultAsync(u => u.Id == appointmentId &&
                                                        u.OwnerId == userId);
            return res;
        }

        public async Task<Appointment?> GetByIdInclude(Guid appointmentId,
                                                        string userId,
                                                        string role)
        {
            if(role == SD.VISTOR)
            {
                
                var res = await _db.Appointments
                                    .AsNoTracking()
                                    .Include(u => u.Surcharges)
                                    .FirstOrDefaultAsync(u => u.Id == appointmentId &&
                                                        u.CustomerId == userId);

                if (res.Surcharges.Any())
                {
                    foreach (var surcharge in res.Surcharges)
                    {
                        surcharge.Appointment = null;
                    }
                }

                return res;
            }
            else if(role == SD.OWNER)
            {
                var res = await _db.Appointments
                                    .AsNoTracking()
                                    .Include(u => u.Surcharges)
                                    .FirstOrDefaultAsync(u => u.Id == appointmentId &&
                                                        u.OwnerId == userId);

                if (res != null && res.Surcharges != null)
                {
                    foreach (var surcharge in res.Surcharges)
                    {
                        surcharge.Appointment = null;
                    }
                }

                return res;
            }
            return null;
        }


        public async Task<IEnumerable<Surcharge>> getSurcharges(Guid appointmentId)
        {
            return await _db.Surcharges.Where(u =>  u.Appointment.Id == appointmentId).ToListAsync();
        }

        public async Task<object> GetInformation(Guid appointmentId)
        {
            var res = await _db.Appointments.Include(u => u.Motorbike)
                                        .Include(u => u.Customer)
                                        .Select(x => new {
                                            Id = x.Id,
                                            CustomerName = x.Customer.Name,
                                            RentalBegin = x.RentalBegin.ToString("yyyy-MM-dd"),
                                            RentalEnd = x.RentalEnd.ToString("yyyy-MM-dd"),
                                            MotorbikeName = x.Motorbike.Name,
                                            LicensePlate = x.Motorbike.LicensePlate
                                        })
                .FirstOrDefaultAsync(u => u.Id == appointmentId);

            return res;
                                        
        }
        #endregion

        #region update
        public async Task<Appointment> UpdateAppointmentStatus(Appointment appointment,
                                                                int statusAppointment,
                                                                bool notSave = false)
        {
            appointment.StatusAppointment = statusAppointment;
            _db.Appointments.Attach(appointment);
            _db.Entry(appointment).Property(x => x.StatusAppointment).IsModified = true;
            if(!notSave ) await _db.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment> UpdateNotPay(Appointment appointment, Surcharge[] surcharges)
        {
            // update appointment
            _db.Entry(appointment).CurrentValues.SetValues(appointment);
           

            // create surcharges
            foreach(var surcharge in surcharges)
            {
                surcharge.CreatedAt = DateTime.Now;
                surcharge.Appointment = appointment;
                await _db.AddAsync(surcharge);
            }

            await _db.SaveChangesAsync();

            return appointment;
        }

        public async Task<Appointment> UpdatePayed(Appointment appointment)
        {
            var existingAppointment = await _db.Appointments
                                                .FirstOrDefaultAsync(u => u.Id == appointment.Id);

            existingAppointment.StatusPayment = SD.Status_Payment_Payed;
            existingAppointment.UpdateAt = DateTime.Now;

            _db.Update(existingAppointment);

            return appointment;
        }
        #endregion

        #region payment
        public async Task<Appointment> addSessionId(Guid id, string sessionId)
        {
            var existingAppointment = await _db.Appointments
                                                .FirstOrDefaultAsync(u => u.Id == id);

            existingAppointment.sessionId = sessionId;

            _db.Update(existingAppointment);

            await _db.SaveChangesAsync();

            return existingAppointment;
        }

        

        #endregion
    }
}
