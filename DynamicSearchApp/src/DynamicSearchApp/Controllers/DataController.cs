using DynamicSearchApp.Services;
using DynamicSearchApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSearchApp.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DataController : ControllerBase
{
    private readonly DataService _dataService;

    public DataController(DataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet("tables")]
    public async Task<IActionResult> GetTableNames()
    {
        try
        {
            var tables = await _dataService.GetTableNames();
            return Ok(tables);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    [HttpGet("schema")]
    public async Task<IActionResult> GetTableSchema(string tableName)
    {
        var columns = await _dataService.GetTableSchema(tableName);
        return Ok(columns);
    }

    [HttpGet("search")]
    public async Task<IActionResult> SearchTable(string tableName, string searchColumn, string searchValue)
    {
        var results = await _dataService.SearchTable(tableName, searchColumn, searchValue);
        return Ok(results);
    }
}
