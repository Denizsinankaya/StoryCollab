namespace StoryCollab.WebApi.Models;

public class CreateStoryRequest
{
    public string Title { get; set; } = default!;
    public string? Description { get; set; }
}
