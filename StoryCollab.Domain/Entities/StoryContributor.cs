namespace StoryCollab.Domain.Entities;
public class StoryContributor
{
    public int UserId { get; set; }
    public int StoryId { get; set; }

    public User User { get; set; }
    public Story Story { get; set; }
}
