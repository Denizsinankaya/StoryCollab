using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace StoryCollab.AdminMvc.Services;

public class ApiClient
{
    private readonly HttpClient _http;
    private readonly IHttpContextAccessor _ctx;
    private readonly JsonSerializerOptions _opts = new() { PropertyNameCaseInsensitive = true };

    public ApiClient(HttpClient http, IHttpContextAccessor ctx)
    {
        _http = http;
        _ctx = ctx;
    }

    public void SetAuthHeader()
    {
        var token = _ctx.HttpContext?.Session.GetString("AccessToken");
        if (!string.IsNullOrEmpty(token))
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
    }

    public async Task<T?> GetAsync<T>(string url)
    {
        SetAuthHeader();
        var res = await _http.GetAsync(url);
        if (!res.IsSuccessStatusCode) return default;
        var json = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json, _opts);
    }

    public async Task<bool> PostAsync(string url, object data)
    {
        SetAuthHeader();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _http.PostAsync(url, content);
        return res.IsSuccessStatusCode;
    }

    public async Task<string?> PostJsonAsync(string url, object data)
    {
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");

        var res = await _http.PostAsync(url, content);

        // API'den dönen yanıtı logla
        if (!res.IsSuccessStatusCode)
        {
            var errorResponse = await res.Content.ReadAsStringAsync();
            Console.WriteLine("API Error: " + errorResponse);  // Log error response
            return null;
        }

        var jsonResponse = await res.Content.ReadAsStringAsync();
        return string.IsNullOrEmpty(jsonResponse) ? null : jsonResponse;
    }



    public async Task<T?> PostAsync<T>(string url, object data)
    {
        SetAuthHeader();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _http.PostAsync(url, content);
        if (!res.IsSuccessStatusCode) return default;
        var body = await res.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(body, _opts);
    }

    public async Task<bool> PutAsync(string url, object data)
    {
        SetAuthHeader();
        var json = JsonSerializer.Serialize(data);
        var content = new StringContent(json, Encoding.UTF8, "application/json");
        var res = await _http.PutAsync(url, content);
        return res.IsSuccessStatusCode;
    }
}
