using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class SmallCarouselPanelDto
    {
        public ImageDto? DesktopImage { get; set; }
        public ImageDto? MobileImage { get; set; }
        public string? Title { get; set; }
    }
}
