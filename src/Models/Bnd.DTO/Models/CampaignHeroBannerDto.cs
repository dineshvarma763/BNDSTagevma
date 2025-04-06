namespace Bnd.DTO.Models
{
    public class CampaignHeroBannerDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Links { get; set; }
        public string? DesktopVideoUrl { get; set; }
        public string? MobileVideoUrl { get; set; }
    }
}
