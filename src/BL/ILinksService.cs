using System.Collections.Generic;
using System.Threading.Tasks;
using src.DAL;

namespace src.BL
{
    public interface ILinksService
    {
        Task<Link> GetLinkById(string id);
        Task CreateLink(Link link);
        Task AddHitToLink(Link link);
        Task<IEnumerable<Link>> GetLinks(string userId = null);
    }
}