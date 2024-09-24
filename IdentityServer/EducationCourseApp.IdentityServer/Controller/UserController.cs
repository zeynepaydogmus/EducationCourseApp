using System;
using System.Linq;
using System.Threading.Tasks;
using EducationCourseApp.IdentityServer.Dtos;
using EducationCourseApp.IdentityServer.Models;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace EducationCourseApp.IdentityServer.Controller
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignupDto signupDto)
        {
            try
            {
                var user = new ApplicationUser
                {
                    UserName = signupDto.UserName,
                    Email = signupDto.Email,
                    City = signupDto.City
                };
                var result = await _userManager.CreateAsync(user, signupDto.Password);
                if (!result.Succeeded)
                {
                    //TODO: Fix.
                    return BadRequest();
                }

                return NoContent();
            }
            catch (Exception)
            {
                throw new Exception();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userIdClaim = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if (userIdClaim is null)
            {
                return BadRequest();
            }

            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user is null)
            {
                return BadRequest();
            }

            return Ok(user);
        }
    }
}