namespace Play.Catalog.Service.Settings
{
    public class MongoDBSettings {
    // public string ConnectionURI { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionName { get; set; } = null!;
    public string Host { get; set; } = "localhost";
    public string Port { get; set; } = "27017";
    public string ConnectionString 
        { 
            get
        {
            // return $"mongodb://{Host}:{Port}";
            return $"mongodb://{Host}:{Port}";
        } 
        }
    }
}