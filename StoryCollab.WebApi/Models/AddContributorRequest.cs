namespace StoryCollab.WebApi.Models;

public class AddContributorRequest
{
    public string Email { get; set; } = default!;
    public string? Role { get; set; }
}
