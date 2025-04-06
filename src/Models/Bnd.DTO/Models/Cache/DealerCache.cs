using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models.Cache
{
    public class DealerCache
    {
        public string CacheKey { get; set; }
        //public List<DealerItem> Dealers { get; set; }
        public List<DealerDto> Dealers { get; set; }
    }
}
