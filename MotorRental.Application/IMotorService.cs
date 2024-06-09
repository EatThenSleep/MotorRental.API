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
        Task<Motorbike> Update(Motorbike obj);
        Task<IEnumerable<Motorbike>> GetAll(Guid? userId = null);
        Task<Motorbike> GetById(Guid Id);
    }
}
