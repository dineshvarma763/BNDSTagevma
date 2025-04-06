namespace Bnd.DTO.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    public class CardListingDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? NoOfCards { get; set; }
        public IEnumerable<CardListingItemDto>? ListingContents {get;set;}
    }
}
