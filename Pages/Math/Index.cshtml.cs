using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

public class MathModel : PageModel
{
    [BindProperty] public string FinalCode { get; set; }
    public string GeneratedCode { get; set; }

    public void OnPost()
    {
        GeneratedCode = FinalCode;
    }
}
