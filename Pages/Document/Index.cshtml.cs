using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

public class DocumentTemplateModel : PageModel
{
    // Document settings
    [BindProperty] public string DocClass { get; set; }
    [BindProperty] public string FontSize { get; set; }
    [BindProperty] public string PaperSize { get; set; }

    // Packages
    [BindProperty] public bool PkgGeometry { get; set; }
    [BindProperty] public bool PkgAmsmath { get; set; }
    [BindProperty] public bool PkgGraphicx { get; set; }
    [BindProperty] public bool PkgHyperref { get; set; }
    [BindProperty] public bool PkgXcolor { get; set; }

    // Document info
    [BindProperty] public string Title { get; set; }
    [BindProperty] public string Author { get; set; }
    [BindProperty] public string Date { get; set; }

    // Sections
    [BindProperty] public string JsonSections { get; set; }

    public string GeneratedCode { get; set; }

    public void OnPost()
    {
        // Build package list
        List<string> packages = new();

        if (PkgGeometry) packages.Add("\\usepackage{geometry}");
        if (PkgAmsmath) packages.Add("\\usepackage{amsmath}");
        if (PkgGraphicx) packages.Add("\\usepackage{graphicx}");
        if (PkgHyperref) packages.Add("\\usepackage{hyperref}");
        if (PkgXcolor) packages.Add("\\usepackage{xcolor}");

        string pkgText = string.Join("\n", packages);

        // Date
        string latexDate = string.IsNullOrEmpty(Date)
            ? "\\today"
            : DateTime.Parse(Date).ToString("dd MMMM yyyy");

        // Sections
        var sections = JsonSerializer.Deserialize<List<Section>>(JsonSections);

        string secText = "";
        foreach (var sec in sections)
        {
            secText += $"\\section*{{{sec.Title}}}\n{sec.Content}\n\n";
        }

        // Build full document
        GeneratedCode =
$@"\documentclass[{FontSize}, {PaperSize}]{{{DocClass}}}

{pkgText}

\title{{{Title}}}
\author{{{Author}}}
\date{{{latexDate}}}

\begin{{document}}
\maketitle

{secText}

\end{{document}}";
    }

    public class Section
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
