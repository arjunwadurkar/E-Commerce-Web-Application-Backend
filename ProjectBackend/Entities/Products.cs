using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Entities
{
    public class Products
    {
        [Key]
        public int productid {  get; set; }
        public string title { get; set; }
        public string price { get; set; }
        public string description { get; set; }
        public string category { get; set; }
        public string image {  get; set; }
        public string rate { get; set; }

        public string count { get; set; }
    }
}
