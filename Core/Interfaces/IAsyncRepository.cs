using System.Collections.Generic;
using Core.Entities;
using System.Threading.Tasks;

namespace Core.Interfaces
{
    public interface IAsyncRepository<T> where T : BaseEntity
    {        
        Task<T> AddAsync(T entity);
    }
}
