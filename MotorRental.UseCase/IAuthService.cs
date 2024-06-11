using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterUser(string name, string email, string password, string role);
        Task<AuthResponse> LoginUser(string email, string password);
    }
}
