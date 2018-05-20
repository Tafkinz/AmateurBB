using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApp.Controllers.api
{
    [Route("api/PersonTypes")]
    public class PersonTypesController : Controller
    {
        private readonly IAppUnitOfWork _uow;

        public PersonTypesController(IAppUnitOfWork uow)
        {
            _uow = uow;
        }
        // GET: api/PersonTypes
        [HttpGet]
        public IEnumerable<PersonType> Get()
        {
            return _uow.PersonTypes.GetAll();
        }

        // GET: api/PersonTypes/5
        [HttpGet("{id}", Name = "GetPersonTypeById")]
        public IActionResult Get(int id)
        {
            var p = _uow.PersonTypes.Find(id);
            if (p == null) return NotFound();

            return Ok(p);
        }
        
        // POST: api/PersonTypes
        [HttpPost]
        public IActionResult Post([FromBody]PersonType personType)
        {
            if (!ModelState.IsValid) return BadRequest("Invalid input");
            _uow.PersonTypes.Add(personType);
            return CreatedAtAction("Get", new {id = personType.PersonTypeId}, personType);
        }
        
        // PUT: api/PersonTypes/5
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
