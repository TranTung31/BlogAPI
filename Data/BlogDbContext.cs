using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Data
{
    public class BlogDbContext : DbContext
    {
        public BlogDbContext(DbContextOptions<BlogDbContext> opt) : base(opt)
        {
        }

        #region DbSet
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Author> Authors { get; set; }
        #endregion
    }
}
