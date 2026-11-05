
using Core_Layer.Entities;
using Core_Layer.Entities.Model;

namespace Core_Layer.InterfaceRepositories
{
    public interface IConversationRepo
    {
        Task<Guid?> FindPrivateConversationIdAsync(Guid userAId, Guid userBId);
        Task AddConversationAsync(Conversation conversation, List<ConversationMember> members);
        Task<bool> IsActiveMemberAsync(Guid conversationId, Guid userId);
        Task<Conversation?> GetConversationByIdAsync(Guid conversationId);
        
       Task<User?> GetOtherParticipantAsync(Guid conversationId, Guid currentUserId);
        Task<(List<ConversationSummary> Items, int TotalCount)> GetUserConversationsAsync(Guid userId, int pageNumber, int pageSize);



    }
}  

