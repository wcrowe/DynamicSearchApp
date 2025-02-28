using DynamicSearchApp.Models;

namespace DynamicSearchApp.Services;

public class FileService
{
    private readonly string _baseNetworkPath;

    public FileService(IConfiguration configuration)
    {
        _baseNetworkPath = configuration["FileStorage:NetworkPath"] ?? @"\\networkdrive\files";
    }

    // Add this method
    public string GetBasePath()
    {
        return _baseNetworkPath;
    }

    public FileItem GetFileByIndex(int index)
    {
        int category = GetRangeCategory(index);
        string folderPath = Path.Combine(_baseNetworkPath, category.ToString());
        string fileNameWithoutExtension = index.ToString();
        string[] extensions = { ".tif", ".pdf", ".html" };

        foreach (var ext in extensions)
        {
            string potentialFilePath = Path.Combine(folderPath, fileNameWithoutExtension + ext);
            if (File.Exists(potentialFilePath))
            {
                return new FileItem
                {
                    Index = index,
                    Name = fileNameWithoutExtension + ext,
                    Url = $"/api/file/{index}"
                };
            }
        }

        return new FileItem
        {
            Index = index,
            Name = fileNameWithoutExtension + ".tif",
            Url = $"/api/file/{index}"
        };
    }

    private int GetRangeCategory(int input)
    {
        if (input < 1) return 0;
        if (input <= 999) return 0;
        return (input - 1000) / 1000 + 1;
    }
}
