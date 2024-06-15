using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Entities
{
    public class Surcharge
    {
        public Guid Id { get; set; }
        public int Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public Appointment Appointment { get; set; }
    }
}
