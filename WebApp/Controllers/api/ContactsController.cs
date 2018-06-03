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
using Microsoft.AspNet.Identity;

namespace WebApp.Controllers.api
{
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {

        private readonly IAccountService _accountService;

        public ContactsController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Get a contact by ID
        /// </summary>
        // GET: api/Contacts/5
        [Authorize]
        [HttpGet("{id}", Name = "GetContactById")]
        [ProducesResponseType(typeof(List<ContactsDTO>), 200)]
        [ProducesResponseType(401)]
        public IActionResult Get(long id)
        {
            var result = _accountService.GetContactById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Get All Contacts for user by userId
        /// </summary>
        [Authorize]
        [HttpGet("{userId}", Name = "GetAllUserContacts")]
        [ProducesResponseType(typeof(List<ContactsDTO>), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetAllUserContacts(string userId)
        {
            var result = _accountService.GetAllContactsForUser(userId);
            return Ok(result);
        }

        /// <summary>
        /// Add a contact to use with userId and ContactTypeId
        /// </summary>
        // POST: api/Contacts
        [Authorize]
        [HttpPost]
        [ProducesResponseType(typeof(ContactsDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Post([FromBody]ContactsDTO dto)
        {
            if (dto.UserId != User.Identity.GetUserId())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();
            var result = _accountService.AddContact(dto);
            return CreatedAtAction("Get", new { id = result.ContactId }, result);
        }

        // PUT: api/Contacts/5
        /// <summary>
        /// Update a contact by ID and new value
        /// </summary>
        [Authorize]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContactsDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Put(long id, [FromBody]ContactsDTO dto)
        {
            if (dto.UserId != User.Identity.GetUserId())
            {
                return Forbid();
            }
            var result = _accountService.UpdateContact(id, dto);
            if (result == null) return NotFound();

            return Ok(result);
        }

        // DELETE: api/ApiWithActions/5
        /// <summary>
        /// Remove a contact by ID
        /// </summary>
        [Authorize]
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ContactsDTO), 200)]
        [ProducesResponseType(401)]
        public IActionResult Delete(long id)
        {
            var result = _accountService.RemoveContactById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }
    }
}
