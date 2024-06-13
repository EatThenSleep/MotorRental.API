using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public interface IMotorbikeStateManager
    {
        Task<Motorbike> Add(Motorbike obj, string userId);
        Task<Motorbike> Update(Motorbike obj, bool afterSuccess = true, string? userId = null);
        Task<Motorbike> DeleteMotorbike(Guid Id, string userId);
    }
}
