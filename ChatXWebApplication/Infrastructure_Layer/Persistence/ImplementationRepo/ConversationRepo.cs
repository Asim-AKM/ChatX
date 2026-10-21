using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Infrastructure_Layer.Persistence.ImplementationRepo.GenericRepo;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure_Layer.Persistence.ImplementationRepo
{
    public class ConversationRepo : Repository<Conversation>, IConversationRepo 
    {
        private readonly AppDbContext _db;

        public ConversationRepo(AppDbContext db) : base(db)
        {
            _db = db;
        }
       

        public async Task AddConversationAsync(Conversation conversation, List<ConversationMember> members)
        {
            await _db.Conversations.AddAsync(conversation);
            await _db.ConversationMembers.AddRangeAsync(members);
            
        }

        public async Task<Guid?> FindPrivateConversationIdAsync(Guid userAId, Guid userBId)
        {
            {
                return await _db.ConversationMembers
                    .Where(m => m.IsActive && (m.UserId == userAId || m.UserId == userBId))
                    .GroupBy(m => m.ConversationId)
                    .Where(g => g.Select(x => x.UserId).Distinct().Count() == 2)
                    .Join(_db.Conversations.Where(c => c.IsActive),
                          g => g.Key, c => c.Id, (g, c) => (Guid?)c.Id)
                    .FirstOrDefaultAsync();
            }

        }
    }
}
    

