using Core_Layer.Enums;
using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        public string? DisplayName { get; set; }
        public string ?UserName { get; set; }
        public string ?Email { get; set; }
        public string ?Contact { get; set; }
        public string ?PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public PrivacyType Privacy { get; set; } = PrivacyType.Public;
        public DateTime? LastSeenAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
