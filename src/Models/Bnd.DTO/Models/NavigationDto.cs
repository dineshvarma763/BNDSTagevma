using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public  class NavigationDto
    {
        public ImageDto? SiteLogo { get; set; }
        public ImageDto? ShowroomImage {get; set; }
        public CtaDto? ShowroomLink { get; set; }
        public List<NavigationItemDto>? UtilityNavigationItems { get; set; }
        public List<NavigationItemDto>? MobileStickyNavigationItems { get; set; }

        public List<NavigationItemDto>? NavigationItems { get; set; }
        public string? SearchResultPage { get; set; }
        public string? SearchPlaceholderText { get; set; }
        public string? SearchPlacholderTextMobile { get; set; }
    }
}
