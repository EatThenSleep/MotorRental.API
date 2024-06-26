using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Repository
{
    public interface IStatisticRepository
    {
        Task<object> StatisticAmountAndCount(string OwnerId, DateTime beginDate, DateTime endDate);
        Task<object> CalculateRentalsAndTotalAmount(string OwnerId, DateTime beginDate, DateTime endDate);
    }
}
