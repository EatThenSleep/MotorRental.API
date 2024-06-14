using MotorRental.Entities;
using MotorRental.UseCase.Helper;
using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public class RepositoryMotorbikeFinder : IMotorbikeFinder
    {
        private readonly IMotorRepository _motorRepository;

        public RepositoryMotorbikeFinder(IMotorRepository motorRepository)
        {
            _motorRepository = motorRepository;
        }

        public async Task<IEnumerable<Motorbike>> GetAll(MotorbikeFindCreterias creterias,
                                                        MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending,
                                                        string? userId = null)
        {
            // process creterias
            var creteriasProcessed = ValidationMotorbike.ProcessCreterias(creterias);

            var res = await _motorRepository.GetAllAsync(creteriasProcessed, sortBy, userId: userId);

            return res;
        }

        public async Task<Motorbike> GetById(Guid Id)
        {
            var res = await _motorRepository.GetByIdAsync(Id);

            return res;
        }

        
    }
}
