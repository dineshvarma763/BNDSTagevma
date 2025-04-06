using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class SmallCarouselDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Link { get; set; }
        public string? LinkAlignment { get; set; }
        public IEnumerable<SmallCarouselPanelDto>? Panels { get; set; }
    }
}
