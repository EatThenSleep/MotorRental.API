using MotorRental.Entities;
using MotorRental.UseCase.Feature;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Repository
{
    public interface IMotorRepository
    {
        Task<Motorbike> Add(Motorbike motorbike, string userId);
        Task<Motorbike?> UpdateAsync(Motorbike motorbike, bool afterSuccess = true);
        Task<IEnumerable<Motorbike>> GetAllAsync(MotorbikeFindCreterias creterias,
                                            MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending,
                                            string? userId = null);
        Task<Motorbike> GetByIdAsync(Guid Id);
        Task<Motorbike> CheckOfOwner(Guid Id, string userId);
        Task<Motorbike> DeleteByIdAsync(Guid Id, string userId);
        Task<Motorbike> GetByIdAndUserId(Guid Id, string UserId);

        Motorbike UpdateNotSave(Motorbike motorbike);
    }
}
