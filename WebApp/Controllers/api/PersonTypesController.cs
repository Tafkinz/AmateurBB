using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApp.Controllers.api
{
    [Route("api/PersonTypes")]
    public class PersonTypesController : Controller
    {
        private readonly IAccountService _accountService;

        public PersonTypesController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/PersonTypes
        /// <summary>
        /// Get all person types as list
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<PersonTypeDTO>), 200)]
        public IActionResult Get()
        {
            return Ok(_accountService.GetAllPersonTypes());
        }

        // GET: api/PersonTypes/5
        /// <summary>
        /// Get single person type by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetPersonTypeById")]
        [ProducesResponseType(typeof(PersonTypeDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var p = _accountService.GetPersonTypeById(id);
            if (p == null) return NotFound();

            return Ok(p);
        }

        // POST: api/PersonTypes
        /// <summary>
        /// Add new type for person
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(PersonTypeDTO), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize]
        public IActionResult Post([FromBody]PersonTypeDTO personType)
        {
            if (!ModelState.IsValid) return BadRequest();

            var result = _accountService.AddPersonType(personType);
            return CreatedAtAction("Get", new {id = result.PersonTypeId}, personType);
        }

        // PUT: api/PersonTypes/5
        /// <summary>
        /// Update person type with new value by ID
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(PersonTypeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [Authorize]
        public IActionResult Put(long id, [FromBody]PersonTypeDTO value)
        {
            var result = _accountService.UpdatePersonType(id, value);
            if (result == null) return NotFound();

            return Ok(result);
        }
       
    }
}
