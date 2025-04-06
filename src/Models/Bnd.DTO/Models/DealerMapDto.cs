using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class DealerMapDto : ComponentBaseDto
    {
        public DealerDto? Dealer { get; set; }
        public bool IsDealerMap { get; set; } = false;
    }
}
