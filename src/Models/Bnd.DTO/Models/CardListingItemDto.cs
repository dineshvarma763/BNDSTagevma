using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class CardListingItemDto
    {
        public string? ListingLink { get; set; }
        public string? ListingTitle { get; set; }
        public string? ListingSubTitle { get; set; }
        public string? ListingDescription { get; set; }
        public ImageDto? ListingMobileImage { get; set; } = new ImageDto();
        public ImageDto? ListingDesktopImage { get; set; } = new ImageDto();
        public DateTime ListingCreatedDate { get; set; }
        public bool IsVideoPage { get; set; }
        public IEnumerable<string>? Topics { get; set; }
        public IEnumerable<string>? ContentTypes { get; set; }
        public string? DateTimeFormatted
        {
            get
            { 
                var result = string.Empty;

                if(ListingCreatedDate != DateTime.MinValue)
                {
                    result = ListingCreatedDate.ToString("MMMM yyyy");
                }

                return result;
            }
        }
        public string? TagsConfig
        {
            get
            {
                var topic = new List<string>();
                var contentType = new List<string>();
                if(Topics != null && Topics.Any())
                {
                    topic = Topics.ToList();
                }
                if(ContentTypes != null && ContentTypes.Any())
                {
                    contentType = ContentTypes.ToList();
                }

                var resultObject = new
                {
                    tags = topic.Concat(contentType),
                };
                string? result = JsonConvert.SerializeObject(resultObject);
                return result;
            }
        }
    }
}
