using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;

namespace BL.Services
{
    public interface IResultService
    {
        Task<List<StandingDTO>> GetAllStandingsAsync();
        List<StandingDTO> GetAllStandings();

        StandingDTO GetStandingsByTeamId(long teamId);

        GameDTO CreateGame(GameDTO game);
        Task<List<GameResultActionDto>> GetUserPendingResults();
        Task<GameResultActionDto> UpdateResult(long id, GameResultActionDto gameResult);
    }
}
