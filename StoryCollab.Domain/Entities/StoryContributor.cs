namespace StoryCollab.Domain.Entities;

public class StoryContributor
{
    public int UserId { get; set; }
    public User User { get; set; } = default!;

    public int StoryId { get; set; }
    public Story Story { get; set; } = default!;

    // ilerde rolleri (Editor / Viewer) olabilir
    public string Role { get; set; } = "Contributor";
}
