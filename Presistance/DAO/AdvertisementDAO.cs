using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AdvertisingApi.Model;
using Microsoft.EntityFrameworkCore;

namespace AdvertisingApi.Presistance.DAO
{
    public class AdvertisementDAO
    {
        private AppDbContext _appDbContext;

        public AdvertisementDAO(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task SaveAdvertisementAsync(Advertisement add)
        {
            _appDbContext.Add(add);
            return _appDbContext.SaveChangesAsync();
        }

        public async Task<Advertisement> GetNextAddAsync()
        {
            long i = _appDbContext.GetNextQueueSequenceValue();
            Advertisement add = _appDbContext.Advertisements
                .Include(ad => ad.Category)
                .FirstOrDefault(ad => ad.AdvertisementId >= i);
            if (add == null)
            {
                _appDbContext.ResetQueueSequence();
                i = 1;
                add = _appDbContext.Advertisements
                    .Include(ad => ad.Category)
                    .FirstOrDefault(ad => ad.AdvertisementId >= i);
            }
            else if (add.AdvertisementId != i)
            {
                _appDbContext.SetQueueSequenceValue(add.AdvertisementId + 1);
            }
            
            if (add != null)
                add.Views++;
            
            await _appDbContext.SaveChangesAsync();

            return add;
        }

        public async Task<Advertisement> GetRelevantAsync(AdType type, string category, ICollection<string> tags)
        {
            var query = from s in _appDbContext.Advertisements
                    .Include(ad => ad.Category)
                where
                    s.AdType == type &&
                    s.Category.Title == category &&
                    (
                        !tags.Any() ||
                        s.Tags.Any(t => tags.Contains(t.Title))
                    )
                select s;

            Advertisement result = query.FirstOrDefault();
            
            if (result != null)
            {
                result.Views++;
                await _appDbContext.SaveChangesAsync();
            }

            return result;
        }

        public async Task DeleteAsync(long id)
        {
            var obj = await _appDbContext.Advertisements.FirstOrDefaultAsync(ad => ad.AdvertisementId.Equals(id));
            if (obj != null)
            {
                _appDbContext.Advertisements.Remove(obj);
                await _appDbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Object with id: {id} doesn't exist");
            }
        }
    }
}