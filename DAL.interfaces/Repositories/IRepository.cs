using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.interfaces.Repositories
{
    public interface IRepository<TEntity> 
        where TEntity : class
    {

        List<TEntity> GetAll();

        Task<List<TEntity>> GetAllAsync();

        TEntity Add(TEntity entity);

        Task<TEntity> AddAsync(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity Remove(TEntity entity);

        TEntity RemoveById(long id);

        TEntity Find(params object[] keyValues);

        Task<TEntity> FindAsync(params object[] keyValues);
    }
}
