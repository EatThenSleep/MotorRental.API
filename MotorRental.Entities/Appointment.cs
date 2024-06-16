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

        [ForeignKey("Customer")]
        public string CustomerId { get; set; } = string.Empty;
        public User Customer { get; set; }
        [ForeignKey("Owner")]
        public string OwnerId { get; set; } = string.Empty;
        public User Owner { get; set; }

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
        public string? sessionId {  get; set; }

        public DateTime CreatedAt {  get; set; }
        public DateTime UpdateAt {  get; set; }

        public ICollection<Surcharge>? Surcharges { get; set;}

        public int GetTotalPrice()
        {
            int res = RentalPrice;
            if(Surcharges != null)
            {
                for (int i = 0; i < Surcharges.Count(); i++)
                {
                    res += Surcharges.ElementAt(i).Amount;
                }
            }
            return res;
        }
    }
}
