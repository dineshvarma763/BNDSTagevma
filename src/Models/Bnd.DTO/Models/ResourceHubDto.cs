using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class ResourceHubDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<CardListingItemDto>? CardItems { get; set; }
        public string? FilterConfig { get; set; }
    }
}
