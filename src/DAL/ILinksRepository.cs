using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace src.DAL
{
    public interface ILinksRepository
    {
        Task<IEnumerable<Link>> GetAllLinks();
        Task<IEnumerable<Link>> GetUserLinks(string userId);
        Task<Link> GetLinkById(string id);
        Task AddLink(Link link);
        Task<bool> UpdateLinkHitCount(Link link);
    }
}