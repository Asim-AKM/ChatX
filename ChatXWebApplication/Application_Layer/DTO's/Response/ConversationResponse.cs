

namespace Application_Layer.DTO_s.Response
{
    public class ConversationResponse
    {
        
        
            public Guid ConversationId { get; set; }

            public List<ConversationParticipant> Participants { get; set; } = new();

            public DateTime CreatedAt { get; set; }

            public DateTime? LastMessageAt { get; set; }
        }
    }

