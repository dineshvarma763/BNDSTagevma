using Newtonsoft.Json;

namespace Bnd.DTO.Models
{
    public class RedirectsDto
    {
        [JsonIgnore]
        public string Id { get; set; }
        public string? OldUrl { get; set; }
        public string? NewUrl { get; set; }
        public bool HasRedirect { get; set; }
        public string? Type { get; set; }
        
    }
}
