using MotorRental.Entities;
using MotorRental.MotorRental.UseCase;
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
            var creteriasProcessed = ProcessCreterias(creterias);

            var res = await _motorRepository.GetAllAsync(creteriasProcessed, sortBy, userId: userId);

            return res;
        }

        public async Task<Motorbike> GetById(Guid Id)
        {
            var res = await _motorRepository.GetByIdAsync(Id);

            return res;
        }

        private MotorbikeFindCreterias ProcessCreterias(MotorbikeFindCreterias creterias)
        {
            if (creterias.FilterStatus == 0
                && creterias.FilterType == 0
                && string.IsNullOrEmpty(creterias.Name)
                && string.IsNullOrEmpty(creterias.LicensePlate)
                && creterias.Skip == 0
                && creterias.Take == int.MaxValue)
            {
                return MotorbikeFindCreterias.Empty;
            }

            if (creterias.FilterStatus > 3 && creterias.FilterStatus < 1) creterias.FilterStatus = 0;
            if (creterias.FilterType > 3 && creterias.FilterType < 1) creterias.FilterType = 0;

            return creterias;
        }
    }
}
