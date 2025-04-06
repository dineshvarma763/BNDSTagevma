using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class LookingForDto: ComponentBaseDto
    {
        public string? SmallTitle { get; set; }
        public string? LargeTitle { get; set; }
        public string? Description { get; set; }
        public bool CardImagesOnLeft { get; set; }
        public List<CardDto> CardItems { get; set; }
    }
}
