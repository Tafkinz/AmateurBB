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
using Microsoft.AspNetCore.Routing.Constraints;

namespace WebApp.Controllers.api
{
    [Route("api/Teams")]
    public class TeamsController : Controller
    {
        private readonly ITeamService _teamService;
        private readonly AuthUtil _auth;

        public TeamsController(ITeamService teamService, AuthUtil auth)
        {
            _teamService = teamService;
            _auth = auth;
        }
        // GET: api/Teams
        /// <summary>
        /// Get all teams as list
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<TeamDTO>), 200)]
        public IActionResult Get()
        {
            var teams = _teamService.GetAllTeams();

            return Ok(teams);
        }

        // GET: api/Teams/5
        /// <summary>
        /// Get a single team by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetTeamById")]
        [ProducesResponseType(typeof(TeamDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null) return NotFound();

            return Ok(team);
        }

        // POST: api/Teams
        /// <summary>
        /// Add a new team
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(TeamDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody]TeamDTO team)
        {
            if (!_auth.IsAdmin() && !_auth.IsManager())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();
            var result =_teamService.AddTeam(team);
            return CreatedAtAction("Get", new {id = result.TeamId }, result);
        }

        // PUT: api/Teams/5
        /// <summary>
        /// Update team
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(TeamDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Put(long id, [FromBody]TeamDTO team)
        {
            if (!_auth.IsAdmin() && !_auth.IsManager())
            {
                return Forbid();
            }
            return Ok(_teamService.UpdateTeam(id, team));
        }
       
    }
}
