using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; }= string.Empty;
        public string PasswordHash { get; set; }=string.Empty;
        public string? ProfileImageUrl { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime? LastSeenAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
    }
}
