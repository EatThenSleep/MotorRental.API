namespace MotorRental.Infrastructure.Presentation.Models.DTO
{
    public class LoginResponseDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = new List<string>();
    }
}
