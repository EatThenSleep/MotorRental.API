using MotorRental.Entities;
using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public class MotorbikeStateManager : IMotorbikeStateManager
    {
        private readonly IMotorRepository _motorRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICompanyRepository _companyRepository;

        public MotorbikeStateManager(IMotorRepository motorRepository,
                                    IUserRepository userRepository,
                                    ICompanyRepository companyRepository)
        {
            _motorRepository = motorRepository;
            _userRepository = userRepository;
            _companyRepository = companyRepository;
        }

        public async Task<Motorbike> Add(Motorbike obj, string userId)
        {
            // check User is exist
            var existingUser = await _userRepository.GetById(userId);
            obj.User = existingUser;
            if (existingUser == null)
            {
                return null;
            }

            // process company
            var existingCompany = await _companyRepository.GetByName(obj.Company.Name.Trim());
            obj.Company = existingCompany != null ? existingCompany : obj.Company;

            if (existingCompany == null)
            {
                Company company = new Company() { Name = obj.Company.Name };
                var objCreated = await _companyRepository.Add(company);
                obj.Company = objCreated;
            }

            var res = await _motorRepository.Add(obj, userId);

            return res;
        }

        public async Task<Motorbike> Update(Motorbike obj, bool afterSuccess = true, string? userId = null)
        {
            if (!afterSuccess)
            {
                var checkOwner = await _motorRepository.CheckOfOwner(obj.Id, userId);

                if (checkOwner == null) return null;
            }

            var res = await _motorRepository.UpdateAsync(obj, afterSuccess);

            return res;
        }

        public async Task<Motorbike> DeleteMotorbike(Guid Id, string userId)
        {

            var res = await _motorRepository.DeleteByIdAsync(Id, userId);

            return res;
        }
    }
}
