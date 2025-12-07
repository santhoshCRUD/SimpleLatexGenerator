using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class GenerateModel : PageModel
{
    [BindProperty]
    public IFormFile? LatexFile { get; set; }

    [BindProperty]
    public string SelectedTool { get; set; } = "Header";

    public IActionResult OnPost()
    {
        // If user uploaded a file, save it temporarily
        if (LatexFile != null)
        {
            var filePath = Path.Combine("wwwroot/uploads", LatexFile.FileName);
            Directory.CreateDirectory("wwwroot/uploads");

            using var stream = System.IO.File.Create(filePath);
            LatexFile.CopyTo(stream);

            TempData["UploadedFilePath"] = filePath;
        }

        // Redirect to selected generator
        return SelectedTool switch
        {
            "Header" => RedirectToPage("/HeaderGenerator"),
            "TitlePage" => RedirectToPage("/TitlePageGenerator"),
            "Table" => RedirectToPage("/TableGenerator"),
            "Math" => RedirectToPage("/MathGenerator"),
            "FullDocument" => RedirectToPage("/DocumentGenerator"),
            _ => RedirectToPage("/Index"),
        };
    }
}
