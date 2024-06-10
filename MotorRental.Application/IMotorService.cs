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
        Task<Motorbike> Add(Motorbike obj, string userId);
        Task<Motorbike> Update(Motorbike obj, bool afterSuccess = true);
        Task<IEnumerable<Motorbike>> GetAll(string? userId = null);
        Task<Motorbike> GetById(Guid Id);
        Task<Motorbike> DeleteMotorbike(Guid Id);
    }
}
