using MotorRental.Application.IRepository;
using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<Motorbike>> GetAll(string? userId = null)
        {
            var res = await _motorRepository.GetAllAsync(userId);

            return res;
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
