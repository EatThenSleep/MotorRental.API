using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using MotorRental.Entities;
using MotorRental.UseCase.Helper;
using MotorRental.UseCase.Mails;
using MotorRental.Utilities;

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
                UserName = email,
                Email = email,
            };

            /// create user
            var identityResult = await _userManager.CreateAsync(user, password);

            if (identityResult.Succeeded)
            {
                // default is VISITOR
                role = GetRole(role);
                identityResult = await _userManager.AddToRoleAsync(user, role);

                if (identityResult.Succeeded)
                {
                    // send email
                    IEmailSender emailSender = new SendgridEmailSennder(_configuration);
                    await emailSender.SendEmailAsync(email, "Welcome",CreatMessageWelcome(name));

                    // create token
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

        private string CreatMessageWelcome(string name)
        {
            return @"<!DOCTYPE html>
                <html>
                <head>
                    <title>Welcome Page</title>
                    <style>
                        body {
                            font-family: 'Arial', sans-serif;
                            background-color: #f0f0f0;
                            text-align: center;
                            padding-top: 50px;
                        }
                        h1 {
                            color: #333;
                            background-color: #ffd700;
                            display: inline-block;
                            padding: 10px 20px;
                            border-radius: 10px;
                            box-shadow: 2px 2px 10px rgba(0,0,0,0.2);
                        }
                        p {
                            color: #666;
                        }
                    </style>
                </head>
                <body>
                    <h1>Welcome " + name + @" to Our Website!</h1>
                    <p>We're glad to have you here.</p>
                </body>
                </html>";
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
