namespace Bnd.DTO.Models
{
    using Newtonsoft.Json;

    public class DealerDto
    {
        public int Id { get; set; }
        public ImageDto? Logo { get; set; }
        public string? DealerName { get; set; }
        public CtaDto? DealerPage { get; set; }
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? OpeningHours { get; set; }
        public string? WebsiteUrl { get; set; }
        public bool MobileHubShowroom { get; set; }      
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string? MapInfoBoxContentTitle { get; set; }
        public string? MapInfoBoxContent { get; set; }
        public string? PostcodeList { get; set; }
        public IEnumerable<string>? Postcodes
        {
            get
            {
                return !string.IsNullOrEmpty(PostcodeList) ? PostcodeList.Split('|') : null;
            }
        }
        public bool IsMobileShowroom { get; set; }
        public string? Postcode
        {
            get
            {
                return Postcodes != null && Postcodes.Any() ? Postcodes.First() : null;
            }
        }

        public string? MapJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    logo = Logo is not null ? Logo.Src : null,
                    id = Id.ToString(),
                    position = new
                    {
                        lat = Latitude,
                        lng = Longitude,
                    },
                    dealerUrl = DealerPage is not null ? DealerPage.Url : string.Empty,
                    dealerName = DealerName,          
                    message_title = !IsMobileShowroom ? MapInfoBoxContentTitle : string.Empty,
                    message = !IsMobileShowroom ? MapInfoBoxContent : string.Empty,
                    website = WebsiteUrl,                  
                    phone = Phone,
                    address = Address,
                    postcode = Postcodes is not null ? Postcodes?.First() : "",
                    postcodesCovered = Postcodes,
                    mobileShowroom = IsMobileShowroom,
                    distance = DealerDistance,
                    booknow = BookNow is not null ? BookNow.Url : string.Empty
                });
            }
        }
        public bool FreeMeasureQuote { get; set; }
        public bool ServiceOrRepair { get; set; }
        public bool Commercial { get; set; }
        public double? DealerDistance { get; set; }
        public bool DistanceCalculateFromLatLng { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? State { get; set; }
        public string? DealerPostCode { get; set; }
        public CtaDto? BookNow { get; set; }

    }
}
