using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using DAL.App.Interfaces;
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

        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
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
        [Authorize]
        public IActionResult Post([FromBody]TeamDTO team)
        {
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
        [Authorize]
        public IActionResult Put(long id, [FromBody]TeamDTO team)
        {
            return Ok(_teamService.UpdateTeam(id, team));
        }
       
    }
}
