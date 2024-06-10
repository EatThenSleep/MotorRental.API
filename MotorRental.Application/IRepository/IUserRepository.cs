using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Application.IRepository
{
    public interface IUserRepository
    {
        Task<User> Add(User user);
        Task<User> GetById(string UserId);
        Task<User> FindByEmail(string email);
    }
}
