using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogAPI.Core.Entities
{
    public class RefreshToken
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public required string Token { get; set; }

        [Required]
        [ForeignKey(nameof(AspNetUser))]
        public required string UserId { get; set; }

        [Required]
        public DateTime ExpiresAt { get; set; }

        [Required]
        public bool IsRevoked { get; set; } = false;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual AspNetUser? AspNetUser { get; set; }

        public string? DeviceInfo { get; set; }
        public string? IpAddress { get; set; }
    }
}
