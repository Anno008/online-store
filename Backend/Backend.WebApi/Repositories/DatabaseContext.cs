using Microsoft.EntityFrameworkCore;
using Backend.WebApi.Models;

namespace Backend.WebApi.Repositories
{
    public class DatabaseContext : DbContext
    {
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Component> Components { get; set; }
        public DbSet<ComponentType> ComponentTypes { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RefreshToken> Tokens { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<ChatRoomMessage> ChatRoomMessages { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }
    }
}
