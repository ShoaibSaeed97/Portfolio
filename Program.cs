using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// ≈÷«›… œ⁄„ «· —Ã„… Ê«··€« 
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en"),
        new CultureInfo("ar")
    };

    options.DefaultRequestCulture = new RequestCulture("ar");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

// ≈÷«›… ’›Õ«  Razor „⁄ œ⁄„ «· —Ã„… ›Ì «·Ê«ÃÂ« 
builder.Services.AddRazorPages()
    .AddViewLocalization();

// ≈÷«›… Œœ„«  MVC
builder.Services.AddControllersWithViews();

//  ⁄ÌÌ‰ ⁄‰Ê«‰ URL ··«” „«⁄ „⁄ ﬁ—«¡… „ €Ì— «·»Ì∆… PORT (√Ê «·„‰›– 5000 ﬂ«› —«÷Ì)
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
builder.WebHost.UseUrls($"http://*:{port}");

var app = builder.Build();

// Middleware · ⁄—Ì› «··€… »‰«¡ ⁄·Ï «” ⁄·«„ URL ÊÕ›ŸÂ ›Ì «·ﬂÊﬂÌ“
app.Use(async (context, next) =>
{
    var cultureQuery = context.Request.Query["culture"];
    if (!string.IsNullOrWhiteSpace(cultureQuery))
    {
        var culture = new CultureInfo(cultureQuery);
        context.Response.Cookies.Append(
            CookieRequestCultureProvider.DefaultCookieName,
            CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
            new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
        );
    }

    await next();
});

//  ›⁄Ì· ≈⁄œ«œ«  «· —Ã„… Ê«··€« 
var locOptions = app.Services.GetRequiredService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(locOptions.Value);

//  ﬂÊÌ‰ „”«— «·ÿ·»«  Ê«· ⁄«„· „⁄ «·√Œÿ«¡
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

// ≈–« ·œÌﬂ MapStaticAssets ﬂ«„ œ«œ Œ«’°  √ﬂœ „‰ ÊÃÊœÂ° √Ê «Õ–›Â ≈‰ ·„ Ìﬂ‰ „” Œœ„«
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
