using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Entities
{
    public class Appointment
    {
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserId { get; set; } = string.Empty;
        public User User { get; set; }

        [Required]
        [ForeignKey("Motorbike")]
        public Guid MotorbikeId { get; set; }
        public Motorbike Motorbike { get; set; }

        public DateTime RentalBegin { get; set; } // is created
        public DateTime RentalEnd { get; set; }
        public DateTime RentalReturn { get; set; }
        public string LocationReceive { get; set; } = string.Empty;
        public int StatusAppointment {  get; set; }
        public int StatusPayment {  get; set; }
        public int RentalPrice {  get; set; }
        public DateTime UpdateAt {  get; set; }

        public ICollection<Surcharge> Surcharges { get; set;}
    }
}
