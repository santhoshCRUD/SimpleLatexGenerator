using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;

public class DownloadModel : PageModel
{
    public IActionResult OnPost()
    {
        var snips = HttpContext.Session.GetString("snips");
        var arr = string.IsNullOrEmpty(snips) ? new List<string>() : System.Text.Json.JsonSerializer.Deserialize<List<string>>(snips);

        var sb = new StringBuilder();
        sb.AppendLine("\\documentclass[12pt]{article}");
        sb.AppendLine("\\usepackage{amsmath}");
        sb.AppendLine("\\usepackage{xcolor}");
        sb.AppendLine("\\usepackage{graphicx}");
        sb.AppendLine("\\usepackage{fancyhdr}");
        sb.AppendLine("\\begin{document}");
        sb.AppendLine();

        foreach (var s in arr)
        {
            sb.AppendLine(s);
            sb.AppendLine();
        }

        sb.AppendLine("\\end{document}");

        var bytes = Encoding.UTF8.GetBytes(sb.ToString());
        return File(bytes, "application/x-tex", "document.tex");
    }
}
