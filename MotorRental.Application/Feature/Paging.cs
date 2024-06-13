using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public class Paging
    {
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = int.MaxValue;
    }
}
