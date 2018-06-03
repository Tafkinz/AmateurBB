using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using Model;

namespace BL.Services
{
    public interface ITeamService
    {
        TeamDTO AddTeam(TeamDTO team);

        TeamDTO GetTeamById(long teamId);

        List<TeamDTO> GetAllTeams();

        TeamDTO UpdateTeam(long id, TeamDTO team);

        Task<List<TeamPersonDTO>> GetAllTeamPersonsAsync(long teamId);

        TeamPersonDTO GetTeamPersonById(long id);

        void PutPersonToTeam(long teamId, string userId, Boolean isManager);

        void RemovePersonFromTeam(long teamId, string userId);
    }
}
