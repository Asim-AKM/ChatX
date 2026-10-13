
using System.ComponentModel.DataAnnotations;

namespace Core_Layer.Entities
{
    public class UserConnection
    {
        [Key]
        public Guid UserConnectionId { get; set; } = Guid.NewGuid();
        public Guid UserId { get; set; }
        public DateTime ConnectedAt { get; set; }= DateTime.UtcNow;
    }
}
