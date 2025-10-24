namespace StoryCollab.Domain.Entities;
public class User
{
    public int Id { get; set; }
    public string Email { get; set; } = default!;
    public string PasswordHash { get; set; } = default!;
    public string Role { get; set; } = "User";
    public ICollection<StoryContributor> Contributions { get; set; } // buraya bir bak!
}
