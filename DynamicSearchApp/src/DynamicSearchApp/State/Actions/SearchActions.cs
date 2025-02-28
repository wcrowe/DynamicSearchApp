using DynamicSearchApp.Models;

namespace DynamicSearchApp.State;

public record LoadTablesAction(List<string> Tables);
public record SelectTableAction(string TableName);
public record LoadSchemaAction(List<ColumnSchema> Columns);
public record UpdateSearchFieldAction(int Index, string StringValue, DateTime? DateValue);
public record SearchAction(List<Dictionary<string, object>> Results);
public record SetErrorAction(string ErrorMessage);
public record ClearResultsAction;
