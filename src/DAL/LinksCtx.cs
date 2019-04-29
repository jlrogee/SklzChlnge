using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace src.DAL
{
    public class LinksCtx : ILinksCtx
    {
        private readonly IMongoDatabase _database;
        
        public LinksCtx(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Link> Links => _database.GetCollection<Link>("Links");
    }
}