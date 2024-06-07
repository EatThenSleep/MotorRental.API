using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Application.IRepository
{
    public interface IMotorRepository
    {
        Task<Motorbike> Add(Motorbike motorbike);
    }
}
