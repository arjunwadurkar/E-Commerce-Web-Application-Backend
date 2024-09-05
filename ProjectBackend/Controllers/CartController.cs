
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProjectBackend.Data;
using ProjectBackend.Entities;
using ProjectBackend.ProductDTO;





namespace ProjectBackend.Controllers
{
    //[Route("api/[controller]")]
   //[ApiController]
    public class CartController : DbContext
    {
        private readonly DbContextData _context;
        private readonly IConfiguration _configuration;
        private SymmetricSecurityKey _key;

        public CartController(DbContextData context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }
        [HttpGet("GetCartItems/{id}")]
        public ActionResult<IEnumerable<CartDTO>> GetCartItems(int id)
        {
            var cartItems = _context.AddToCarts
                .Where(item => item.UserId == id)
                .Include(item => item.Product)
                .Select(item => new CartDTO
                {
                    image = item.Product.image,
                    price = item.Product.price,
                    title = item.Product.title,
                    description = item.Product.description,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                })
                .ToList();

         /*   if (cartItems == null || cartItems.Count == 0)
            {
               // Response.StatusCode = 404;
                return null;

            }*/
         

            return cartItems;
        }
        

    }
}
