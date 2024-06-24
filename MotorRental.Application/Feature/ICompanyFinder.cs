using MotorRental.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public interface ICompanyFinder
    {
        Task<IEnumerable<Company>> GetAll();
    }
}
