using DynamicSearchApp.Models;
using Fluxor;

namespace DynamicSearchApp.State;

[FeatureState]
public class SearchState
{
    public List<string> Tables { get; }
    public string SelectedTable { get; }
    public List<ColumnSchema> Columns { get; }
    public List<SearchField> SearchFields { get; }
    public List<Dictionary<string, object>> SearchResults { get; }
    public string ErrorMessage { get; }

    public SearchState(
        List<string> tables,
        string selectedTable,
        List<ColumnSchema> columns,
        List<SearchField> searchFields,
        List<Dictionary<string, object>> searchResults,
        string errorMessage)
    {
        Tables = tables ?? new List<string>();
        SelectedTable = selectedTable ?? "";
        Columns = columns ?? new List<ColumnSchema>();
        SearchFields = searchFields ?? new List<SearchField>();
        SearchResults = searchResults;
        ErrorMessage = errorMessage ?? "";
    }
}
