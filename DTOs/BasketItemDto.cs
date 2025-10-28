using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dotnet_crud.DTOs
{
    public class BasketItemDto
    {
         public int ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public decimal Price { get; set; } 
        public int Quantity { get; set; }
    }
}