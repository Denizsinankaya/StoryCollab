namespace StoryCollab.WebApi.Models;

public class RegisterRequest
{
    public string Email { get; set; }
    public string Password { get; set; }

    // opsiyonel: ilk kurulumda admin yaratmak istersen
    public string? Role { get; set; } // null gelirse "User" kabul ederiz
}
