using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class ProductListingItemDto
    {
        public string? ListingLink { get; set; }
        public string? ListingTitle { get; set; }
        public string? ListingSubTitle { get; set; }
        public string? ListingDescription { get; set; }
        public ImageDto? ListingMobileImage { get; set; } = new ImageDto();
        public ImageDto? ListingDesktopImage { get; set; } = new ImageDto();
        public DateTime ListingCreatedDate { get; set; }
        public bool IsVideoPage { get; set; }
        public IEnumerable<string>? ProductCategories { get; set; }
        public IEnumerable<string>? ProductFilters { get; set; }
        public double? ProductPopularity { get; set; }
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
                var filters = new List<string>();
                var categories = new List<string>();
                if(ProductFilters is not null && ProductFilters.Any())
                {
                    filters = ProductFilters.ToList();
                }
                if(ProductCategories is not null && ProductCategories.Any())
                {
                    categories = ProductCategories.ToList();
                }

                var resultObject = new
                {
                    tags = filters.Concat(categories)
                };
                string? result = JsonConvert.SerializeObject(resultObject);
                return result;
            }
        }
    }
}
