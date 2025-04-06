using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class ZigZagCardDto
    {
        public ImageDto? CardMobileImage { get; set; }
        public ImageDto? CardDesktopImage { get; set; }
        public string? CardTitle { get; set; }
        public string? CardSubTitle { get; set; }
        public bool UseSmallCardTitle { get; set; }
        public string? CardTitleAlignment { get; set; }
        public string? CardDescription { get; set; }
        public IEnumerable<ConfigurableLinkDto>? CardLinks { get; set; }
    }
}
