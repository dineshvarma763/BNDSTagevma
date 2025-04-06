using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class DoorVisualiserLandingPageDto
    {
        public ImageDto BackgroundImage { get; set; }
        public ImageDto LogoImage { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public IEnumerable<ConfigurableLinkDto> BasePageCta { get; set; }
        public string Disclaimer { get; set; }
    }
}
