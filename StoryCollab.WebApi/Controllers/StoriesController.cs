using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StoryCollab.Domain.Entities;
using StoryCollab.Infrastructure.Data;
using StoryCollab.WebApi.Models;

namespace StoryCollab.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize] // sadece giriş yapan kullanıcılar
public class StoriesController : ControllerBase
{
    private readonly StoryDbContext _db;

    public StoriesController(StoryDbContext db)
    {
        _db = db;
    }

    // Yardımcı: giriş yapan kullanıcının ID'sini al
    private int GetUserId() =>
        int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // POST /api/stories
    [HttpPost]
    public async Task<IActionResult> CreateStory([FromBody] CreateStoryRequest req)
    {
        var story = new Story
        {
            Title = req.Title,
            Description = req.Description,
            OwnerId = GetUserId()
        };

        _db.Stories.Add(story);
        await _db.SaveChangesAsync();

        return Ok(new { story.Id, story.Title });
    }

    // GET /api/stories/mine
    [HttpGet("mine")]
    public async Task<IActionResult> GetMyStories()
    {
        var userId = GetUserId();
        var stories = await _db.Stories
            .Where(s => s.OwnerId == userId || s.Contributors.Any(c => c.UserId == userId))
            .Select(s => new { s.Id, s.Title, s.Description, s.CreatedAt })
            .ToListAsync();

        return Ok(stories);
    }

    // POST /api/stories/{id}/contributors
    [HttpPost("{id}/contributors")]
    public async Task<IActionResult> AddContributor(int id, [FromBody] AddContributorRequest req)
    {
        var story = await _db.Stories.FindAsync(id);
        if (story == null)
            return NotFound("Story not found");

        // sadece owner ekleyebilsin
        if (story.OwnerId != GetUserId())
            return Forbid("Only owner can add contributors.");

        // kullanıcı var mı
        var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
        if (user == null)
            return NotFound("User not found");

        // zaten katkıcı mı?
        bool exists = await _db.Set<StoryContributor>()
            .AnyAsync(sc => sc.UserId == user.Id && sc.StoryId == id);
        if (exists)
            return Conflict("User already a contributor.");

        _db.Set<StoryContributor>().Add(new StoryContributor
        {
            UserId = user.Id,
            StoryId = id,
            Role = req.Role ?? "Contributor"
        });

        await _db.SaveChangesAsync();
        return Ok(new { message = "Contributor added" });
    }
}
