
using Application_Layer.Common;
using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;

namespace Application_Layer.Conversation
{
    public class ConversationAccessGuard : IConversationAccessGuard
    {
            private readonly IUnitOfWork _uow;

            public ConversationAccessGuard(IUnitOfWork uow)
            {
                _uow = uow;
            }

            public async Task EnsureActiveMemberAsync(Guid conversationId, Guid userId)
            {
                if (conversationId == Guid.Empty || userId == Guid.Empty)
                    throw new ConversationAccessDeniedException();

                var isMember = await _uow.ConversationRepo.IsActiveMemberAsync(conversationId, userId);

                if (!isMember)
                    throw new ConversationAccessDeniedException();
            }
        }
    }
    