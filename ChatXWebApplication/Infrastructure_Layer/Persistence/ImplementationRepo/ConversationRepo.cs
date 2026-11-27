
using Core_Layer.Entities;
using Core_Layer.Entities.Model;
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
                          g => g.Key, c => c.ConversationId, (g, c) => (Guid?)c.ConversationId)
                    .FirstOrDefaultAsync();
            }

        }

        public async Task<User?> GetOtherParticipantAsync(Guid conversationId, Guid currentUserId)
        {
            return await(
                from m in _db.ConversationMembers
                join u in _db.Users on m.UserId equals u.UserId
                where m.ConversationId == conversationId
                      && m.IsActive
                      && m.UserId != currentUserId
                select u)
                .FirstOrDefaultAsync();
        }


        public async Task<bool> IsActiveMemberAsync(Guid conversationId, Guid userId)
        {
            {
                return await _db.ConversationMembers
                    .AnyAsync(m => m.ConversationId == conversationId
                                && m.UserId == userId
                                && m.IsActive
                                && _db.Conversations.Any(c => c.ConversationId == conversationId && c.IsActive));
            }
        }
        public async Task<Conversation?> GetConversationByIdAsync(Guid conversationId)
        {
            return await _db.Conversations.FirstOrDefaultAsync(c => c.ConversationId == conversationId && c.IsActive);
        }

        public async Task<(List<ConversationSummary> Items, int TotalCount)> GetUserConversationsAsync( Guid userId, int pageNumber, int pageSize)
        {
            var query =
                from mine in _db.ConversationMembers
                join c in _db.Conversations on mine.ConversationId equals c.ConversationId
                join other in _db.ConversationMembers on c.ConversationId equals other.ConversationId
                join u in _db.Users on other.UserId equals u.UserId
                where mine.UserId == userId && mine.IsActive && c.IsActive
                      && other.UserId != userId && other.IsActive
                select new ConversationSummary
                {
                    ConversationId = c.ConversationId,
                    CreatedAt = c.CreatedAt,
                    LastMessageAt = c.LastMessageAt,
                    OtherUserId = u.UserId,
                    OtherUserName = u.UserName,
                    OtherUserProfileImageUrl = u.ProfileImageUrl
                };

            var totalCount = await query.CountAsync();

            var items = await query
                .OrderByDescending(x => x.LastMessageAt ?? x.CreatedAt)
                .ThenByDescending(x => x.ConversationId)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();

            return (items, totalCount);
        }
    }
}
    

