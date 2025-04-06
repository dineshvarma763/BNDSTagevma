namespace Bnd.DTO.Models
{
    public class HeroBannerDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public ImageDto? DesktopImage { get; set; }
        public ImageDto? MobileImage { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Links { get; set; }
        public bool? BrandingEnable { get; set; }
    }
}
