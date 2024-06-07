using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Entities
{
    public class Motorbike
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public int Type { get; set; } // 1: xe số, 2: xe tay ga, 3: xe côn
        public int? Speed { get; set; } //km/h
        public int? Capacity { get; set; } // lit
        public string Color { get; set; } = string.Empty;
        public int? YearOfManufacture { get; set; }
        public string MadeIn { get; set; } = string.Empty;
        [Required]
        public int status { get; set; } // 1: khả dụng, 2: đang cho thuê, 3: bảo dưỡng
        public string Description { get; set; } = string.Empty;
        [Required]
        public int PriceDay { get; set; }
        [Required]
        public int PriceWeek { get; set; }
        [Required]
        public int PriceMonth { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public string MotorbikeAvatar { get; set; } = string.Empty;
        public string LicensePlate { get; set; } = string.Empty;

        public Company Company { get; set; }
        public User User { get; set; }
    }
}
