using MotorRental.Entities;
using Microsoft.EntityFrameworkCore;
using MotorRental.UseCase.Repository;

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


        public async Task<User> FindByEmail(string email)
        {
            var existingUser = await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Email.ToLower() == email.ToLower());
            return existingUser;
        }

        public async Task<User?> GetById(string UserId)
        {
            return await _db.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == UserId);
        }

        public User? GetByIdNoAsync(string UserId)
        {
            return _db.Users.AsNoTracking().FirstOrDefault(x => x.Id == UserId);
        }
        
    }
}
