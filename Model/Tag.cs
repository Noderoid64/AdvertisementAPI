using System.Collections.Generic;

namespace AdvertisingApi.Model
{
    public class Tag
    {
        public long TagId { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Advertisement> Advertisements { get; set; }

        public Tag()
        {
            
        }

        public Tag (string title)
        {
            Title = title;
        }
    }
}