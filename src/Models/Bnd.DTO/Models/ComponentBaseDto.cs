namespace Bnd.DTO.Models
{
    public class ComponentBaseDto
    {
        public int? Id { get; set; }
        public Guid Key { get; set; }
        
        public string? BackgroundColour { get; set; }
        public string? FontColour { get; set; }
        public bool EnableOnThisPage { get; set; }
        public string? OnThisPageAnchor { get; set; }
        public string? OnThisPageLabel { get; set; }
        public bool UseSmallHeading { get; set; }
        public string? ContentAlignment { get; set; }
        public ImageDto? DesktopBackgroundImage { get; set; }
        public ImageDto? MobileBackgroundImage { get; set; }
        public string? AspectRatio { get; set; }
        public VideoDto? BackgroundVideo { get; set; }
        public VideoDto? MobileBackgroundVideo { get; set; }
    }
}
