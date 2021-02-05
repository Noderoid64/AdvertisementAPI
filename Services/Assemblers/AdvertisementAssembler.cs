using System;
using AdvertisingApi.Helpers;
using AdvertisingApi.Model;
using AdvertisingApi.Model.Dto;

namespace AdvertisingApi.Services
{
    public class AdvertisementAssembler
    {
        private readonly TagAssembler _tagAssembler;

        public AdvertisementAssembler(TagAssembler tagAssembler)
        {
            _tagAssembler = tagAssembler;
        }
        public Advertisement Assemble(AdvertisementDto dto)
        {
            Assert.IsNotBlank(dto.AdType.ToString());
            Assert.IsNotBlank(dto.Category);
            Assert.IsNotBlank(dto.Content);
            Assert.IsNotBlank(dto.Cost);

            Advertisement result = new Advertisement();
            
            Enum.TryParse(dto.AdType.ToString(), out AdType adType);
            result.AdType = adType;
            result.Category = new Category(dto.Category);
            result.Cost = decimal.Parse(dto.Cost);
            result.Content = dto.Content;

            result.Tags = _tagAssembler.Assemble(dto.Tags);

            return result;
        }
    }
}