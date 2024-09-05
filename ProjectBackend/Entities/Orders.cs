using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Entities
{
    public class Orders
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }
        public virtual UsersData User { get; set; }

        public int ProductId { get; set; }
        public virtual Products Product { get; set; }

        public string name { get; set; }

        public string mobile { get; set; } 
        public string address { get; set; } 
        public string state { get; set; } 
        public string city { get; set; } 
        public string pin { get; set; } 
        public string paymentMode { get; set; }
        public string paymentStatus { get; set; }
        public string Orderstatus { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; } // New time column
    }
}
