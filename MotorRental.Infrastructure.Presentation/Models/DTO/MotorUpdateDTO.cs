using System.ComponentModel.DataAnnotations;

namespace MotorRental.Infrastructure.Presentation.Models.DTO
{
    public class MotorUpdateDTO
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Mù à thằng lồn")]
        [MinLength(3, ErrorMessage = "Địt mẹ mày, nhập cho đàng hoàng vào!")]
        [MaxLength(20, ErrorMessage = "Bố mày là nhà văn à, viết cho gọn vào!")]
        public string Name { get; set; } = string.Empty;
       
        [Required]
        [Range(1, 3, ErrorMessage = "Mày đùa tao à. Một lần nữa pay acc")]
        public int status { get; set; } = 1; // 1: khả dụng, 2: đang cho thuê, 3: bảo dưỡng
        public string Description { get; set; } = string.Empty;
        [Required]
        [Range(5, 100)]
        public int PriceDay { get; set; }
        [Required]
        [Range(5, 100)]
        public int PriceWeek { get; set; }
        [Required]
        [Range(5, 100)]
        public int PriceMonth { get; set; }
        [Required]
        [MinLength(9)]
        [MaxLength(13)]
        public string LicensePlate { get; set; } = string.Empty;
        public IFormFile? Image { get; set; }
    }
}
