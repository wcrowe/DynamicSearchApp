namespace DynamicSearchApp.Models;

public class SearchField
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
    public string StringValue { get; set; } = "";
    public DateTime? DateValue { get; set; }
}
