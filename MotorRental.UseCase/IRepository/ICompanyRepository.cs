using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Infrastructure.Data.IRepository
{
    public interface ICompanyRepository
    {
        Task<Company> Add(Company company);
        Task<Company> GetByName(string name);
    }
}
