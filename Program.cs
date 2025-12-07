using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text;
using System.Text.Json;

// Create builder
var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(4);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
var app = builder.Build();

// Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseStaticFiles();
app.UseRouting();
app.UseSession();   // <-- enable session

app.MapRazorPages();

// --------------------- Snippets API ------------------------

// Add snippet
app.MapPost("/api/snippets/add", async (HttpContext ctx) =>
{
    using var reader = new StreamReader(ctx.Request.Body);
    var content = await reader.ReadToEndAsync();
    var listJson = ctx.Session.GetString("snips");
    List<string> snippets = string.IsNullOrEmpty(listJson)
        ? new List<string>()
        : JsonSerializer.Deserialize<List<string>>(listJson);

    snippets.Add(content);
    ctx.Session.SetString("snips", JsonSerializer.Serialize(snippets));
    return Results.Ok();
});

// List all snippets
app.MapGet("/api/snippets/list", (HttpContext ctx) =>
{
    var listJson = ctx.Session.GetString("snips");
    List<string> snippets = string.IsNullOrEmpty(listJson)
        ? new List<string>()
        : JsonSerializer.Deserialize<List<string>>(listJson);

    return Results.Json(snippets);
});

// Remove snippet by index
app.MapPost("/api/snippets/remove/{idx:int}", (HttpContext ctx, int idx) =>
{
    var listJson = ctx.Session.GetString("snips");
    if (string.IsNullOrEmpty(listJson)) return Results.Ok();

    var snippets = JsonSerializer.Deserialize<List<string>>(listJson);
    if (idx >= 0 && idx < snippets.Count) snippets.RemoveAt(idx);

    ctx.Session.SetString("snips", JsonSerializer.Serialize(snippets));
    return Results.Ok();
});

// Clear all snippets
app.MapPost("/api/snippets/clear", (HttpContext ctx) =>
{
    ctx.Session.Remove("snips");
    return Results.Ok();
});

// --------------------- Export .tex Download ------------------------
app.MapPost("/Export/Download", (HttpContext ctx) =>
{
    var listJson = ctx.Session.GetString("snips");
    List<string> snippets = string.IsNullOrEmpty(listJson)
        ? new List<string>()
        : JsonSerializer.Deserialize<List<string>>(listJson);

    var sb = new StringBuilder();
    sb.AppendLine("\\documentclass[12pt]{article}");
    sb.AppendLine("\\usepackage{amsmath}");
    sb.AppendLine("\\usepackage{xcolor}");
    sb.AppendLine("\\usepackage{graphicx}");
    sb.AppendLine("\\usepackage{fancyhdr}");
    sb.AppendLine("\\begin{document}");
    sb.AppendLine();

    foreach (var s in snippets)
    {
        sb.AppendLine(s);
        sb.AppendLine();
    }

    sb.AppendLine("\\end{document}");

    var bytes = Encoding.UTF8.GetBytes(sb.ToString());
    return Results.File(bytes, "application/x-tex", "document.tex");
});

// Run the app
app.Run();
