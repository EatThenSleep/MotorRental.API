using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public class AuthResponse
    {
        public bool isSuccess { get; set; } = true;
        public AuthInfo authInfo { get; set; }
        public List<string> ErrorMessage { get; set; } = new List<string>();
    }

    public class AuthInfo
    {
        public string Email { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
        public string Token { get; set; } = string.Empty;
    }
}
