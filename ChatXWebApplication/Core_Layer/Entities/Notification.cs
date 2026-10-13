using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class Notification
    {
        [Key]
        public Guid NotificationId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public string? Type { get; set; }
        public string? Message { get; set; }
        public Guid MessageId { get; set; }
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ReadAt { get; set; } = DateTime.UtcNow;
    }
}
