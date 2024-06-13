using MotorRental.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.UseCase
{
    public class AppointmentFindCreterias : Paging
    {
        public int FilterStatusAppointment { get; set; } = -1;
        public int FilterStatusPayment { get; set; } = -1;

    }
}
