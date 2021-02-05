using System;
using AdvertisingApi.Helpers;
using AdvertisingApi.Model;
using AdvertisingApi.Model.Dto;

namespace AdvertisingApi.Services
{
    public class AdvertisementAssembler
    {
        private TagAssembler _tagAssembler;

        public AdvertisementAssembler(TagAssembler tagAssembler)
        {
            _tagAssembler = tagAssembler;
        }
        public Advertisement assemble(AdvertisementDto dto)
        {
            Assert.isNotBlank(dto.AdTypeDto.ToString());
            Assert.isNotBlank(dto.Category);
            Assert.isNotBlank(dto.Content);
            Assert.isNotBlank(dto.Cost);

            Advertisement result = new Advertisement();
            
            Enum.TryParse(dto.AdTypeDto.ToString(), out AdType adType);
            result.AdType = adType;
            result.Category = new Category(dto.Category);
            result.Cost = decimal.Parse(dto.Cost);
            result.Content = dto.Content;

            result.Tags = _tagAssembler.Assemble(dto.tags);

            return result;
        }
    }
}