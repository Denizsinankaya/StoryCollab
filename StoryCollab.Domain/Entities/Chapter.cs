namespace StoryCollab.Domain.Entities;
public class Chapter
{
    public int Id { get; set; }
    public int StoryId { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsPublished { get; set; }

    public Story Story { get; set; }
}
