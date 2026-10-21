
namespace Application_Layer.Conversation
{
    public interface IConversationAccessGuard
    {
        Task EnsureActiveMemberAsync(Guid conversationId, Guid userId);
    }
}
