namespace StoryCollab.WebApi.Models;

public class AuthResponse
{
    public string AccessToken { get; set; } = default!;
    public DateTime ExpiresAt { get; set; }
}
