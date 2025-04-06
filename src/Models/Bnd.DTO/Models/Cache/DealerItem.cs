using Newtonsoft.Json;

namespace Bnd.DTO.Models.Cache
{
    public class CtaDto
    {
        public string? Name { get; set; }
        public string? Target { get; set; }
        public string? Url { get; set; }
    }

    public class DealerPage
    {
        [JsonProperty("name")]
        public string? Name { get; set; }

        [JsonProperty("target")]
        public string? Target { get; set; }

        [JsonProperty("url")]
        public string? Url { get; set; }
    }
    public class Logo
    {
        [JsonProperty("src")]
        public string? Src { get; set; }

        [JsonProperty("altText")]
        public string? AltText { get; set; }

        [JsonProperty("target")]
        public string? Target { get; set; }
    }

    public class DealerItem
    {
        [JsonProperty("id")]
        public int? Id { get; set; }

        [JsonProperty("logo")]
        public Logo? Logo { get; set; }

        [JsonProperty("dealerName")]
        public string? DealerName { get; set; }

        [JsonProperty("dealerPage")]
        public DealerPage? DealerPage { get; set; }

        [JsonProperty("address")]
        public string? Address { get; set; }

        [JsonProperty("phone")]
        public string? Phone { get; set; }

        [JsonProperty("email")]
        public string? Email { get; set; }

        [JsonProperty("openingHours")]
        public string? OpeningHours { get; set; }

        [JsonProperty("websiteUrl")]
        public string? WebsiteUrl { get; set; }

        [JsonProperty("mobileHubShowroom")]
        public bool? MobileHubShowroom { get; set; }

        [JsonProperty("servicesUrl")]
        public CtaDto? ServicesUrl { get; set; }

        [JsonProperty("longitude")]
        public double? Longitude { get; set; }

        [JsonProperty("latitude")]
        public double? Latitude { get; set; }

        [JsonProperty("postcodeList")]
        public string? PostcodeList { get; set; }

        [JsonProperty("postcodes")]
        public List<string>? Postcodes { get; set; }

        [JsonProperty("isMobileShowroom")]
        public bool? IsMobileShowroom { get; set; }

        [JsonProperty("postcode")]
        public string? Postcode { get; set; }

        [JsonProperty("mapJson")]
        public string? MapJson { get; set; }

        [JsonProperty("freeMeasureQuote")]
        public bool FreeMeasureQuote { get; set; }

        [JsonProperty("serviceOrRepair")]
        public bool serviceOrRepair { get; set; }
        [JsonProperty("commercial")]
        public bool Commercial { get; set; }
        [JsonProperty("dealerDistance")]
        public double? distance { get; set; }
        [JsonProperty("distanceCalculateFromLatLng")]
        public bool DistanceCalculateFromLatLng { get; set; }

        [JsonProperty("title")]
        public string? title { get; set; }

        [JsonProperty("description")]
        public string? description { get; set; }
        
        [JsonProperty("state")]
        public string? State { get; set; }

        [JsonProperty("dealerPostCode")]
        public string? dealerPostCode { get; set; }

    }
}
