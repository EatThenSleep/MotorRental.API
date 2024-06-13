using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase.Feature
{
    public class MotorbikeFindCreterias : Paging
    {
        public int FilterStatus { get; set; } = 0;
        public int FilterType { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;

        public static MotorbikeFindCreterias Empty => new()
        {

        };
    }
}
