using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace src.DAL
{
    public class LinksRepository : ILinksRepository
    {
        private readonly ILinksCtx _ctx;
        public LinksRepository(ILinksCtx ctx)
        {
            _ctx = ctx;
        }
        
        public async Task<IEnumerable<Link>> GetAllLinks()
        {
            return await _ctx.Links.Find(_ => true).ToListAsync();
        }
        
        public async Task<IEnumerable<Link>> GetUserLinks(string userId)
        {
            return await _ctx.Links.Find(l => l.UserId == userId).ToListAsync();
        }
        
        public async Task<Link> GetLinkById(string id)
        {
            var filter = Builders<Link>.Filter.Eq(l => l.ShortLink, id);
            return await _ctx
                .Links
                .Find(filter)
                .FirstOrDefaultAsync();
        }

        public async Task AddLink(Link link)
        {
            await _ctx.Links.InsertOneAsync(link);
        }

        public async Task<bool> UpdateLinkHitCount(Link link)
        {
            var filter = Builders<Link>.Filter.Eq(l => l.ShortLink, link.ShortLink);
            var result = await _ctx.Links.UpdateOneAsync(
                filter,
                new BsonDocument("$inc", new BsonDocument("HitCount", 1)));
            
            return result.IsAcknowledged && result.ModifiedCount > 0; 
        }
    }
}