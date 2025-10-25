namespace StoryCollab.AdminMvc.Models;

public class AddContributorVm
{
    public int StoryId { get; set; }
    public string Email { get; set; } = "";
    public string? Role { get; set; }
}
