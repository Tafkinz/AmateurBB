using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/Game")]
    public class GameController : Controller
    {
        private readonly IResultService _resultService;

        public GameController(IResultService resultService)
        {
            _resultService = resultService;
        }
        // GET: api/Game
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Game/5
        [HttpGet("{id}", Name = "GetGameById")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Game
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]GameDTO game)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _resultService.CreateGame(game);
            return CreatedAtAction("Get", new { id = result.GameId }, result);
        }
        
        // PUT: api/Game/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
