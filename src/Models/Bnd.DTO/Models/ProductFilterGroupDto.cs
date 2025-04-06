using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class ProductFilterGroupDto
    {
        public string? Title { get; set; }
        public IEnumerable<string>? FilterItems { get; set; }
    }
}
