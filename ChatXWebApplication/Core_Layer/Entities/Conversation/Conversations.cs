using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities.Conversation
{
    public class Conversations
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; }
        public DateTime LastMessageAt { get; set; }
        public bool IsActive { get; set; }
    }
}
