namespace MotorRental.Infrastructure.Presentation.Models.DTO
{
    public class CreateAppoinmentDTO
    {
        public Guid MotorbikeId { get; set; }
        public Guid OwnerId { get; set; }

        public DateTime RentalBegin { get; set; } // is created
        public DateTime RentalEnd { get; set; }
        public string LocationReceive { get; set; } = string.Empty;
        public int RentalPrice { get; set; }
    }
}
