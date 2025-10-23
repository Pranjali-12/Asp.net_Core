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
    public class UserController(SignInManager<User> signInManager):BaseApiController
    {
        [HttpPost("register")]
        public async Task<ActionResult<RegisterDto>> Register(RegisterDto registerDto)
        {
            return registerDto;
        }
    }
}