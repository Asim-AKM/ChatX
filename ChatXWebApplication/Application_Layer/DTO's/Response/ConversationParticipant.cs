

namespace Application_Layer.DTO_s.Response
{
    public class ConversationParticipant
    {
        

        public Guid UserId { get; set; }
        public string? DisplayName { get; set; } = string.Empty;
        public string? Contact {get; set; }
        public string? ProfileImageUrl { get; set; }
    }
}
