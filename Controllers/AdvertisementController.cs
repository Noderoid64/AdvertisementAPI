using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisingApi.Helpers;
using AdvertisingApi.Model;
using AdvertisingApi.Model.Dto;
using AdvertisingApi.Presistance.DAO;
using AdvertisingApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisingApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdvertisementController: Controller
    {
        private readonly AdvertisementAssembler _advertisementAssembler;
        private readonly AdvertisementDao _advertisementDao;
        private readonly StatisticDao _statisticDao;

        public AdvertisementController(
            AdvertisementAssembler advertisementAssembler,
            AdvertisementDao advertisementDao,
            StatisticDao statisticDao
            )
        {
            _advertisementAssembler = advertisementAssembler;
            _advertisementDao = advertisementDao;
            _statisticDao = statisticDao;
        }
        
        [HttpPut("add")]
        public async Task<IActionResult> AddAdvertisement(AdvertisementDto advertisement)
        {
            IActionResult result = Ok();
            try
            {
                Advertisement advertisementToAdd = _advertisementAssembler.Assemble(advertisement);
                await _advertisementDao.SaveAdvertisementAsync(advertisementToAdd);
            }
            catch (Exception e)
            {
                result = Problem(e.Message);
            }
            return result;
        }

        [HttpGet("next")]
        public Task<Advertisement> GetNextAdd()
        {
            return _advertisementDao.GetNextAddAsync();
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAdd(string type, string category, [FromQuery(Name = "tags")] string [] tags)
        {
            IActionResult result;
            try
            {
                AdType.TryParse(type, out AdType adType);
                Assert.IsNotBlank(category);
                Advertisement add = await _advertisementDao.GetRelevantAsync(adType, category, tags);
                if (add != null)
                {
                    result = new JsonResult(add);
                }
                else
                {
                    result = BadRequest("Record does not exist");
                }
                
            }
            catch (Exception e)
            {
                result = Problem(e.Message);
            }

            return result;
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteAd(string id)
        {
            IActionResult result = Ok();
            try
            {
                long.TryParse(id, out long longId);
                await _advertisementDao.DeleteAsync(longId);
            }
            catch (Exception e)
            {
                result = Problem(e.Message);
            }

            return result;
        }

        [HttpGet("statistic")]
        public async Task<dynamic> GetStatistic()
        {
            var adTypeViews = await _statisticDao.GetAllViewsByTypeAsync();
            var mostViewedPosts = await _statisticDao.GetTop10PostsByViews();
            var mostPopularTags = await _statisticDao.GetTop15TagsByViews();
            var topCategories = _statisticDao.GetTop3Categories();
            return new
            {
                AdTypeViews = adTypeViews,
                MostViewedPosts = mostViewedPosts,
                MostPopularTags = mostPopularTags,
                TopCategories = topCategories
            };
        }
    }
}