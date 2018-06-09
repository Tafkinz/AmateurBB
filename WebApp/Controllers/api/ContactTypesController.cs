using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using BL.Util;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace WebApp.Controllers.api
{
    [Route("api/ContactTypes")]
    public class ContactTypesController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly AuthUtil _auth;

        public ContactTypesController(IAccountService accountService, AuthUtil auth)
        {
            _accountService = accountService;
            _auth = auth;
        }
        // GET: api/ContactTypes
        /// <summary>
        /// Get all contact types as list
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<ContactTypeDTO>), 200)]
        public IActionResult Get()
        {
            return Ok(_accountService.GetAllContactTypes());
        }

        // GET: api/ContactTypes/5
        /// <summary>
        /// Get contact type by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetContactTypeById")]
        [ProducesResponseType(typeof(ContactTypeDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult Get(long id)
        {
            var result = _accountService.GetAllContactTypes().Single(p => p.ContactTypeId == id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/ContactTypes
        /// <summary>
        /// Add a contact type with value
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ContactTypeDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody]ContactTypeDTO value)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();
            var type = _accountService.AddContactType(value);
            return CreatedAtAction("Get", new {id = type.ContactTypeId}, type);
        }

        // PUT: api/ContactTypes/5
        /// <summary>
        /// Update contact type by ID and new value
        /// </summary>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContactTypeDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Put(long id, [FromBody]ContactTypeDTO value)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            var type = _accountService.UpdateContactType(id, value);
            if (type == null)
            {
                return NotFound();
            }

            return Ok(type);
        }

    }
}
