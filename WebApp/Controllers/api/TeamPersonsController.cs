using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        [HttpGet("/teams/{teamId}", Name ="GetTeamPersonByTeamId")]
        public async Task<IActionResult> GetAllByTeamAsync(int teamId)
        {
            return Ok( await _teamService.GetAllTeamPersonsAsync(teamId));
        }

        // GET: api/TeamPersons/5
        [HttpGet("{id}", Name = "GetTeamPersonById")]
        public IActionResult Get(long id)
        {
            var result = _teamService.GetTeamPersonById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
        
        // PUT: api/TeamPersons/teams/5/persons/5
        [HttpPut("/teams/{teamId}/persons/{userId}")]
        public IActionResult Put(long teamId, string userId, [FromBody]Boolean manager)
        {
            _teamService.PutPersonToTeam(teamId, userId, manager);
            return Ok();
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpPut("/teams/{teamId}/persons/{personId}")]
        public IActionResult RemovePersonFromTeam(long teamId, string personId)
        {
            _teamService.RemovePersonFromTeam(teamId, personId);
            return Ok();
        }
    }
}
