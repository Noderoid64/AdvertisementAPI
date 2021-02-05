using System.Collections.Generic;
using System.Linq;
using AdvertisingApi.Model;

namespace AdvertisingApi.Services
{
    public class TagAssembler
    {
        public ICollection<Tag> Assemble(ICollection<string> tagsDto)
        {
            List<Tag> tags = new List<Tag>();
            if (tagsDto != null)
            {
                tags.AddRange(tagsDto.Select(t => new Tag(t)));
            }

            return tags;
        }
    }
}