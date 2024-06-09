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

        public async Task<Motorbike> Add(Motorbike obj)
        {
            var res = await _motorRepository.Add(obj);

            return res;
        }

        public async Task<Motorbike> Update(Motorbike obj)
        {
            var res = await _motorRepository.UpdateAsync(obj);

            return res;
        }

        public async Task<IEnumerable<Motorbike>> GetAll(Guid? userId = null)
        {
            var res = await _motorRepository.GetAllAsync(userId);

            return res;
        }

        public async Task<Motorbike> GetById(Guid Id)
        {
            var res = await _motorRepository.GetByIdAsync(Id);



            return res;
        }
    }
}
