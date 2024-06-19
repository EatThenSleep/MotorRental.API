using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.UseCase.Feature;
using MotorRental.UseCase.Repository;
using MotorRental.Utilities;
using System.Linq;

namespace MotorRental.Infrastructure.Data.Repository
{
    public class MotorRepository : IMotorRepository
    {
        private readonly ApplicationDbContext _db;
    

        public MotorRepository(ApplicationDbContext db)
        {
            _db = db;
        }
        public async Task<Motorbike> Add(Motorbike motorbike, string userId)
        {
            // avoid insert parent when insert child
            _db.Entry(motorbike.Company).State = EntityState.Unchanged;
            _db.Entry(motorbike.User).State = EntityState.Unchanged;

            await _db.Motorbikes.AddAsync(motorbike);
            await _db.SaveChangesAsync();

            return motorbike;
        }
        #region henlper
        public async Task<Motorbike> CheckOfOwner(Guid Id, string userId)
        {
            var existingMotor = await _db.Motorbikes.FirstOrDefaultAsync(u => u.Id == Id
                                        && u.User.Id == userId);
            return existingMotor;
        }
        #endregion

        public async Task<Motorbike> DeleteByIdAsync(Guid Id, string userId)
        {
            var existingMotor = _db.Motorbikes.FirstOrDefault(u => u.Id == Id 
                                                              && u.User.Id == userId);

            if(existingMotor == null)
            {
                return null;
            }

            _db.Motorbikes.Remove(existingMotor);
            await _db.SaveChangesAsync();

            return existingMotor;
        }

        #region find
        public async Task<IEnumerable<Motorbike>> GetAllAsync(MotorbikeFindCreterias creterias,
                                                        MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending,
                                                        string? userId = null)
        {
            var motorbikes = _db.Motorbikes.AsQueryable();

            motorbikes = from a in _db.Motorbikes
                         join b in _db.Users on a.User.Id equals b.Id
                         join c in _db.Companies on a.Company.Id equals c.Id
                         select new Motorbike()
                         {
                             Id = a.Id,
                             Name = a.Name,
                             Type = a.Type,
                             Color = a.Color,
                             status = a.status,
                             PriceDay = a.PriceDay,
                             PriceWeek = a.PriceWeek,
                             PriceMonth = a.PriceMonth,
                             LicensePlate = a.LicensePlate,
                             MotorbikeAvatar = a.MotorbikeAvatar,
                             User = new User { Id = b.Id, Name = b.Name, PhoneNumber = b.PhoneNumber },
                             Company = new Company { Id = c.Id, Name = c.Name },
                         };


            if (userId != null)
            {
                motorbikes = motorbikes.Where(a => a.User.Id == userId);
            }

            if (creterias.FilterStatus > 0)
            {
                motorbikes = motorbikes.Where(a => a.status == creterias.FilterStatus);
            }

            if (creterias.FilterType > 0)
            {
                motorbikes = motorbikes.Where(a => a.Type == creterias.FilterType);
            }

            if (!string.IsNullOrEmpty(creterias.Name))
            {
                motorbikes = motorbikes.Where(a => a.Name
                                        .ToLower()
                                        .Contains(creterias.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(creterias.LicensePlate))
            {
                motorbikes = motorbikes.Where(a => a.LicensePlate
                                        .ToLower()
                                        .Contains(creterias.LicensePlate.ToLower()));
            }

            if (sortBy == MotorbikeSortBy.NameDescending)
            {
                motorbikes = motorbikes.OrderByDescending(motorbikes => motorbikes.Name);
            }
            else if(sortBy == MotorbikeSortBy.PriceAscending)
            {
                motorbikes = motorbikes.OrderBy(motorbikes => motorbikes.PriceDay +
                                                                motorbikes.PriceWeek +
                                                                motorbikes.PriceMonth);
            }
            else if (sortBy == MotorbikeSortBy.PriceDescending)
            {
                motorbikes = motorbikes.OrderByDescending(motorbikes => motorbikes.PriceDay +
                                                                motorbikes.PriceWeek +
                                                                motorbikes.PriceMonth);
            }
            else
            {
                motorbikes = motorbikes.OrderBy(motorbikes => motorbikes.Name);
            }

            if(creterias.Skip >= 0 && creterias.Take > 0)
            {
                motorbikes = motorbikes.Skip(creterias.Skip).Take(creterias.Take);
            }



            var res = await motorbikes.ToListAsync();

            return res;
        }

        public async Task<Motorbike> GetByIdAsync(Guid Id)
        {
            var motorbike = await _db.Motorbikes.AsNoTracking()
                            .Join(_db.Companies.AsNoTracking(),
                                a => a.Company.Id,
                                b => b.Id,
                                (a, b) => new Motorbike
                                {
                                    Id = a.Id,
                                    Name = a.Name,
                                    Type = a.Type,
                                    Color = a.Color,
                                    status = a.status,
                                    PriceDay = a.PriceDay,
                                    PriceWeek = a.PriceWeek,
                                    PriceMonth = a.PriceMonth,
                                    LicensePlate = a.LicensePlate,
                                    MotorbikeAvatar = a.MotorbikeAvatar,
                                    Capacity = a.Capacity,
                                    MadeIn = a.MadeIn,
                                    Speed = a.Speed,
                                    YearOfManufacture = a.YearOfManufacture,
                                    Company = new Company { Id = b.Id, Name = b.Name },
                                    User = new User { Id = a.User.Id, Name = a.User.Name },
                                })
                            .FirstOrDefaultAsync(u => u.Id == Id);

            return motorbike;
        }

        public async Task<Motorbike> GetByIdAndUserId(Guid Id, string UserId)
        {
            var res =  await _db.Motorbikes
                            .AsNoTracking()
                            .Where(v => v.Id == Id && v.User.Id == UserId)
                            .FirstOrDefaultAsync();

            return res;
        }
        #endregion

        #region update
        public async Task<Motorbike?> UpdateAsync(Motorbike motorbike, bool afterSuccess = true)
        {
            if (!afterSuccess)
            {
                var existingMotor = await _db.Motorbikes.FirstOrDefaultAsync(u => u.Id == motorbike.Id);

                existingMotor.Name = motorbike.Name;
                existingMotor.status = motorbike.status;
                existingMotor.Description = motorbike.Description;
                existingMotor.PriceDay = motorbike.PriceDay;
                existingMotor.PriceWeek = motorbike.PriceWeek;
                existingMotor.PriceMonth = motorbike.PriceMonth;
                existingMotor.LicensePlate = motorbike.LicensePlate;
                existingMotor.MotorbikeAvatar = motorbike.MotorbikeAvatar ?? "";
                existingMotor.UpdatedAt = DateTime.Now;

                _db.Entry(existingMotor).CurrentValues.SetValues(existingMotor);
                    await _db.SaveChangesAsync();

                motorbike = existingMotor;
            }
            else
            {
                _db.Entry(motorbike).CurrentValues.SetValues(motorbike);
                await _db.SaveChangesAsync();

                if (motorbike.User != null && motorbike.Company != null)
                {
                    motorbike.User.Motorbikes = [];
                    motorbike.Company.Motorbikes = [];
                }
            }

            

            return motorbike;
            
        }

        public Motorbike UpdateNotSave(Motorbike motorbike)
        {
            _db.Motorbikes.Update(motorbike);

            return motorbike;
        }
        #endregion
    }
}
