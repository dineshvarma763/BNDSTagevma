using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class AnimatedBannerDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ConfigurableLinkDto? Cta { get; set; }
        public ImageDto? DesktopImage { get; set; }
        public ImageDto? MobileImage { get; set; }
    }
}
