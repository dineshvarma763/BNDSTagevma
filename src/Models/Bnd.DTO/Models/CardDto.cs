using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class CardDto
    {
        public ConfigurableLinkDto? ConfigurableLink { get; set; }
        public string? Title { get; set; }
        public bool UseSmallTitle { get; set; }
        public string? TitleAlignment { get; set; }
        public string? Description { get; set; }
        public ImageDto? MobileImage { get; set; }
        public ImageDto? DesktopImage { get; set; }
        public IEnumerable<FileDto>? Files { get; set; }
    }
}
