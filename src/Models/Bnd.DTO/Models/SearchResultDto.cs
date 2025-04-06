using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bnd.DTO.Models
{
    public class SearchResultDto
    {
        // Content
        public string? PlaceholderText { get; set; }
        public string? ApplyCtaLabel { get; set; }
        public string? ClearAllLabel { get; set; }
        public string? FilterHeading { get; set; }
        public string? PrimaryFilterLabel { get; set; }
        public string? SecondaryFilterLabel { get; set; }
        public string? NoResultMessage { get; set; }
        public string? ResultCountMessage { get; set; }
        public string? HelpMessage { get; set; }
    }
}
