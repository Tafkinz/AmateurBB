using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/Standings")]
    public class StandingsController : Controller
    {
        private readonly IResultService _resultService;

        public StandingsController(IResultService result)
        {
            _resultService = result;
        }
        // GET: api/Standings
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _resultService.GetAllStandingsAsync());
        }

        // GET: api/Standings/teams/5
        [HttpGet("teams/{teamId}", Name = "GetTeamStandings")]
        public IActionResult GetById(long teamId)
        {
            var result = _resultService.GetStandingsByTeamId(teamId);
            if (result == null) return NotFound();

            return Ok(result);
        }
       
    }
}
