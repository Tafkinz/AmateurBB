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

        Task<GameDTO> CreateGame(GameDTO gameDto);
        Task<List<GameActionDto>> GetUserPendingResults();
        Task<GameActionDto> UpdateResult(long id, GameActionDto game);
    }
}
