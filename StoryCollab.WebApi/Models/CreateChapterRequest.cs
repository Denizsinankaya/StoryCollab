namespace StoryCollab.WebApi.Models;

public class CreateChapterRequest
{
    public int StoryId { get; set; }
    public string Title { get; set; } = default!;
    public string Content { get; set; } = default!;
}
