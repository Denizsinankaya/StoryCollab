namespace StoryCollab.Domain.Entities;

public class Story
{
    public int Id { get; set; }
    public string Title { get; set; } = default!;
    public string? Description { get; set; }

    // hikâyeyi oluşturan kişi
    public int OwnerId { get; set; }
    public User Owner { get; set; } = default!;

    // katkıcılar (çoktan çoğa)
    public ICollection<StoryContributor> Contributors { get; set; } = new List<StoryContributor>();

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
