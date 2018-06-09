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
using Microsoft.AspNetCore.Identity;
using BL.Util;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Model;

namespace WebApp.Controllers.api
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Contacts")]
    public class ContactsController : Controller
    {

        private readonly IAccountService _accountService;
        private readonly AuthUtil _auth;
        private readonly UserManager<ApplicationUser> _userManager;

        public ContactsController(IAccountService accountService, AuthUtil auth, UserManager<ApplicationUser> userManager)
        {
            _accountService = accountService;
            _auth = auth;
            _userManager = userManager;
        }

        /// <summary>
        /// Get a contact by ID
        /// </summary>
        // GET: api/Contacts/5
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
        [HttpGet("user/{userId}", Name = "GetAllUserContacts")]
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
        [HttpPost]
        [ProducesResponseType(typeof(ContactsDTO), 201)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Post([FromBody]ContactsDTO dto)
        {
            _userManager.GetUserAsync(HttpContext.User);
            if (!_auth.IsCurrentUser(dto.UserId))
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
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ContactsDTO), 200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        public IActionResult Put(long id, [FromBody]ContactsDTO dto)
        {
            if (!_auth.IsCurrentUser(dto.UserId))
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
