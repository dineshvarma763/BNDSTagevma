using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class InlineVideoDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? VideoUrl { get; set; }
    }
}
