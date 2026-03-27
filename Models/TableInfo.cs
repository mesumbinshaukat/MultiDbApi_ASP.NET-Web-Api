namespace MultiDbApi.Models
{
    public class TableInfo
    {
        public string TableName { get; set; } = string.Empty;
        public string SchemaName { get; set; } = string.Empty;
        public long RowCounts { get; set; }
    }
}
