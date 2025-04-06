using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class FooterDto
    {
        public CtaDto? AppStoreLink { get; set; }
        public CtaDto? PlayStoreLink { get; set; }
        public IEnumerable<FooterNavigationSectionDto>? FooterNavigationSections { get; set; }
        public List<NavigationItemDto>? UtilityNavigationItems { get; set; }
        public string? CopyrightText { get; set; }
        public IEnumerable<LinkImageDto>? ExternalLinks { get; set; }
        public IEnumerable<LinkImageDto>? SocialLinks { get; set; }
        public FooterContactDto? Contact { get; set; }
    }
}
