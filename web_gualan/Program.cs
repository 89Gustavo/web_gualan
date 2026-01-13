using web_gualan.Services;
using Microsoft.AspNetCore.Http.Features;

var builder = WebApplication.CreateBuilder(args);

// Razor Pages
builder.Services.AddRazorPages();

// HttpClient + ApiService
builder.Services.AddHttpClient<ApiService>();

// Session
builder.Services.AddSession();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UrlService>();

// 🔥 PERMITIR ARCHIVOS GRANDES (1 GB)
builder.Services.Configure<FormOptions>(options =>
{
    options.MultipartBodyLengthLimit = 512L * 1024L * 1024L; // 512 MB
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.UseSession();

app.MapRazorPages();
app.Run();
