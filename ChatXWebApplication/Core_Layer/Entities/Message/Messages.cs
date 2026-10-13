using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities.Message
{
    public class Messages
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ConversationId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }
        public DateTime EditAt { get; set; }
        public DateTime DeletedAt { get; set; }


    }
}
