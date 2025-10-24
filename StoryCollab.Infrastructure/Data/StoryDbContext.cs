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

        modelBuilder.Entity<User>(b =>
        {
            b.ToTable("Users");
            b.HasKey(x => x.Id);

            b.HasIndex(x => x.Email).IsUnique();
            b.Property(x => x.Email).IsRequired().HasMaxLength(256);
            b.Property(x => x.PasswordHash).IsRequired();

            b.Property(x => x.Role)
            .IsRequired()
            .HasMaxLength(32)
            .HasDefaultValue("User");
        });


        //Story - Contributor İlişkisi (many-to-many)
        modelBuilder.Entity<StoryContributor>()
            .HasKey(sc => new { sc.UserId, sc.StoryId });

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
