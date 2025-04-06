using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class CampaignBannerDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? SmallText { get; set; }
        public string? Description { get; set; }
        public ImageDto? DesktopImage { get; set; }
        public ImageDto? MobileImage { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Link { get; set; }
        public string? DesktopVideoUrl { get; set; }
        public string? MobileVideoUrl { get; set; }
        public string? Variant { get; set; }
        public bool? EnableOverlay { get; set; }
    }
}
