using Microsoft.EntityFrameworkCore;
using MotorRental.Entities;
using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Infrastructure.Data.Repository
{
    public class CompanyRepository : ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Company> Add(Company company)
        {
            await _db.Companies.AddAsync(company);
            await _db.SaveChangesAsync();

            return company;
        }

        public async Task<IEnumerable<Company>> GetAll()
        {
           return await _db.Companies.ToListAsync();
        }

        public async Task<Company?> GetByName(string name)
        {
               return await _db.Companies.AsNoTracking().FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower());
        }
    }
}
