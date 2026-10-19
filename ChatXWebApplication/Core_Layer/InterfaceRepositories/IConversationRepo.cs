using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities;

namespace Core_Layer.InterfaceRepositories
{
    public interface IConversationRepo
    {
        Task<Guid?> FindPrivateConversationIdAsync(Guid userAId, Guid userBId);
        Task AddConversationAsync(Conversation conversation, List<ConversationMember> members);
    }
}  

