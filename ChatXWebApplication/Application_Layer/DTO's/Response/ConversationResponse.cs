

namespace Application_Layer.DTO_s.Response
{
    public class ConversationResponse
    {
        public Guid ConversationId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastMessageAt { get; set; }
        public List<ConversationParticipant> Participants { get; set; } = new();
    }
    public  class PagedResult<T>
    {
        public IReadOnlyList<T> Items { get; init; } = Array.Empty<T>();
        public int TotalCount { get; init; }
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public bool HasNextPage => PageNumber * PageSize < TotalCount;
    }
}

