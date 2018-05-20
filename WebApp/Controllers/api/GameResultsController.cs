using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/GameResults")]
    public class GameResultsController : Controller
    {
        private readonly IResultService _resultService;

        public GameResultsController(IResultService resultService)
        {
            _resultService = resultService;
        }
        // GET: api/GameResults
        [HttpGet]
        public IActionResult GetPersonUnacceptedResults()
        {
            return Ok(_resultService.GetUserPendingResults());
        }

        // GET: api/GameResults/5
        [HttpGet("{id}", Name = "GetGameResultById")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/GameResults
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/GameResults/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(long id, [FromBody]GameResultActionDto gameResult)
        {
            if (!ModelState.IsValid) return BadRequest();
            var result = await _resultService.UpdateResult(id, gameResult);
            return Ok(result);
        }
        
        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
