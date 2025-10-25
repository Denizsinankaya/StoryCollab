using StoryCollab.AdminMvc.Services;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();

// Session (JWT token'ý burada tutacaðýz)
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(2);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpContext'e eriþim
builder.Services.AddHttpContextAccessor();

// ApiClient + HttpClient
// Not: API base url'i appsettings.json'dan da verebilirsin (aþaðýya bak)
var apiBaseUrl = builder.Configuration["Api:BaseUrl"] ?? "https://localhost:7112/api/";
builder.Services.AddHttpClient<ApiClient>(c =>
{
    c.BaseAddress = new Uri(apiBaseUrl);
});

var app = builder.Build();

// Hata yakalama / HSTS
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session middleware (Auth'dan önce olmalý)
app.UseSession();

app.UseAuthentication();

// (Ýleride cookie auth eklersek buraya app.UseAuthentication() gelebilir)
app.UseAuthorization();

// Varsayýlan rota: Auth/Login
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();
