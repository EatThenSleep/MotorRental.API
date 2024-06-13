using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public interface IMotorbikeFinder
    {
        Task<IEnumerable<Motorbike>> GetAll(MotorbikeFindCreterias creterias,
                                                       MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending,
                                                       string? userId = null);
        Task<Motorbike> GetById(Guid Id);
    }
}
