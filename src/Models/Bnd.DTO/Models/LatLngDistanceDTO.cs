using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class LatLngDistanceDTO
    {
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public double? DealerDistance { get; set; }
        public string? State { get; set; }
        public string? DealerPostCode {  get; set; }
    }
}
