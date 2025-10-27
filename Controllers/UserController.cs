using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_crud.DTOs;
using dotnet_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_crud.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly SignInManager<User> _signInManager;

        public UserController(SignInManager<User> signInManager)
        {
            _signInManager = signInManager;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDto registerDto)
        {
            var user = new User { UserName = registerDto.Email, Email = registerDto.Email };

            var result = await _signInManager.UserManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return ValidationProblem();
            }

            return Ok("User registered successfully!");
        }

        [HttpGet("user-info")]
        public async Task<ActionResult> GetUserInfo()
        {
            if (User.Identity?.IsAuthenticated == false)
            {
                return NoContent();
            }

            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(new { user.Email, user.UserName });

        }

        [HttpPost("logout")]
        public async Task<ActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok("User logged out");
        }
    }
}