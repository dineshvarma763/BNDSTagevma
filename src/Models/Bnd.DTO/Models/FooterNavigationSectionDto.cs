using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class FooterNavigationSectionDto
    {
        public string? Title { get; set; }
        public IEnumerable<NavigationItemDto>? FooterNavigationItems { get; set; }
    }
}
