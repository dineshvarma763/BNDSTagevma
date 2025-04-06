using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class OnThisPageDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public IEnumerable<OnThisPageItemDto>? Items { get; set; }
    }
}
