using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class DoorVisualiserPanelsPageDto
    {
        public string PageTitle { get; set; }
        public IEnumerable<DoorVisualiserPanelDto> Panels { get; set; }
        public string Iframe { get; set; }
    }
}
