using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class NavigationItemDto
    {
        public bool IsCurrentPage { get; set; }
        public string? Label { get; set; }
        public string? Url { get; set; }
        public string? Target { get; set; }
        public ImageDto? Image { get; set; }
        public IEnumerable<NavigationItemDto>? Children { get; set; }
        public CtaDto? ViewAllCta { get; set; }
    }
}
