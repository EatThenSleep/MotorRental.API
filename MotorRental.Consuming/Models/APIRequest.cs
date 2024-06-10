using static MotorRental.Utilities.SD;

namespace MotorRental.Consuming.Models
{
    public class APIRequest
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; } = string.Empty;
        public object Data { get; set; }
        public string Token { get; set; }
        public ContentType ContentType { get; set; } = ContentType.Json;
    }
}
