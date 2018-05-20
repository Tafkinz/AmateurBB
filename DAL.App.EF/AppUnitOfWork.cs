using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using DAL.App.Interfaces.Repositories;
using DAL.EF;
using DAL.EF.Repositories;
using DAL.interfaces;
using DAL.interfaces.Repositories;
using DAL.Interfaces.Helpers;
using Model;

namespace DAL.App.EF
{
    public class AppUnitOfWork : IAppUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IRepositoryProvider _repositoryProvider;

        public AppUnitOfWork(IDataContext dbContext, IRepositoryProvider repositoryProvider)
        {
            _dbContext = dbContext as ApplicationDbContext;
            if (_dbContext == null)
            {
                throw new ArgumentNullException(nameof(dbContext));
            }

            _repositoryProvider = repositoryProvider;
        }

        public IUserRepository Users =>
            GetCustomRepository<IUserRepository>();

        public IRepository<Team> Teams =>
            GetEntityRepository<Team>();

        public IRepository<Standings> Standings =>
            GetEntityRepository<Standings>();

        public IRepository<Contact> Contacts =>
            GetEntityRepository<Contact>();

        public IRepository<ContactType> ContactTypes =>
            GetEntityRepository<ContactType>();

        public IRepository<Court> Courts =>
            GetEntityRepository<Court>();

        public IRepository<Game> Games =>
            GetEntityRepository<Game>();

        public IRepository<GameResult> GameResults =>
            GetEntityRepository<GameResult>();

        public IRepository<GameType> GameTypes =>
            GetEntityRepository<GameType>();

        public IRepository<GameTeam> GameTeams =>
            GetEntityRepository<GameTeam>();

        public IRepository<TeamPerson> TeamPersons =>
            GetEntityRepository<TeamPerson>();

        public IRepository<PersonType> PersonTypes =>
            GetEntityRepository<PersonType>();


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public IRepository<TEntity> GetEntityRepository<TEntity>() where TEntity : class
        {
            return _repositoryProvider
                .ProvideEntityRepository<TEntity>();
        }

        public TRepositoryInterface GetCustomRepository<TRepositoryInterface>() where TRepositoryInterface : class
        {
            return _repositoryProvider
                .ProvideCustomRepository<TRepositoryInterface>();
        }

    }
}
