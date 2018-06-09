using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using BL.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApp.Controllers.api
{
    [Route("api/GameTypes")]
    public class GameTypesController : Controller
    {
        private readonly IGameService _gameService;
        private readonly AuthUtil _auth;

        public GameTypesController(IGameService gameService, AuthUtil auth)
        {
            _gameService = gameService;
            _auth = auth;
        }
        // GET: api/GameTypes
        /// <summary>
        /// Get all game types as list
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<GameTypeDTO>), 200)]
        public IActionResult Get()
        {
            return Ok(_gameService.GetAllGameTypes());
        }

        /// <summary>
        /// Get a game type by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetGameTypeById")]
        [ProducesResponseType(typeof(GameTypeDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetGameTypeById(long id)
        {
            var result = _gameService.GetAllGameTypes().Single(p => p.GameTypeId == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/GameTypes
        /// <summary>
        /// Create a new game type with value
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(GameTypeDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody]GameTypeDTO value)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();

            var result =_gameService.AddGameType(value);
            return CreatedAtAction("GetGameTypeById", new { id = result.GameTypeId }, result);
        }

        // PUT: api/GameTypes/5
        /// <summary>
        /// Update game type's name by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(GameTypeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Put(long id, [FromBody]GameTypeDTO value)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            var result = _gameService.UpdateGameTypeById(id, value);
            if (result == null) return NotFound();

            return Ok(result);
        }
    }
}
