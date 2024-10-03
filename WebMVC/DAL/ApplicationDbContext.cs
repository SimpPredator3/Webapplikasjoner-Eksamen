using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebMVC.Models;

namespace WebMVC.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet for posts
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed some dummy data
            modelBuilder.Entity<Post>().HasData(
                new Post { Id = 1, Title = "First Post", Content = "This is the first post", Author = "John Doe", CreatedDate = DateTime.Now },
                new Post { Id = 2, Title = "Second Post", Content = "This is the second post", Author = "Jane Smith", CreatedDate = DateTime.Now },
                new Post { Id = 3, Title = "Third Post", Content = "This is the third post", Author = "Jim Beam", CreatedDate = DateTime.Now }
            );
        }
    }
}