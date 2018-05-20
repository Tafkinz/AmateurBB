using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers.api
{
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {

        private readonly IAppUnitOfWork _uow;

        public ContactsController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/Contacts
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Contacts/5
        [HttpGet("{id}", Name = "GetContactById")]
        public string Get(int id)
        {
            return "value";
        }
        
        // POST: api/Contacts
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }
        
        // PUT: api/Contacts/5
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
