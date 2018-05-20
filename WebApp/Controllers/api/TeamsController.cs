using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using DAL.App.Interfaces;
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
        [HttpGet]
        public IActionResult Get()
        {
            var teams = _teamService.GetAllTeams();
            if (teams == null) return NotFound();

            return Ok(teams);
        }

        // GET: api/Teams/5
        [HttpGet("{id}", Name = "GetTeamById")]
        public IActionResult Get(int id)
        {
            var team = _teamService.GetTeamById(id);
            if (team == null) return NotFound();

            return Ok(team);
        }
        
        // POST: api/Teams
        [HttpPost]
        public IActionResult Post([FromBody]TeamDTO team)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result =_teamService.AddTeam(team);
            return CreatedAtAction("Get", new {id = result.TeamId }, result);
        }
        
        // PUT: api/Teams/5
        [HttpPut("{id}")]
        public IActionResult Put(long id, [FromBody]TeamDTO team)
        {
            return Ok(_teamService.UpdateTeam(id, team));
        }
       
    }
}
