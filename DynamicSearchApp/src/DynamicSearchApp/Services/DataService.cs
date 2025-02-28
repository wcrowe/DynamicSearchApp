using Dapper;
using System.Data;
using DynamicSearchApp.Models;

namespace DynamicSearchApp.Services;

public class DataService
{
    private readonly IDbConnection _dbConnection;

    public DataService(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
    }

    public async Task<List<string>> GetTableNames()
    {
        return (await _dbConnection.QueryAsync<string>("GetTableNames", commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<List<ColumnSchema>> GetTableSchema(string tableName)
    {
        return (await _dbConnection.QueryAsync<ColumnSchema>("GetTableSchema",
            new { TableName = tableName }, commandType: CommandType.StoredProcedure)).ToList();
    }

    public async Task<List<Dictionary<string, object>>> SearchTable(string tableName, string searchColumn, string searchValue)
    {
        var results = await _dbConnection.QueryAsync<dynamic>("SearchTable",
            new { TableName = tableName, SearchColumn = searchColumn, SearchValue = searchValue },
            commandType: CommandType.StoredProcedure);
        return results.Select(r => ((IDictionary<string, object>)r).ToDictionary(kvp => kvp.Key, kvp => kvp.Value)).ToList();
    }
}