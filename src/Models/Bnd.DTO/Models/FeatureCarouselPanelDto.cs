using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class FeatureCarouselPanelDto
    {
        public ImageDto? DesktopImage { get; set; }
        public ImageDto? MobileImage { get; set; }
        public string? Title { get; set; }
        public string? Subtitle { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Link { get; set; }
    }
}
