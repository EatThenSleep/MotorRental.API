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
        Task<Motorbike> Add(Motorbike obj, Guid userId);
        Task<Motorbike> Update(Motorbike obj, bool afterSuccess = true);
        Task<IEnumerable<Motorbike>> GetAll(Guid? userId = null);
        Task<Motorbike> GetById(Guid Id);
    }
}
