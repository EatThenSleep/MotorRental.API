using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Statistical
{
    public interface IOrderStatistics
    {
        Task<object> StatisticAmountAndCount(string OwnerId, DateTime beginDate, DateTime endDate);

        Task<object> CalculateRentalsAndTotalAmount(string OwnerId, DateTime beginDate, DateTime endDate);
    }
}
