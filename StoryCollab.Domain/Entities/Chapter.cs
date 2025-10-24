namespace StoryCollab.Domain.Entities;

public class Chapter
{
    public int Id { get; set; }

    public int StoryId { get; set; }
    public Story Story { get; set; } = default!;

    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;

    // versiyon ve durum
    public int VersionNumber { get; set; } = 1;
    public bool IsPublished { get; set; } = false;

    // kim yazdı
    public int AuthorId { get; set; }
    public User Author { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
