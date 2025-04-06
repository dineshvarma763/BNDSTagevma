using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class InlineBannerDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? LinkAlignment { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Links { get; set; }
    }
}
