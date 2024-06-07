using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Application
{
    public interface IMotorService
    {
        Task<Motorbike> Add(Motorbike obj);
    }
}
