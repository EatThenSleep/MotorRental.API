
using MotorRental.Application;
using MotorRental.Application.IRepository;
using MotorRental.Infrastructure.Data;
using MotorRental.Infrastructure.Data.IRepository;
using MotorRental.Infrastructure.Data.Repository;

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
            builder.Services.ImplementPersistence(builder.Configuration);

            // add AutoMapper Service
            builder.Services.AddAutoMapper(typeof(MappingConfig));

            // add repository
            builder.Services.AddScoped<IMotorService, MotorService>();
            builder.Services.AddScoped<IMotorRepository, MotorRepository>();
            builder.Services.AddScoped<ICompanyRepository, CompanyRepository>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
