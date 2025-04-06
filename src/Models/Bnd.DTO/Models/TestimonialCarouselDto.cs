using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class TestimonialCarouselDto: ComponentBaseDto
    {
        public ImageDto? MobileImage { get; set; }
        public ImageDto? DesktopImage { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Link { get; set; }
        public IEnumerable<TestimonialItemDto>? Panels { get; set; }

    }
}
