using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using api.Models;

namespace api.DAL
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Post> Posts { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Upvote> Upvotes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Post-Upvote relationship
            modelBuilder.Entity<Upvote>()
                .HasOne(upvote => upvote.Post)
                .WithMany(post => post.Upvotes)
                .HasForeignKey(upvote => upvote.PostId)
                .OnDelete(DeleteBehavior.Cascade); // Ensure cascade delete is applied correctly
        }
    }
}
