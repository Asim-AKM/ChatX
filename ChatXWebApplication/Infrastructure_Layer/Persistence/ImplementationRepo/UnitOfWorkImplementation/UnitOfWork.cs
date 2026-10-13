using Core_Layer.InterfaceRepositories.UnitOfWorkInterface;
using Infrastructure_Layer.Data.ApplicationDbContext;

namespace Infrastructure_Layer.Persistence.ImplementationRepo.UnitOfWorkImplementation
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;




        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }
        public async Task ExecuteInTransactionAsync(Func<Task> operation, bool autosaveChanges = true)
        {
            if (operation == null)
                throw new ArgumentNullException(nameof(operation));

            await using var transaction = await _context.Database.BeginTransactionAsync();

            try
            {
                await operation();
                if (autosaveChanges)
                {
                    await _context.SaveChangesAsync();
                }
                await transaction.CommitAsync();

            }
            catch
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
