using DynamicSearchApp.Models;
using Fluxor;

namespace DynamicSearchApp.State;

public static class SearchReducer
{
    [ReducerMethod]
    public static SearchState ReduceLoadTablesAction(SearchState state, LoadTablesAction action)
    {
        return new SearchState(action.Tables, state.SelectedTable, state.Columns, state.SearchFields, state.SearchResults, state.ErrorMessage);
    }

    [ReducerMethod]
    public static SearchState ReduceSelectTableAction(SearchState state, SelectTableAction action)
    {
        return new SearchState(state.Tables, action.TableName, new List<ColumnSchema>(), new List<SearchField>(), null, "");
    }

    [ReducerMethod]
    public static SearchState ReduceLoadSchemaAction(SearchState state, LoadSchemaAction action)
    {
        var searchFields = action.Columns.Select(c => new SearchField
        {
            ColumnName = c.ColumnName,
            DataType = c.DataType,
            StringValue = "",
            DateValue = null
        }).ToList();
        return new SearchState(state.Tables, state.SelectedTable, action.Columns, searchFields, null, "");
    }

    [ReducerMethod]
    public static SearchState ReduceUpdateSearchFieldAction(SearchState state, UpdateSearchFieldAction action)
    {
        var newSearchFields = state.SearchFields.ToList();
        newSearchFields[action.Index] = new SearchField
        {
            ColumnName = state.SearchFields[action.Index].ColumnName,
            DataType = state.SearchFields[action.Index].DataType,
            StringValue = action.StringValue,
            DateValue = action.DateValue
        };
        return new SearchState(state.Tables, state.SelectedTable, state.Columns, newSearchFields, state.SearchResults, state.ErrorMessage);
    }

    [ReducerMethod]
    public static SearchState ReduceSearchAction(SearchState state, SearchAction action)
    {
        return new SearchState(state.Tables, state.SelectedTable, state.Columns, state.SearchFields, action.Results, state.ErrorMessage);
    }

    [ReducerMethod]
    public static SearchState ReduceSetErrorAction(SearchState state, SetErrorAction action)
    {
        return new SearchState(state.Tables, state.SelectedTable, state.Columns, state.SearchFields, state.SearchResults, action.ErrorMessage);
    }

    [ReducerMethod]
    public static SearchState ReduceClearResultsAction(SearchState state, ClearResultsAction action)
    {
        return new SearchState(state.Tables, state.SelectedTable, state.Columns, state.SearchFields, null, state.ErrorMessage);
    }
}
