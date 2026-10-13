using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class ConversationMember
    {
        [Key]
        public Guid ConversationMemberId { get; set; } = Guid.NewGuid();
        public Guid ConversationId { get; set; }
        public Guid UserId { get; set; }

        public DateTime JoinedAt { get; set; }= DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

    }
}
