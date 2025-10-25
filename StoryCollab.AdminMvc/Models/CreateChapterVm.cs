namespace StoryCollab.AdminMvc.Models;

public class CreateChapterVm
{
    public int StoryId { get; set; }
    public string Title { get; set; } = "";
    public string Content { get; set; } = "";
}
