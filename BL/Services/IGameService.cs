using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using Model;

namespace BL.Services
{
    public interface IGameService
    {
        Task<List<StandingDTO>> GetAllStandingsAsync();
        List<StandingDTO> GetAllStandings();

        StandingDTO GetStandingsByTeamId(long teamId);

        Task<GameDTO> CreateGame(GameDTO gameDto);
        Task<List<GameDTO>> GetUserPendingResults();
        Task<GameDTO> AcceptGame(long id, GameAcceptDTO accepted);

        GameDTO GetGameById(long id);

        GameTypeDTO AddGameType(GameTypeDTO type);

        List<GameTypeDTO> GetAllGameTypes();

        GameTypeDTO UpdateGameTypeById(long id, GameTypeDTO type);

        GameDTO UpdateGameById(long id, GameDTO dto);
    }
}
