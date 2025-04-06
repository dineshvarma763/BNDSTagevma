namespace Bnd.DTO.Models
{
    public class CardListDto : ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Links { get; set; }
        public string? LinkAlignment { get; set; }
        public IEnumerable<CardDto>? CardItems { get; set; }
        public bool? BrandingEnable { get; set; }
    }
}
