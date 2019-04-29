using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using src.DAL;

namespace src.BL
{
    public class LinksService : ILinksService
    {
        private readonly ILinksRepository _linksRepository;
        
        public LinksService(ILinksRepository linksRepository)
        {
            _linksRepository = linksRepository;
        }

        public async Task<Link> GetLinkById(string id)
        {
            return await _linksRepository.GetLinkById(id);
        }
        
        public async Task CreateLink(Link link)
        {
            await _linksRepository.AddLink(link);
        }

        public async Task AddHitToLink(Link link)
        {
            await _linksRepository.UpdateLinkHitCount(link);
        }

        public async Task<IEnumerable<Link>> GetLinks(string userId = null)
        {
            if (userId != null)
                return await _linksRepository.GetUserLinks(userId);

            return await _linksRepository.GetAllLinks();

        }
    }
}