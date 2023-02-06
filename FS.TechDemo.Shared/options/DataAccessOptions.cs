namespace FS.TechDemo.Shared.options;

public class DataAccessOptions
{
    public const string Database = "Database";

    public DatabaseOptions Db { get; set; } = new();

    public class DatabaseOptions
    {
        public string ConnectionString { get; set; } = "";
    }
}