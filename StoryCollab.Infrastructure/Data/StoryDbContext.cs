using Microsoft.EntityFrameworkCore;
using StoryCollab.Domain.Entities;

namespace StoryCollab.Infrastructure.Data;
public class StoryDbContext : DbContext
{
    public StoryDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Story> Stories { get; set; }
    public DbSet<Chapter> Chapters { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<StoryContributor> StoryContributors { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        //Story - Contributor İlişkisi (many-to-many)
        modelBuilder.Entity<StoryContributor>()
            .HasKey(sc => new {sc.UserId, sc.StoryId});

        modelBuilder.Entity<StoryContributor>()
            .HasOne(sc => sc.User)
            .WithMany(u => u.Contributions)
            .HasForeignKey(sc => sc.UserId);
        
        modelBuilder.Entity<StoryContributor>()
            .HasOne(sc => sc.Story)
            .WithMany(s => s.Contributors)
            .HasForeignKey(sc => sc.StoryId);
    }

}
