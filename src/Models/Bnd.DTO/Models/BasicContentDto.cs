using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class BasicContentDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? HtmlContent { get; set; }
        public CtaDto? Cta { get; set; }
        public string? CtaLinkAlignment { get; set; }
    }
}
