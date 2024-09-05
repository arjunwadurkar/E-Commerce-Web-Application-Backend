using System.ComponentModel.DataAnnotations;

namespace ProjectBackend.Entities
{
    public class UsersData
    {
        [Key]
        public int id { get; set; }

        public string name { get; set; } = string.Empty;

        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;

        public string role { get; set; } = string.Empty;
        public string mobile { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string state { get; set; } = string.Empty;
        public string city { get; set; } = string.Empty;
        public string pin { get; set; } = string.Empty;




        
     
    }
}
