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
[Authorize]
public class ChaptersController : ControllerBase
{
    private readonly StoryDbContext _db;
    public ChaptersController(StoryDbContext db) => _db = db;

    private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

    // POST /api/chapters
    [HttpPost]
    public async Task<IActionResult> CreateChapter([FromBody] CreateChapterRequest req)
    {
        var userId = GetUserId();

        // hikaye var mı?
        var story = await _db.Stories
            .Include(s => s.Contributors)
            .FirstOrDefaultAsync(s => s.Id == req.StoryId);

        if (story == null) return NotFound("Story not found.");

        // erişim kontrolü: sadece owner veya contributor
        bool canWrite = story.OwnerId == userId || story.Contributors.Any(c => c.UserId == userId);
        if (!canWrite) return Forbid();

        // önceki versiyon sayısı
        int version = await _db.Chapters
            .CountAsync(c => c.StoryId == req.StoryId && c.Title == req.Title) + 1;

        var chapter = new Chapter
        {
            StoryId = req.StoryId,
            Title = req.Title,
            Content = req.Content,
            AuthorId = userId,
            VersionNumber = version
        };

        _db.Chapters.Add(chapter);
        await _db.SaveChangesAsync();

        return Ok(new { chapter.Id, chapter.VersionNumber });
    }

    // PUT /api/chapters/{id}/publish
    [HttpPut("{id}/publish")]
    public async Task<IActionResult> PublishChapter(int id)
    {
        var chapter = await _db.Chapters.Include(c => c.Story).FirstOrDefaultAsync(c => c.Id == id);
        if (chapter == null) return NotFound("Chapter not found.");

        // sadece owner yayınlayabilir
        var userId = GetUserId();
        if (chapter.Story.OwnerId != userId) return Forbid();

        chapter.IsPublished = true;
        await _db.SaveChangesAsync();

        return Ok(new { message = "Chapter published" });
    }

    // GET /api/chapters/story/{storyId}
    [HttpGet("story/{storyId}")]
    public async Task<IActionResult> GetStoryChapters(int storyId)
    {
        var chapters = await _db.Chapters
            .Where(c => c.StoryId == storyId)
            .OrderBy(c => c.CreatedAt)
            .Select(c => new
            {
                c.Id,
                c.Title,
                c.VersionNumber,
                c.IsPublished,
                c.CreatedAt
            })
            .ToListAsync();

        return Ok(chapters);
    }
}
