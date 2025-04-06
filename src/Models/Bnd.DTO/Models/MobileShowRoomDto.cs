namespace Bnd.DTO.Models
{
    using Newtonsoft.Json;

    public class MobileShowRoomDto
    {
        public int Id { get; set; }        
        public string? DealerName { get; set; }         
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? WebsiteUrl { get; set; }
     
        [JsonIgnore]
        public CtaDto? ServicesUrl { get; set; }  

        public string? PostcodeList { get; set; }
        public IEnumerable<string>? Postcodes
        {
            get
            {
                return !string.IsNullOrEmpty(PostcodeList) ? PostcodeList.Split('|') : null;
            }
        }
     
        
        public string? Postcode { 
            get
            {
               return Postcodes != null && Postcodes.Any() ? Postcodes.First() : null;
            } 
        }

        public string? MapJson
        {
            get
            {
                var servicesUrl = ServicesUrl is not null ? ServicesUrl.Url : string.Empty;
                var servicesUrlLabel = ServicesUrl is not null ? ServicesUrl.Name : string.Empty;

                return JsonConvert.SerializeObject(new
                {
                    //logo = null, //Logo is not null ? Logo.Src : null,
                    id = Id.ToString(),
                    position = new
                    {
                        lat = "",
                        lng = "",
                    },
                    seeMore = string.Empty,
                    title = DealerName, 
                    message_title = Title,
                    message = Description,
                    website = WebsiteUrl,
                    websiteLabel = servicesUrlLabel,
                    phone = Phone,
                    address = Address,
                    postcode = Postcodes is not null ? Postcodes?.First() : "",
                    postcodesCovered = Postcodes,                    
                    distance = 0
                });
            }
        }
        public bool FreeMeasureQuote { get; set; }
        public bool serviceOrRepair { get; set; }
        public bool Commercial { get; set; }

        public double? distance { get; set; }
    }
}
