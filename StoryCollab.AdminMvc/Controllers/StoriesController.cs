using Microsoft.AspNetCore.Mvc;
using StoryCollab.AdminMvc.Models;
using StoryCollab.AdminMvc.Services;

namespace StoryCollab.AdminMvc.Controllers;

public class StoriesController : Controller
{
    private readonly ApiClient _api;
    public StoriesController(ApiClient api) => _api = api;

    private bool EnsureLoggedIn()
    {
        var token = HttpContext.Session.GetString("AccessToken");
        if (string.IsNullOrEmpty(token))
        {
            TempData["Error"] = "Please login first.";
            return false;
        }
        return true;
    }

    // GET: /Stories
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");

        var list = await _api.GetAsync<List<StoryListItem>>("stories/mine")
                   ?? new List<StoryListItem>();
        return View(list);
    }

    // GET: /Stories/Create
    [HttpGet]
    public IActionResult Create()
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");
        return View(new CreateStoryVm());
    }

    // POST: /Stories/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(CreateStoryVm vm)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");
        if (!ModelState.IsValid) return View(vm);

        // API: POST /api/stories  (Title, Description)
        var ok = await _api.PostAsync<object>("stories", vm) != null;

        if (ok)
        {
            TempData["Ok"] = "Story created.";
            return RedirectToAction(nameof(Index));
        }
        TempData["Error"] = "Failed to create story.";
        return View(vm);
    }

    // GET: /Stories/AddContributor/5
    [HttpGet]
    public IActionResult AddContributor(int id)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");
        return View(new AddContributorVm { StoryId = id });
    }

    // POST: /Stories/AddContributor
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddContributor(AddContributorVm vm)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");
        if (!ModelState.IsValid) return View(vm);

        // API beklediği body: { email, role }
        var ok = await _api.PostAsync<object>(
            $"stories/{vm.StoryId}/contributors",
            new { email = vm.Email, role = vm.Role }) != null;

        TempData[ok ? "Ok" : "Error"] = ok ? "Contributor added." : "Failed to add contributor.";
        return RedirectToAction(nameof(Index));
    }

    // GET: /Stories/AddChapter/5
    [HttpGet]
    public IActionResult AddChapter(int id)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");
        return View(new CreateChapterVm { StoryId = id });
    }

    // POST: /Stories/AddChapter
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddChapter(CreateChapterVm vm)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");

        // API'ye veri gönder
        var res = await _api.PostJsonAsync("chapters", vm);

        // Eğer res null dönerse, hata mesajını göster
        if (res == null)
        {
            TempData["Error"] = "Failed to create chapter. No response from API.";
        }
        else
        {
            TempData["Ok"] = "Chapter created (draft).";
        }

        return RedirectToAction(nameof(Index));
    }



    // POST: /Stories/PublishChapter/12
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> PublishChapter(int chapterId)
    {
        if (!EnsureLoggedIn()) return RedirectToAction("Login", "Auth");

        // Veri göndermiyorsak, null geçebiliriz
        var ok = await _api.PutAsync($"chapters/{chapterId}/publish", null);

        TempData[ok ? "Ok" : "Error"] = ok ? "Chapter published." : "Failed to publish.";
        return RedirectToAction(nameof(Index));
    }

}
