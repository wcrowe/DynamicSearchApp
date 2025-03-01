using DynamicSearchApp.Models;
using Fluxor;
using DynamicSearchApp.State;

namespace DynamicSearchApp.State;

public class SearchFeature : Feature<SearchState>
{
    public override string GetName() => "Search";

    protected override SearchState GetInitialState()
    {
        return new SearchState(
            tables: new List<string>(),
            selectedTable: "",
            columns: new List<ColumnSchema>(),
            searchFields: new List<SearchField>(),
            searchResults: null,
            errorMessage: ""
        );
    }
}