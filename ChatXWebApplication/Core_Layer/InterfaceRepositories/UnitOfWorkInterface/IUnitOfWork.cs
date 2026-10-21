using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.InterfaceRepositories.UnitOfWorkInterface
{
    public interface IUnitOfWork
    {
         IUserRepo UserRepo { get; }
        IConversationRepo ConversationRepo { get; }
        
        Task<int> SaveChangesAsync();
        Task ExecuteInTransactionAsync(Func<Task> operation, bool autosaveChanges = true);
    }
}
