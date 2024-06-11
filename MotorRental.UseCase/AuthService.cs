using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MotorRental.Application.IRepository;
using MotorRental.Entities;
using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MotorRental.UseCase
{
    public class AuthService : IAuthService
    {
        private  UserManager<User> _userManager;
        private  IConfiguration _configuration;

        public AuthService(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        public async Task<AuthResponse> LoginUser(string email, string password)
        {
            // check email
            var identityUser = await _userManager.FindByEmailAsync(email);

            if (identityUser is not null)
            {
                // Check Password
                var checkPasswordResutl = await _userManager.CheckPasswordAsync(identityUser, password);

                if (checkPasswordResutl)
                {
                    var roles = await _userManager.GetRolesAsync(identityUser);

                    // Create a token and response
                    var jwtToken = new ManageToken().CreateJwtToken(identityUser
                                                                    , roles.ToList(),
                                                                    _configuration);

                    var authInfo = new AuthInfo()
                    {
                        Email = email,
                        Roles = roles.ToList(),
                        Token = jwtToken
                    };

                    return new AuthResponse { authInfo = authInfo };
                }
            }


            return new AuthResponse() { isSuccess = false, ErrorMessage = new List<string>() { "Email or Password Incorrect" } };
        }

        public async Task<AuthResponse> RegisterUser(string name ,string email, string password, string role)
        {
            var user = new User
            {
                Name = name,
                UserName = name,
                Email = email,
            };

            /// create user
            var identityResult = await _userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                // Add Role to user (Reader)
                role = GetRole(role);
                identityResult = await _userManager.AddToRoleAsync(user, role);

                if (identityResult.Succeeded)
                {
                    var token = new ManageToken().CreateJwtToken(user,
                                            new List<string> { role },
                                            _configuration);

                    var authInfo = new AuthInfo()
                    {
                        Email = email,
                        Roles = new List<string> { role },
                        Token = token
                    };

                    return new AuthResponse { authInfo = authInfo };
                }
            }

            List<string> errors = new();
            if (identityResult.Errors.Any())
            {
                
                foreach (var error in identityResult.Errors)
                {
                    errors.Add(error.Description);
                }
            }

            return new AuthResponse() { isSuccess = false, ErrorMessage = errors };
        }

        private string GetRole(string role)
        {
            foreach(string item in SD.RoleForUser)
            {
                if(string.Equals(item.ToLower(), role.ToLower())){
                    return item;
                }
            }
            return SD.VISTOR;
        }
    }
}
