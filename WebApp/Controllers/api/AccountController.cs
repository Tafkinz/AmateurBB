using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/Account")]
    public class AccountController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public AccountController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Account
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Account/5
        [HttpGet("{id}", Name = "GetAccountById")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Account
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Account/5
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
