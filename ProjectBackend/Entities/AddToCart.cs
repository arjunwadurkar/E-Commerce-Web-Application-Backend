using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ProjectBackend.Entities
{
    public class AddToCart
    {
        /*  [Key]
         public int CartId { get; set; }

           [ForeignKey("UsersData")]
           public int UserId { get; set; }

           public int Quantity { get; set; }


           public UsersData User { get; set; }

           public int productid { get; set; }
        */

        [Key]
        public int CartId { get; set; }

        public int UserId { get; set; }
        public virtual UsersData User { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }

        public int Quantity { get; set; }
    }
}
