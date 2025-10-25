using Microsoft.AspNetCore.Mvc;
using StoryCollab.AdminMvc.Models;
using StoryCollab.AdminMvc.Services;
using StoryCollab.WebApi.Models;
using System.Text.Json;
using LoginRequest = StoryCollab.AdminMvc.Models.LoginRequest;
using AuthResponse = StoryCollab.AdminMvc.Models.AuthResponse;

namespace StoryCollab.AdminMvc.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApiClient _api;

        public AuthController(ApiClient api)
        {
            _api = api;
        }

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequest model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Login isteği: POST /api/auth/login
            var response = await _api.PostJsonAsync("auth/login", model);

            if (response == null)
            {
                ViewBag.Error = "Invalid credentials or server error.";
                return View(model);
            }

            // Gelen JSON'u AuthResponse modeline deserialize et
            var auth = JsonSerializer.Deserialize<AuthResponse>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (auth == null || string.IsNullOrEmpty(auth.AccessToken))
            {
                ViewBag.Error = "Login failed.";
                return View(model);
            }

            // token'ı Session'a yaz
            HttpContext.Session.SetString("AccessToken", auth.AccessToken);

            return RedirectToAction("Index", "Stories");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }
}
