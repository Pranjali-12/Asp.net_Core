using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace dotnet_crud.Models
{
    public class User: IdentityUser
    {
        public Basket? Basket { get; set; }
    }
}