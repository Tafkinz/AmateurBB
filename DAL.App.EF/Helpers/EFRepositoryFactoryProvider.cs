using System;
using System.Collections.Generic;
using System.Text;
using DAL.App.EF.Repositories;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using DAL.EF.Repositories;
using DAL.interfaces;
using DAL.Interfaces;
using DAL.Interfaces.Helpers;

namespace DAL.App.EF.Helpers
{
    public class EFRepositoryFactoryProvider : IRepositoryFactoryProvider
    {
        private static readonly Dictionary<Type, Func<IDataContext, object>> _customRepositoryFactories = GetCustomRepoFactories();

        public Func<IDataContext, object> GetFactoryForStandarRepo<TEntity>() where TEntity : class
        {
            return (context) => new EFRepository<TEntity>(context as ApplicationDbContext);
        }

        public Func<IDataContext, object> GetFactoryForCustomRepo<TRepositoryInterface>() where TRepositoryInterface : class
        {

            _customRepositoryFactories.TryGetValue(typeof(TRepositoryInterface), out var factory);
            return factory;
        }

        private static Dictionary<Type, Func<IDataContext, object>> GetCustomRepoFactories()
        {
            return new Dictionary<Type, Func<IDataContext, object>>()
            {
                { typeof(IUserRepository), (dataContext) =>  new UserRepository(dataContext as ApplicationDbContext)},
                { typeof(IStandingsRepository), (dataContext) =>  new StandingsRepository(dataContext as ApplicationDbContext)},
                { typeof(IContactRepository), (dataContext) =>  new ContactRepository(dataContext as ApplicationDbContext)},
                { typeof(IContactTypeRepository), (dataContext) =>  new ContactTypeRepository(dataContext as ApplicationDbContext)},
                { typeof(ICourtRepository), (dataContext) =>  new CourtRepository(dataContext as ApplicationDbContext)},
                { typeof(IGameResultRepository), (dataContext) =>  new GameResultRepository(dataContext as ApplicationDbContext)},
                { typeof(IGameTeamRepository), (dataContext) =>  new GameTeamRepository(dataContext as ApplicationDbContext)},
                { typeof(IGameTypeRepository), (dataContext) =>  new GameTypeRepository(dataContext as ApplicationDbContext)},
                { typeof(IPersonTypeRepository), (dataContext) =>  new PersonTypeRepository(dataContext as ApplicationDbContext)},
                { typeof(ITeamPersonRepository), (dataContext) =>  new TeamPersonRepository(dataContext as ApplicationDbContext)},
                { typeof(IGameRepository), (dataContext) =>  new GameRepository(dataContext as ApplicationDbContext)},
                { typeof(ITeamRepository), (dataContext) =>  new TeamsRepository(dataContext as ApplicationDbContext)}
            };
        }

    }
}
