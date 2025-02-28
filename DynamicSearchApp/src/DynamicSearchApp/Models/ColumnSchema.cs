namespace DynamicSearchApp.Models;

public class ColumnSchema
{
    public string ColumnName { get; set; }
    public string DataType { get; set; }
    public string IsNullable { get; set; }
    public string DisplayName => GetDisplayName(ColumnName);

    private static string GetDisplayName(string columnName)
    {
        return columnName switch
        {
            "policy_number" => "Policy Number",
            "scan_date" => "Scan Date",
            "crated_date" => "Created Date",
            "last_name" => "Last Name",
            "image_index" => "Image Index",
            _ => columnName.Split('_').Select(w => char.ToUpper(w[0]) + w.Substring(1)).Aggregate((a, b) => a + " " + b)
        };
    }
}
