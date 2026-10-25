

namespace Core_Layer.Entities.Model
{
    public class ConversationSummary
    {
        public Guid ConversationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public Guid OtherUserId { get; set; }
        public string? OtherUserName { get; set; }
        public string? OtherUserProfileImageUrl { get; set; }
    }
}