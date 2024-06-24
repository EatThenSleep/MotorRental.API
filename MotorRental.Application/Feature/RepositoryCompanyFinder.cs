using MotorRental.Entities;
using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public class RepositoryCompanyFinder : ICompanyFinder
    {
        private readonly ICompanyRepository _companyRepository;

        public RepositoryCompanyFinder(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
            return await _companyRepository.GetAll();
        }
    }
}
