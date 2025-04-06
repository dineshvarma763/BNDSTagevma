using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class DoorVisualiserPanelDto
    {
        public ImageDto Image { get; set; }
        public IEnumerable<ConfigurableLinkDto> Cta { get; set; }
    }
}
