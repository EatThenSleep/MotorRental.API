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
        Task<Motorbike> Add(Motorbike motorbike, string userId);
        Task<Motorbike?> UpdateAsync(Motorbike motorbike, bool afterSuccess = true);
        Task<IEnumerable<Motorbike>> GetAllAsync(string? userId = null);
        Task<Motorbike> GetByIdAsync(Guid Id);
        Task<Motorbike> DeleteByIdAsync(Guid Id);
    }
}
