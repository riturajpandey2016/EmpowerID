using Microsoft.EntityFrameworkCore;

namespace CommentsManagement.Models
{
    public class ApplicationDbCommentContext : DbContext
    {
        public ApplicationDbCommentContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set;}
    }
}
