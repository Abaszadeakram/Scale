using System.Data.Entity;

namespace ScaleManagment.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("name=AppDbContext")
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Card> Cards { get; set; }
    }
}
