using System.Threading.Tasks;
using src.DAL;

namespace src.BL
{
    public interface ILinksService
    {
        Task CreateLink(Link link);
        Task AddHitToLink(Link link);
        
        Task<Link> GetLinks(string userId = null);
    }
}