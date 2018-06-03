using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/GameTeams")]
    public class GameTeamsController : Controller
    {
        private readonly IGameService _gameService;

        public GameTeamsController(IGameService gameService)
        {
            _gameService = gameService;
        }
        // GET: api/GameTeams
        /// <summary>
        /// Get all team's non-accepted games for a team manager
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GameActionDto>), 200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> GetUserPendingResults()
        {
            return Ok(await _gameService.GetUserPendingResults());
        }

        // PUT: api/GameTeams/5
        /// <summary>
        /// Accept or deny a game result by gameId
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GameDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [Authorize]
        public async Task<IActionResult> Put(long id, [FromBody]GameAcceptDTO accept)
        {
            var result = await _gameService.AcceptGame(id, accept);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }
       
    }
}
