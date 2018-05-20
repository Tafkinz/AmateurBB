using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/GameTeams")]
    public class GameTeamsController : Controller
    {
        // GET: api/GameTeams
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/GameTeams/5
        [HttpGet("{id}", Name = "GetGameTeamById")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/GameTeams
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/GameTeams/5
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
