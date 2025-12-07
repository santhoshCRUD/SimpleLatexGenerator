using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class TitlePageModel : PageModel
{
    [BindProperty] public string Title { get; set; }
    [BindProperty] public string Subtitle { get; set; }
    [BindProperty] public string Author { get; set; }
    [BindProperty] public string Date { get; set; }
    [BindProperty] public string Alignment { get; set; }

    public string GeneratedCode { get; set; }

    public void OnPost()
    {
        string alignEnvironment = Alignment.ToLower() == "left" ? "flushleft" : "center";

        string dateValue = string.IsNullOrEmpty(Date)
            ? "\\today"
            : DateTime.Parse(Date).ToString("dd MMMM yyyy");

        GeneratedCode =
@$"\begin{{titlepage}}
\begin{{{alignEnvironment}}}
    \vspace*{{3cm}}
    {{\LARGE \textbf{{{Title}}}}} \\
    \vspace{{0.5cm}}
    {{\large {Subtitle}}} \\
    \vspace{{2cm}}
    \textbf{{Author:}} {Author} \\
    \vspace{{1cm}}
    \textbf{{Date:}} {dateValue} \\
\end{{{alignEnvironment}}}
\end{{titlepage}}";
    }
}
