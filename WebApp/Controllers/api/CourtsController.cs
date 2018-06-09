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

namespace WebApp.Controllers.api
{
    [Route("api/Courts")]
    public class CourtsController : Controller
    {
        private readonly ICourtService _courtService;
        private readonly AuthUtil _auth;

        public CourtsController(ICourtService courtService, AuthUtil auth)
        {
            _courtService = courtService;
            _auth = auth;
        }
        // GET: api/Courts
        /// <summary>
        /// Get all courts as list
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(List<CourtDTO>), 200)]
        public IActionResult GetAll()
        {
            return Ok(_courtService.GetAllCourts());
        }

        // GET: api/Courts/5
        /// <summary>
        /// Get a single court by ID
        /// </summary>
        [HttpGet("{id}", Name = "GetCourtById")]
        [ProducesResponseType(typeof(CourtDTO), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetById(long id)
        {
            var result = _courtService.GetCourtById(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        // POST: api/Courts
        /// <summary>
        /// Add a new court
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(CourtDTO), 201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(400)]
        [ProducesResponseType(403)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Post([FromBody]CourtDTO dto)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            if (!ModelState.IsValid) return BadRequest();
            var result = _courtService.AddCourt(dto);
            return CreatedAtAction("GetById", new { id = result.CourtId }, result);
        }

        // PUT: api/Courts/5
        /// <summary>
        /// Update court by ID with updated values
        /// </summary>
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(CourtDTO), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(403)]
        public IActionResult Put(long id, [FromBody]CourtDTO dto)
        {
            if (!_auth.IsAdmin())
            {
                return Forbid();
            }
            var result = _courtService.UpdateCourt(id, dto);
            if (result == null) return BadRequest();
            return Ok(result);
        }
    }
}
