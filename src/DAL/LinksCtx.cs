using MongoDB.Driver;

namespace src.DAL
{
    public class LinksCtx : ILinksCtx
    {
        public IMongoCollection<Link> Links { get; set; }
    }
}