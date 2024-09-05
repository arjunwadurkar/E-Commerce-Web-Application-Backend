using Microsoft.EntityFrameworkCore;
using ProjectBackend.Entities;

namespace ProjectBackend.Data
{
    public class DbContextData : DbContext
    {

        public DbContextData(DbContextOptions options) : base(options) { }


        public DbSet<UsersData> UsersData { get; set; }
        public DbSet<Products> Products { get; set; }
        public DbSet<AddToCart> AddToCarts { get; set;}
        public DbSet<Orders> Orders { get; set; }
    }
}
