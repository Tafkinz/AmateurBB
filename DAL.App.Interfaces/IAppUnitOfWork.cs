using System;
using DAL.App.Interfaces.Repositories;
using DAL.interfaces;
using DAL.interfaces.Repositories;
using Model;

namespace DAL.App.Interfaces
{
    public interface IAppUnitOfWork : IUnitOfWork
    {
        IUserRepository Users { get; }
        IRepository<Standings> Standings { get; }
        IRepository<Team> Teams { get; }
        IRepository<Contact> Contacts { get; }
        IRepository<ContactType> ContactTypes { get; }
        IRepository<Court> Courts { get; }
        IRepository<Game> Games { get; }
        IRepository<GameResult> GameResults { get; }
        IRepository<GameTeam> GameTeams { get; }
        IRepository<GameType> GameTypes { get; }
        IRepository<PersonType> PersonTypes { get; }
        IRepository<TeamPerson> TeamPersons { get; }
    }
}
