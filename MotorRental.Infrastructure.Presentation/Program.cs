
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MotorRental.Application;
using MotorRental.Application.IRepository;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.Data.IRepository;
using MotorRental.Infrastructure.Data.Repository;
using MotorRental.UseCase;

namespace MotorRental.Infrastructure.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling =
                    Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // add database
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // add AutoMapper Service
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // add repository
            builder.Services.AddScoped<IMotorService, MotorService>();
            builder.Services.AddScoped<IMotorRepository, MotorRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddIdentityCore<User>()
                        .AddRoles<IdentityRole>()
                        .AddTokenProvider<DataProtectorTokenProvider<User>>("MotorRental")
                        .AddEntityFrameworkStores<ApplicationDbContext>()
                        .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        AuthenticationType = "Jwt",
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey =
                        new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseCors(options =>
            {
                options.AllowAnyHeader();
                options.AllowAnyOrigin();
                options.AllowAnyMethod();
            });
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
