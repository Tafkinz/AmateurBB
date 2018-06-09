using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using BL.Util;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IGameService _gameService;
        private readonly AuthUtil _auth;

        public GameController(IGameService gameService, AuthUtil auth)
        {
            _gameService = gameService;
            _auth = auth;
        }
        // GET: api/Game
        /// <summary>
        /// Get current standings with all teams
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<StandingDTO>), 200)]
        public IActionResult GetStandings()
        {
            return Ok(_gameService.GetAllStandings());
        }

        /// <summary>
        /// Get standings for a single team by teamId
        /// </summary>
        [HttpGet("{teamId}", Name = "GetStandingsByTeamId")]
        [ProducesResponseType(typeof(StandingDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetStandingsByTeam(long teamId)
        {
            return Ok(_gameService.GetStandingsByTeamId(teamId));
        }

        // GET: api/Game/5
        /// <summary>
        /// Get game by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetGameById")]
        [ProducesResponseType(typeof(GameDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetGameById(long id)
        {
            var result = _gameService.GetGameById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Game
        /// <summary>
        /// Post a new game with referees and teams
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GameDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> CreateGame([FromBody]GameDTO game)
        {
            if (!_auth.IsAdmin() && !_auth.IsManager())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();
            var result = await _gameService.CreateGame(game);
            return CreatedAtAction("GetGameById", new { id = result.GameId }, result);
        }

        // PUT: api/Game/5
        /// <summary>
        /// Update game with new values by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(List<GameDTO>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Put(int id, [FromBody]GameDTO dto)
        {
            if (!_auth.IsAdmin() && !_auth.IsManager())
            {
                return Forbid();
            }
            var result = _gameService.UpdateGameById(id, dto);
            if (result == null) return NotFound();
            return Ok(result);
        }
       
    }
}
