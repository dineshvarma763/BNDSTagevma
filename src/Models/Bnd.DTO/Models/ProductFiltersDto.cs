using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class ProductFiltersDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<string>? ProductCategories {get;set;}
        public List<ProductFilterGroupDto>? FilterGroups { get; set; }

        // Mapped in Component factory
        public IEnumerable<ProductListingItemDto>? Items { get; set; }
        public string? FilterConfig { get; set; }
    }
}
