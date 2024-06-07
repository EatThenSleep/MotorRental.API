using AutoMapper;
using MotorRental.Entities;
using MotorRental.Infrastructure.Presentation.Models;

namespace MotorRental.Infrastructure.Presentation
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Motorbike, MotorCreateDTO>().ReverseMap();
        }  
    }
}
