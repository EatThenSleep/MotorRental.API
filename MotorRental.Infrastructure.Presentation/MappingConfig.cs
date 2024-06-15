using AutoMapper;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Models.DTO;

namespace MotorRental.Infrastructure.Presentation
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Motorbike, MotorCreateDTO>().ReverseMap();
            CreateMap<Motorbike, MotorDTO>().ReverseMap();
            CreateMap<Motorbike, MotorUpdateDTO>().ReverseMap();

            CreateMap<Appointment, CreateAppoinmentDTO>().ReverseMap();

            CreateMap<Surcharge, CreateSurchargeDTO>().ReverseMap();
        }  
    }
}
