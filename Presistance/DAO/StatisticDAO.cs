using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingApi.Presistance.DAO
{
    public class StatisticDao
    {
        private readonly AppDbContext _appDbContext;

        public StatisticDao(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        
        // !important 'dynamic' used only for reducing app complicity by not adding new models for every linq data projection

        public async Task<dynamic> GetAllViewsByTypeAsync()
        {

            return await _appDbContext.Advertisements
                .GroupBy(ad => ad.AdType)
                .Select(g => new {AdType = g.Key, Views = g.Sum(m => m.Views)})
                .ToListAsync();

        }

        public dynamic GetTop3Categories()
        {
            return _appDbContext.Advertisements
                .Include(ad => ad.Category)
                .AsEnumerable()
                .GroupBy(ad => ad.Category.Title)
                .Select(g => new {Category = g.Key, PostCount = g.Count()})
                .OrderBy(g => g.PostCount)
                .Take(3)
                .ToList();
        }

        public async Task<dynamic> GetTop10PostsByViews()
        {
            return await _appDbContext.Advertisements
                .OrderBy(ad => ad.Views)
                .Take(10)
                .Select(ad => new {Post = ad.Content, Views = ad.Views})
                .ToListAsync();
        }
        
        public async Task<dynamic> GetTop15TagsByViews()
        {
            return await _appDbContext.Tags
                .Include(t => t.Advertisements)
                .OrderBy(t => t.Advertisements.Sum(ad => ad.Views))
                .Take(15)
                .Select(t => new {Title = t.Title, Views = t.Advertisements.Sum(ad => ad.Views)})
                .ToListAsync();
        }
    }
}