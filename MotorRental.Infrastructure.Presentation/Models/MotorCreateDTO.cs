using System.ComponentModel.DataAnnotations;

namespace MotorRental.Infrastructure.Presentation.Models
{
    public class MotorCreateDTO
    {
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

        public string CompanyName { get; set; } = string.Empty;
        public Guid UserId {  get; set; }
    }
}
