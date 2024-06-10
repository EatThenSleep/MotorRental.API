using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MotorRental.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        public string? UrlAvatar { get; set; } = string.Empty;
        public string? IdentityNumber { get; set; } = string.Empty;
        public string? IdentityImagePre { get; set; } = string.Empty;
        public string? IdentityImageBack { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }

        public ICollection<Motorbike> Motorbikes { get; set; } = new List<Motorbike>();
    }
}
