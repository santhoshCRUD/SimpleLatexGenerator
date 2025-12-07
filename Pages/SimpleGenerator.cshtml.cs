using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class SimpleGeneratorModel : PageModel
{
    [BindProperty]
    public string? UserText { get; set; }

    public string? GeneratedLatex { get; set; }

    public void OnGet()
    {
        // initial load
    }

    public void OnPost()
    {
        if (!string.IsNullOrWhiteSpace(UserText))
        {
            // Generate a very simple LaTeX code
            GeneratedLatex = $"\\textbf{{{UserText}}}";
        }
    }
}
