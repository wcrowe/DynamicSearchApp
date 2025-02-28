namespace DynamicSearchApp.Models;

public class FileItem
{
    public int Index { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Url { get; set; } = string.Empty;

    public string GetInferredType()
    {
        if (string.IsNullOrEmpty(Name)) return "unknown";
        var extension = Path.GetExtension(Name).ToLowerInvariant();
        return extension switch
        {
            ".tiff" or ".tif" => "tiff",
            ".pdf" => "pdf",
            ".html" or ".htm" => "html",
            _ => "unknown"
        };
    }
}
