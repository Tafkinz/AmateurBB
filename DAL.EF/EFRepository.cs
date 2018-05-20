using DAL.interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DAL.EF.Repositories
{
   public class EFRepository<TEntity> : IRepository<TEntity> 
        where TEntity : class
   {
       protected DbContext RepositoryDbContext;
       protected DbSet<TEntity> RepositoryDbSet;

       public EFRepository(DbContext dbContext)
       {

           RepositoryDbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

           RepositoryDbSet = RepositoryDbContext.Set<TEntity>();
       }


        public List<TEntity> GetAll()
       {
           return RepositoryDbSet.ToList();
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await RepositoryDbSet.ToListAsync();
        }

        public TEntity Add(TEntity entity)
        {
            TEntity result = RepositoryDbSet.Add(entity).Entity;
            RepositoryDbContext.SaveChanges();
            return result;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await RepositoryDbSet.AddAsync(entity);
            await RepositoryDbContext.SaveChangesAsync();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            RepositoryDbSet.Update(entity);
            RepositoryDbContext.SaveChangesAsync();
            return entity;
        }

        public TEntity Remove(TEntity entity)
        {
            RepositoryDbSet.Remove(entity);
            RepositoryDbContext.SaveChanges();
            return entity;
        }

        public TEntity RemoveById(long id)
        {
            TEntity entity = this.Find(id);
            RepositoryDbSet.Remove(entity);
            RepositoryDbContext.SaveChanges();
            return entity;
        }

        public TEntity Find(params object[] keyValues)
        {
            return RepositoryDbSet.Find(keyValues);
        }

        public async Task<TEntity> FindAsync(params object[] keyValues)
        {
            return await RepositoryDbSet.FindAsync(keyValues);
        }
    }
}
