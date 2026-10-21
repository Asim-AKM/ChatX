

namespace Application_Layer.Common
{
    public sealed class ConversationAccessDeniedException : Exception
    {
        public ConversationAccessDeniedException(): base("You do not have access to this conversation.") { }
        
    }
}
