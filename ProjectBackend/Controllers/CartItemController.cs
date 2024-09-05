using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using ProjectBackend.CartItemDto;
using ProjectBackend.Data;
using ProjectBackend.Entities;

namespace ProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly DbContextData _context;
        private readonly IConfiguration _configuration;
        public CartItemController(DbContextData context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost("AddItemInCart")]
        public async Task<ActionResult> AdditemInCart(CartItemDTO addToCartDto)
        {
            var cartItem = await _context.AddToCarts.FirstOrDefaultAsync(c => c.UserId == addToCartDto.UserId && c.ProductId == addToCartDto.ProductId);

            if (cartItem == null)
            {
                // If the cart item doesn't exist, create a new one
                cartItem = new AddToCart
                {
                    UserId = addToCartDto.UserId,
                    ProductId = addToCartDto.ProductId,
                    Quantity = addToCartDto.Quantity
                };
                _context.AddToCarts.Add(cartItem);
            }
            else
            {
                // If the cart item already exists, increase the quantity
                cartItem.Quantity += addToCartDto.Quantity;
            }

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Item Added in Cart Successfully" });


        }

        //Update Quantity
        [HttpPut("UpdateCartItemQuantity")]
        public async Task<ActionResult> UpdateCartItemQuantity(CartItemDTO updateCartItemDto)
        {
            var cartItem = await _context.AddToCarts.FirstOrDefaultAsync(c => c.UserId == updateCartItemDto.UserId && c.ProductId == updateCartItemDto.ProductId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            cartItem.Quantity = updateCartItemDto.Quantity;

            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cart Item Quantity Updated Successfully" });
        }

        // Delete Cart Item
        [HttpDelete("DeleteCartItem")]
        public async Task<ActionResult> DeleteCartItem(int userId, int productId)
        {
            var cartItem = await _context.AddToCarts.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);

            if (cartItem == null)
            {
                return NotFound("Cart item not found.");
            }

            _context.AddToCarts.Remove(cartItem);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Cart Item Deleted Successfully" });
        }
    }
}
