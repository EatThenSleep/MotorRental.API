using MotorRental.Entities;
using MotorRental.Application.IRepository;
using Microsoft.EntityFrameworkCore;

namespace MotorRental.Infrastructure.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> Add(User user)
        {
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return user;
        }

        public async Task<User?> GetById(Guid UserId)
        {
            return await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == UserId);
        }

    }
}
