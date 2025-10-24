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

        modelBuilder.Entity<Story>(b =>
        {
            b.ToTable("Stories");
            b.HasKey(x => x.Id);
            b.Property(x => x.Title).HasMaxLength(256).IsRequired();
            b.Property(x => x.Description).HasMaxLength(1000);
            b.Property(x => x.CreatedAt).HasDefaultValueSql("GETUTCDATE()");

            // Story.Owner ilişkisi
            b.HasOne(s => s.Owner)
             .WithMany()
             .HasForeignKey(s => s.OwnerId)
             .OnDelete(DeleteBehavior.Restrict);
        });

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


        modelBuilder.Entity<StoryContributor>(b =>
        {
            b.ToTable("StoryContributors");
            b.HasKey(sc => new { sc.UserId, sc.StoryId });

            b.Property(sc => sc.Role).HasMaxLength(64).HasDefaultValue("Contributor");

            b.HasOne(sc => sc.User)
             .WithMany(u => u.Contributions)
             .HasForeignKey(sc => sc.UserId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(sc => sc.Story)
             .WithMany(s => s.Contributors)
             .HasForeignKey(sc => sc.StoryId)
             .OnDelete(DeleteBehavior.Cascade);
        });

    }
}