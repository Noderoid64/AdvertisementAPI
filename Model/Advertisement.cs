using System;
using System.Collections.Generic;

namespace AdvertisingApi.Model
{
    public class Advertisement
    {
        public long AdvertisementId { get; set; }
        public AdType AdType { get; set; }
        public decimal Cost { get; set; }
        public string Content { get; set; }
        public long Views { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}