using Core_Layer.Entities;
using Core_Layer.InterfaceRepositories.IGenericRepo;
using Infrastructure_Layer.Data.ApplicationDbContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure_Layer.Persistence.ImplementationRepo.GenericRepo
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public Repository(AppDbContext context, DbSet<T> dbSet)
        {
            _context = context;
            _dbSet = dbSet;
        }

     
        public async Task<T> CreateAsync(T entities)
        {
            await _dbSet.AddAsync(entities);
            _context.SaveChanges();
            return entities;
        }

        public async Task<List<T>> GetAllAsync()
        {
            var list = await _dbSet.AsNoTracking().ToListAsync();
            return list;
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var data = await _dbSet.FindAsync(id);
            return data;
        }

        public async Task<string> Remove(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
            return "Removed Successfully";
        }

        public async Task<T> Update(T data)
        {
            _dbSet.Update(data);
            await _context.SaveChangesAsync();
            return data;
        }
    }
}
