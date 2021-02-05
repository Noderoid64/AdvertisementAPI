using System.Collections.Generic;

namespace AdvertisingApi.Model.Dto
{
    public class AdvertisementDto
    {
        public AdType AdType { get; set; }
        public string Category { get; set; }
        public string Cost { get; set; }
        public string Content { get; set; }
        public ICollection<string> Tags { get; set; }
    }
}