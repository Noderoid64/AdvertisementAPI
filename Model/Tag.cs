using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AdvertisingApi.Model
{
    public class Tag
    {
        public long TagId { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
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