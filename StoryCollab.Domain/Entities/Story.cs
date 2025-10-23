namespace StoryCollab.Domain.Entities;
public class Story
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    public ICollection<Chapter> Chapters { get; set; } //Bir hikayenin birden fazla bölümü olabilir
    public ICollection<StoryContributor> Contributors { get; set; } // Hikayeye katkı yapan kullanıcılar
}
