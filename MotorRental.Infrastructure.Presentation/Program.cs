
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MotorRental.Entities;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.Data.Repository;
using MotorRental.Infrastructure.SqlServer.Repository;
using MotorRental.UseCase;
using MotorRental.UseCase.Feature;
using MotorRental.UseCase.Repository;
using MotorRental.UseCase.Statistical;
using MotorRental.UseCase.UnitOfWork;
using Stripe;

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
            builder.Services.AddTransient<IMotorRepository>(services =>
            new MotorRepository(services.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddTransient<IUserRepository>(services =>
           new UserRepository(services.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddTransient<ICompanyRepository>(services =>
          new CompanyRepository(services.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddTransient<IAppointmentUnitOfWork>(services =>
            new AppointmentUnitOfWork(services.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddTransient<IAppointmentRepository>(services =>
            new AppointmentRepository(services.GetRequiredService<ApplicationDbContext>()));

            builder.Services.AddTransient<IStatisticRepository>(services =>
           new StatisticRepository(services.GetRequiredService<ApplicationDbContext>()));

            //

            builder.Services.AddTransient<IMotorbikeStateManager>(services =>
            new MotorbikeStateManager(services.GetRequiredService<IMotorRepository>(),
            services.GetRequiredService<IUserRepository>(),
            services.GetRequiredService<ICompanyRepository>()));

            builder.Services.AddTransient<IMotorbikeFinder>(services =>
            new RepositoryMotorbikeFinder(services.GetRequiredService<IMotorRepository>()));

            builder.Services.AddTransient<IAppointmentStateManager>(services =>
            new AppointmentStateManager(services.GetRequiredService<IAppointmentUnitOfWork>()));

            builder.Services.AddTransient<IAppointmentFinder>(services =>
            new RepositoryAppointmentFinder(services.GetRequiredService<IAppointmentRepository>()));

            builder.Services.AddTransient<ICompanyFinder>(services =>
           new RepositoryCompanyFinder(services.GetRequiredService<ICompanyRepository>()));

            builder.Services.AddTransient<IOrderStatistics>(services =>
           new OrderStatistics(services.GetRequiredService<IStatisticRepository>()));
            //

            builder.Services.AddTransient<IAuthService, AuthService>();

            

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

            // configure authen, author for swagger
            // configure swagger for auth
            builder.Services.AddSwaggerGen(options =>
            {
                // add security definition
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. \r\n\r\n " +
                        "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\n" +
                        "Example: \"Bearer 12345abcdef\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Scheme = "Bearer"
                });
                // configure global security requirement
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                                        {
                                            Type = ReferenceType.SecurityScheme,
                                            Id = "Bearer"
                                        },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new List<string>()
                    }
                });
            });

            // add authentication for 

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

            StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();

            app.MapControllers();

            app.Run();
        }
    }
}
