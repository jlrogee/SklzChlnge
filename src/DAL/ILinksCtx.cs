using MongoDB.Driver;

namespace src.DAL
{
    public interface ILinksCtx
    {
        IMongoCollection<Link> Links { get; set; }
    }
}