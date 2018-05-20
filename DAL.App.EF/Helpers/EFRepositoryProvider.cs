using System;
using System.Collections.Generic;
using DAL.interfaces;
using DAL.interfaces.Repositories;
using DAL.Interfaces.Helpers;

namespace DAL.App.EF.Helpers
{
    public class EFRepositoryProvider : IRepositoryProvider
    {
        private readonly Dictionary<Type, object> _repositoryCache = new Dictionary<Type, object>();
        private readonly ApplicationDbContext _context;
        private readonly IRepositoryFactoryProvider _factoryProvider;

        public EFRepositoryProvider(IDataContext context, IRepositoryFactoryProvider factoryProvider)
        {
            _factoryProvider = factoryProvider;
            _context = context as ApplicationDbContext;
        }

        public IRepository<TEntity> ProvideEntityRepository<TEntity>() where TEntity : class
        {
            return GetOrCreateRepository<IRepository<TEntity>>(
                _factoryProvider.GetFactoryForStandarRepo<TEntity>()
                );
        }

        public TRepositoryInterface ProvideCustomRepository<TRepositoryInterface>() where TRepositoryInterface : class
        {
            return GetOrCreateRepository<TRepositoryInterface>(
                _factoryProvider.GetFactoryForCustomRepo<TRepositoryInterface>()
                );
        }


        TRepositoryInterface GetOrCreateRepository<TRepositoryInterface>(
            Func<ApplicationDbContext, object> repoCreationFunc) where TRepositoryInterface : class
        {
            _repositoryCache.TryGetValue(typeof(TRepositoryInterface), out var repo);
            if (repo != null)
            {
                return (TRepositoryInterface)repo;
            }

            if (repoCreationFunc == null)
            {
                throw new ArgumentNullException(nameof(repoCreationFunc));
            }

            repo = repoCreationFunc(_context);
            if (repo == null)
            {
                throw new NullReferenceException(typeof(TRepositoryInterface).FullName);
            }

            _repositoryCache.Add(typeof(TRepositoryInterface), repo);
            return (TRepositoryInterface)repo;
        }
    }
}
