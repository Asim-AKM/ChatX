using Core_Layer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core_Layer.InterfaceRepositories.IGenericRepo
{
    public interface IRepository<T> where T : class
    {
      
        Task<T> CreateAsync(T entities);
        Task<List<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task<T> Update(T data);

        Task<string> Remove(T entity);


    }

}
