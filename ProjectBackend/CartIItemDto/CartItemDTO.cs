using ProjectBackend.Entities;

namespace ProjectBackend.CartItemDto
{
    public class CartItemDTO
    {
        public int UserId { get; set; }
        

        public int ProductId { get; set; }
       

        public int Quantity { get; set; }
    }
}
