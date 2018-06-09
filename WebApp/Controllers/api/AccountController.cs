using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BL.DTO;
using BL.Services;
using BL.Util;
using DAL.App.EF;
using DAL.App.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Model;
using WebApp.Models.AccountViewModels;
using WebApp.Services;

namespace WebApp.Controllers.api
{
    [Route("api/Security")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IAccountService _accountService;

        public AccountController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager, IConfiguration configuration, IAccountService accountService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _configuration = configuration;
            _accountService = accountService;
        }

        [HttpPost]
        [Route("getToken")]
        public async Task<IActionResult> GetToken([FromBody] LoginViewModel loginViewModel)
        {
            var user = await _userManager.FindByEmailAsync(loginViewModel.Email);
            if (user != null)
            {
                var result = await _signInManager
                    .CheckPasswordSignInAsync(user, loginViewModel.Password, false);
                if (result.Succeeded)
                {
                    var options = new IdentityOptions();

                    var claims = new List<Claim>()
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(options.ClaimsIdentity.UserIdClaimType, user.Id),
                        new Claim(options.ClaimsIdentity.UserNameClaimType, user.UserName),
                    };

                    var userClaims = await _userManager.GetClaimsAsync(user);
                    claims.AddRange(userClaims);

                    var userRoles = await _userManager.GetRolesAsync(user);
                    foreach (var userRole in userRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole));
                        var role = await _userManager.FindByNameAsync(userRole);
                        if (role != null)
                        {
                            var roleClaims = await _userManager.GetClaimsAsync(role);
                            foreach (Claim roleClaim in roleClaims)
                            {
                                claims.Add(roleClaim);
                            }
                        }
                    }

                    var key = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(_configuration["Token:Key"]));
                    var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                    var token = new JwtSecurityToken(
                        _configuration["Token:Issuer"],
                        _configuration["Token:Issuer"],
                        claims,
                        expires: DateTime.Now.AddMinutes(50),
                        signingCredentials: creds
                        );

                    var res = new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token)
                    };

                    return Ok(res);
                }
            }
            return BadRequest("Could not create token");
        }

        [HttpGet("user")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [ProducesResponseType(typeof(UserDTO),200)]
        [ProducesResponseType(404)]
        [ProducesResponseType(401)]
        public IActionResult GetUser()
        {
            var user = _accountService.GetCurrentUser();
            if (user == null) return NotFound();

            return Ok(user);
        }
    }
}
