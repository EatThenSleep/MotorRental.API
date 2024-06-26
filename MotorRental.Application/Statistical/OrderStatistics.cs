using MotorRental.UseCase.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Statistical
{
    public class OrderStatistics : IOrderStatistics
    {
        private readonly IStatisticRepository _statisticRepository;

        public OrderStatistics(IStatisticRepository statisticRepository)
        {
            _statisticRepository = statisticRepository;
        }

        public async Task<object> CalculateRentalsAndTotalAmount(string OwnerId, DateTime beginDate, DateTime endDate)
        {
            var result = await _statisticRepository
                .CalculateRentalsAndTotalAmount(OwnerId, beginDate, endDate);
            return result;
        }

        public async Task<object> StatisticAmountAndCount(string OwnerId, DateTime beginDate, DateTime endDate)
        {
           var result = await _statisticRepository
                            .StatisticAmountAndCount(OwnerId, beginDate, endDate);
            return result;
        }
    }
}
