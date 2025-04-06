using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class FeatureCarouselDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<FeatureCarouselPanelDto>? Panels { get; set; }
        public bool? BrandingEnable { get; set; }
    }
}
