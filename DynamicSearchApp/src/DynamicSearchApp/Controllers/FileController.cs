using DynamicSearchApp.Services;
using DynamicSearchApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSearchApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class FileController : ControllerBase
{
    private readonly FileService _fileService;

    public FileController(FileService fileService)
    {
        _fileService = fileService;
    }

    [HttpGet("{index}")]
    public IActionResult GetFile(int index)
    {
        var file = _fileService.GetFileByIndex(index);
        var actualPath = Path.Combine(_fileService.GetBasePath(), (index / 1000).ToString(), file.Name);
        if (!System.IO.File.Exists(actualPath))
        {
            return NotFound();
        }

        var stream = new FileStream(actualPath, FileMode.Open, FileAccess.Read);
        var contentType = file.GetInferredType() switch
        {
            "tiff" => "image/tiff",
            "pdf" => "application/pdf",
            "html" => "text/html",
            _ => "application/octet-stream"
        };
        return File(stream, contentType, file.Name);
    }
}
