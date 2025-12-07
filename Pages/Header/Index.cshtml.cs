using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class HeaderModel : PageModel
{
    [BindProperty] public string HeaderText { get; set; }
    [BindProperty] public string TextSize { get; set; }
    [BindProperty] public string TextColor { get; set; }
    [BindProperty] public string Alignment { get; set; }

    public string GeneratedCode { get; set; }

    public void OnPost()
    {
        string alignCmd = Alignment.ToLower() switch
        {
            "left" => "\\lhead",
            "center" => "\\chead",
            "right" => "\\rhead",
            _ => "\\chead"
        };

        string sizeCmd = TextSize.ToLower() switch
        {
            "small" => "\\small ",
            "medium" => "\\normalsize ",
            "large" => "\\Large ",
            _ => "\\normalsize "
        };

        GeneratedCode =
@$"\usepackage{{fancyhdr}}
\pagestyle{{fancy}}
{alignCmd}{{{{\color[HTML]{{{TextColor.Replace("#", "")}}}{sizeCmd}{HeaderText}}}}}";
    }
}
