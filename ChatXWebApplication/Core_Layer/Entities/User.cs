using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class User
    {
        [Key]
        public Guid UserId { get; set; } 
        public string ?UserName { get; set; }
        public string ?Email { get; set; }
        public string ?PasswordHash { get; set; }
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastSeenAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
