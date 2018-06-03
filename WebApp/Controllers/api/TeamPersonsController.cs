using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApp.Controllers.api
{
    [Route("api/TeamPersons")]
    public class TeamPersonsController : Controller
    {
        private readonly ITeamService _teamService;

        public TeamPersonsController(ITeamService teamService)
        {
            _teamService = teamService;
        }
        // GET: api/TeamPersons/teams/5
        /// <summary>
        /// Get all team related people by teamId
        /// </summary>
        [HttpGet("/teams/{teamId}", Name ="GetTeamPersonByTeamId")]
        [ProducesResponseType(typeof(List<TeamPersonDTO>), 200)]
        [ProducesResponseType(401)]
        [Authorize]
        public async Task<IActionResult> GetAllByTeamAsync(long teamId)
        {
            return Ok( await _teamService.GetAllTeamPersonsAsync(teamId));
        }

        // GET: api/TeamPersons/5
        /// <summary>
        /// Get a person for team by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetTeamPersonById")]
        [ProducesResponseType(typeof(TeamPerson), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public IActionResult Get(long id)
        {
            var result = _teamService.GetTeamPersonById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // PUT: api/TeamPersons/teams/5/persons/5
        /// <summary>
        /// Create a connection to a team for person
        /// </summary>
        [HttpPut("/teams/{teamId}/persons/{userId}")]
        [ProducesResponseType(typeof(TeamPersonDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public IActionResult Put(long teamId, string userId, [FromBody]Boolean manager)
        {
            _teamService.PutPersonToTeam(teamId, userId, manager);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Remove a person's relation to team
        /// </summary>
        [HttpPut("/teams/{teamId}/persons/{personId}")]
        [ProducesResponseType(typeof(TeamPersonDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public IActionResult RemovePersonFromTeam(long teamId, string personId)
        {
            _teamService.RemovePersonFromTeam(teamId, personId);
            return Ok();
        }
    }
}
