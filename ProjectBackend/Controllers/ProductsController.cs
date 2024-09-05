using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjectBackend.Data;
using ProjectBackend.Entities;
using ProjectBackend.ProductDTO;
using ProjectBackend.UsersDTO;

namespace ProjectBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbContextData _context;
        private readonly IConfiguration _configuration;
        private SymmetricSecurityKey _key;

        public ProductsController(DbContextData context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;

        }

        [HttpGet("GetProductList")]
        public ActionResult <List<Products>> GetProductList()
        {
            var products = _context.Products.ToList();
            return products;
        }

        [HttpPost("AddProduct")]
        public ActionResult ADDProduct(Products newproduct)
        {
            if (newproduct != null)
            {
                _context.Products.Add(newproduct);
                _context.SaveChanges();
                return Ok();

            }
            return BadRequest();
        }

        [HttpPut("UpdateProductById/{productid}")]
        public ActionResult UpdateProductById (int productid, [FromBody] UpdateProductDTO updateUser)
        {

            var product = _context.Products.FirstOrDefault(u => u.productid == productid);
            if (product == null)
            {
                return BadRequest("User Not Found");
            }

            product.title = updateUser.title;
            product.price = updateUser.price;
            product.description = updateUser.description;
            product.category = updateUser.category;
            product.image = updateUser.image;
            product.rate = updateUser.rate;
            product.count = updateUser.count;

            _context.SaveChanges();

            return Ok(new { Massege = "Product Updated" });
        }

       [ HttpDelete("DeleteProductById/{id}")]
        public ActionResult DeleteProductById(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
                return Ok(new { Massege = "Delete Sucessfully" });
            }
            return NotFound("Product Not Found");



        }

        [HttpGet("getproductbyid/{productid}")]
        public ActionResult<Products> GetProductById(int productid)
        {
            var product = _context.Products.Find(productid);
            if (product != null)
            {

                return product;
            }
            else
            {
                return NotFound("Product not found");
            }

        }


    }



     
    }

