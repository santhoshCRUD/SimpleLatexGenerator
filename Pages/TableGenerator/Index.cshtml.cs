using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

public class TableGeneratorModel : PageModel
{
    [BindProperty] public string JsonTableData { get; set; }
    public string GeneratedCode { get; set; }

    public void OnPost()
    {
        var table = JsonSerializer.Deserialize<Dictionary<string, Dictionary<string, string>>>(JsonTableData);

        int rows = table.Count;
        int cols = table["0"].Count;

        string alignment = Request.Form["alignment"];
        string border = Request.Form["border"];

        string columnFormat = new string(alignment[0], cols);

        if (border == "line")
            columnFormat = "|" + String.Join("|", columnFormat.ToCharArray()) + "|";

        string latex = $"\\begin{{tabular}}{{{columnFormat}}}\n";

        if (border == "line") latex += "\\hline\n";

        foreach (var row in table)
        {
            string line = "";
            foreach (var col in row.Value)
                line += col.Value + " & ";

            line = line.TrimEnd('&', ' ');
            latex += line + " \\\\ \n";

            if (border == "line") latex += "\\hline\n";
        }

        latex += "\\end{tabular}";

        GeneratedCode = latex;
    }
}
