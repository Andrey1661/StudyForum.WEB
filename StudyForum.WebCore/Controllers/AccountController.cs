using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StudyForum.DataAccess.Core.Abstract.Services;
using StudyForum.DataAccess.Core.Models;
using StudyForum.WebCore.Utils;
using StudyForum.WebCore.ViewModels;
using StudyForum.WebCore.ViewModels.Account;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudyForum.WebCore.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private IConfiguration Configuration { get; }

        public AccountController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Takes user by login and password and creates bearer token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="userService"></param>
        /// <returns>Token for found user</returns>
        /// <response code="200">Returns token for found user</response>
        /// <response code="404">User not found</response>
        [HttpPost("/api/login")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> Login([FromBody]LoginViewModel model, [FromServices]IUserSevice userService)
        {
            var user = await userService.GetUserAsync(model.Email, model.Password);

            if (user == null)
            {
                return NotFound();
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Role, user.Role.Name),
            };

            var token = new JwtSecurityToken(
                issuer: Configuration["Issuer"],
                audience: Configuration["Audience"],
                expires: DateTime.UtcNow.AddHours(double.Parse(Configuration["ExpirationHours"])),
                notBefore: DateTime.UtcNow,
                claims: claims,
                signingCredentials: new SigningCredentials(
                    new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SigninKey"])),
                    SecurityAlgorithms.HmacSha256Signature)
            );

            var encoded = new JwtSecurityTokenHandler().WriteToken(token);

            return Ok(new
            {
                token = encoded,
                user = new UserViewModel
                {
                    Email = user.Email,
                    Role = user.Role.Name,
                    Id = user.Id
                }
            });
        }

        [Authorize]
        [HttpGet("/api/user")]
        [ProducesResponseType(typeof(UserViewModel), 200)]
        public IActionResult LoggedUser()
        {
            return Ok(User.GetUserData());
        }

        [HttpPost("/api/register")]
        [ProducesResponseType(typeof(Guid), 201)]
        [ProducesResponseType(400)]
        public async Task<IActionResult> Register([FromBody]RegisterViewModel model, [FromServices]IUserSevice userSevice, [FromServices]IMapper mapper)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var id = await userSevice.CreateUserAsync(mapper.Map<RegisterViewModel, CreateUserModel>(model));
            return Ok(new {userId = id});
        }
    }
}
