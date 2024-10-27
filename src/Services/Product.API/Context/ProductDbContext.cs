using MongoDB.Driver;
using MongoRepo.Context;

namespace Product.API.Context
{
    public class ProductDbContext : ApplicationDbContext
    {
        private readonly IMongoDatabase _database;

        static IConfiguration configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
        static string connectionString = configuration.GetConnectionString("Product.API");
        static string databaseName = configuration.GetValue<string>("DatabaseName");
        
        public ProductDbContext() : base(connectionString, databaseName)
        {
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase(databaseName);
        }
        
        public IMongoCollection<Models.Product> Products => _database.GetCollection<Models.Product>("Products");
    }
}
