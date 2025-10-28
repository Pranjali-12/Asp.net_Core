using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_crud.DTOs
{
    public class BasketDto
    {
        public int Id { get; set; }
        public List<BasketItemDto> Items { get; set; } = new();
    }
}