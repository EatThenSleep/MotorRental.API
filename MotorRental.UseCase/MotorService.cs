using MotorRental.UseCase.IRepository;
using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MotorRental.MotorRental.UseCase;
using MotorRental.UseCase;

namespace MotorRental.Application
{
    public class MotorService : IMotorService
    {
        public IMotorRepository _motorRepository { get; }

        public MotorService(IMotorRepository motorRepository)
        {
            _motorRepository = motorRepository;
        }

        public async Task<Motorbike> Add(Motorbike obj, string userId)
        {
            var res = await _motorRepository.Add(obj, userId);

            return res;
        }

        public async Task<Motorbike> Update(Motorbike obj, bool afterSuccess = true, string? userId = null)
        {
            if(userId != null)
            {
                return  await _motorRepository.CheckOfOwner(obj.Id, userId);
            }

            var res = await _motorRepository.UpdateAsync(obj, afterSuccess);

            return res;
        }

        public async Task<IEnumerable<Motorbike>> GetAll(MotorbikeFindCreterias creterias,
                                                        MotorbikeSortBy sortBy = MotorbikeSortBy.NameAscending,
                                                        string? userId = null)
        {
            // process creterias
            var creteriasProcessed = ProcessCreterias(creterias);

            var res = await _motorRepository.GetAllAsync(creteriasProcessed,sortBy,userId: userId);

            return res;
        }

        private MotorbikeFindCreterias ProcessCreterias(MotorbikeFindCreterias creterias)
        {
            if(creterias.FilterStatus == 0
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

        public async Task<Motorbike> GetById(Guid Id)
        {
            var res = await _motorRepository.GetByIdAsync(Id);

            return res;
        }

        public async Task<Motorbike> DeleteMotorbike(Guid Id, string userId)
        {
            var checker = await _motorRepository.CheckOfOwner(Id, userId);
            if (checker == null) return null;

            var res = await _motorRepository.DeleteByIdAsync(Id);

            return res;
        }
    }
}
