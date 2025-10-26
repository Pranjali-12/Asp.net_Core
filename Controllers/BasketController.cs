using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_crud.Data;
using dotnet_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace dotnet_crud.Controllers
{
    public class BasketController:BaseApiController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly StoreContext _context;

        public BasketController(SignInManager<User> signInManager, StoreContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }
        
    }
}