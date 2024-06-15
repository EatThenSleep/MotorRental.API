using MotorRental.Utilities;

namespace MotorRental.Infrastructure.Presentation.Models.DTO
{
    public class CreateSurchargeDTO
    {
        public int Amount { get; set; }
        public string Reason { get; set; } = string.Empty;
    }
}
