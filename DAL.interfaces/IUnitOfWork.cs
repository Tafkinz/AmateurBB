using System;
using System.Threading.Tasks;
using DAL.interfaces.Repositories;

namespace DAL.interfaces
{
    public interface IUnitOfWork
    {
        void SaveChanges();

        Task SaveChangesAsync();

        IRepository<TEntity> GetEntityRepository<TEntity>()
            where TEntity : class;
        TRepositoryInterface GetCustomRepository<TRepositoryInterface>()
            where TRepositoryInterface : class;

    }
}
