using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnet_crud.Data;
using dotnet_crud.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotnet_crud.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly SignInManager<User> _signInManager;
        private readonly StoreContext _context;

        public BasketController(SignInManager<User> signInManager, StoreContext context)
        {
            _signInManager = signInManager;
            _context = context;
        }

        [HttpPost("addItem/{productId}")]
        public async Task<ActionResult> AddItem(int productId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var product = await _context.Products.FindAsync(productId);

            if (product == null)
            {
                return NotFound("Product not found");
            }


            var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.UserId == user.Id);

            if (basket == null)
            {
                basket = new Basket
                {
                    UserId = user.Id
                };
                _context.Baskets.Add(basket);
            }

            var existingItem = basket.Items.FirstOrDefault(i => i.ProductId == productId);

            if (existingItem != null)
            {
                existingItem.Quantity++;
            }
            else
            {
                if (product.QuantityInStock > 0)
                {
                    basket.Items.Add(new BasketItem
                    {
                        ProductId = productId,
                        Quantity = 1
                    });

                    product.QuantityInStock--;
                    _context.Products.Update(product);
                }
                else
                {
                    return BadRequest("Product is not available");
                }

            }

            await _context.SaveChangesAsync();

            return Ok(new { message = $"{product.Name} added to basket" });
        }

        [HttpDelete("removeItem/{productId}")]
        public async Task<ActionResult> RemoveItem(int productId)
        {
            var user = await _signInManager.UserManager.GetUserAsync(User);

            if (user == null)
            {
                return Unauthorized();
            }

            var basket = await _context.Baskets.Include(b => b.Items).FirstOrDefaultAsync(b => b.UserId == user.Id);

            if (basket == null)
            {
                return NotFound("Basket not found");
            }

            var item = basket.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item == null)
            {
                return BadRequest("Product is not in the basket");
            }

            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound("Product not found");
            }

            if (item.Quantity > 1)
            {
                item.Quantity--;
            }
            else
            {
                basket.Items.Remove(item);
            }

            product.QuantityInStock++;
            _context.Products.Update(product);

            _context.Baskets.Update(basket);

            await _context.SaveChangesAsync();

            return Ok(new { message = "Item removed from the basket" });
        }
    }
}