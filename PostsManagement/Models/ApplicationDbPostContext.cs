using Microsoft.EntityFrameworkCore;

namespace PostsManagement.Models
{
    public class ApplicationDbPostContext : DbContext
    {
        public ApplicationDbPostContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Post> Posts { get; set;}

    }
}
