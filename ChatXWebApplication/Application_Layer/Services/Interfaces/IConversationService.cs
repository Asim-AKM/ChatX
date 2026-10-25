
using Application_Layer.DTO_s.Request;
using Application_Layer.DTO_s.Response;

namespace Application_Layer.Services.Interfaces
{
    public interface IConversationService
    {
        public  Task<ConversationResponse> StartConversationAsync(Guid currentUserId, ConversationRequest request);
        Task<ConversationResponse> GetConversationByIdAsync(Guid currentUserId, Guid conversationId);
        Task<PagedResult<ConversationResponse>> GetConversationsAsync(Guid currentUserId, GetConversationsRequest request);
    }
}
