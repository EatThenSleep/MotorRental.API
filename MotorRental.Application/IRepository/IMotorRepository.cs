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
        Task<Motorbike> Add(Motorbike motorbike, Guid userId);
        Task<Motorbike?> UpdateAsync(Motorbike motorbike);
        Task<IEnumerable<Motorbike>> GetAllAsync(Guid? userId = null);
        Task<Motorbike> GetByIdAsync(Guid Id);
    }
}
