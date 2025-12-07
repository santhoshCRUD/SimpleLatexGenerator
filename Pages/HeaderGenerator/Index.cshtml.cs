using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class HeaderGeneratorModel : PageModel
{
    [BindProperty]
    public string HeaderText { get; set; }

    [BindProperty]
    public string Size { get; set; }

    [BindProperty]
    public string Color { get; set; }

    [BindProperty]
    public string Alignment { get; set; }

    public string GeneratedLatex { get; set; }

    public void OnGet() { }

    public void OnPost()
    {
        GeneratedLatex = GenerateLatex(HeaderText, Size, Color, Alignment);
    }

    private string GenerateLatex(string text, string size, string color, string alignment)
    {
        string colorWrapped = string.IsNullOrWhiteSpace(color)
            ? text
            : $"\\textcolor{{{color}}}{{{text}}}";

        return
$@"\usepackage{{fancyhdr}}
\pagestyle{{fancy}}
\fancyhead{{}}
\fancyhead[C]{{ {alignment} {{\{size} {colorWrapped}}} }}";
    }
}
