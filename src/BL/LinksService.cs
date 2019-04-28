using System.Threading.Tasks;
using src.DAL;

namespace src.BL
{
    public class LinksService : ILinksService
    {
        public Task CreateLink(Link link)
        {
            throw new System.NotImplementedException();
        }

        public Task AddHitToLink(Link link)
        {
            throw new System.NotImplementedException();
        }

        public Task<Link> GetLinks(string userId = null)
        {
            throw new System.NotImplementedException();
        }
    }
}