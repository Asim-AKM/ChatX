using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class MessageReceipt
    {
        [Key]
        public Guid MessageReceiptId { get; set; } = Guid.NewGuid();
        public Guid MessageId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DeliveredAt { get; set; } = DateTime.UtcNow;
        public DateTime ReadAt { get; set; } = DateTime.UtcNow;
    }
}
