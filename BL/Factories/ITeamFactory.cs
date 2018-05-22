using System;
using System.Collections.Generic;
using System.Text;
using BL.DTO;
using DAL.App.EF.Repositories;
using Model;

namespace BL.Factories
{
    public interface ITeamFactory
    {
        TeamDTO Create(Team t, string manager, List<GameDTO> gameResult, StandingDTO standing);
        Team Create(TeamDTO t);
        TeamPersonDTO CreateFromTeamPersons(TeamPerson p);

    }
}
