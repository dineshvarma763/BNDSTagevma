namespace Bnd.DTO.Models
{
    public class FindADealerDto: ComponentBaseDto
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public string? ResultText { get; set; }
        public string? MobileShowroomPopupTitle { get; set; }
        public string? MobileShowroomPopup { get; set; }        
        public string? ApiEndpointsConfig { get; set; }
        public IEnumerable<ConfigurableLinkDto>? Link { get; set; }
        public CtaDto? BookNowLink { get; set; }
        public IEnumerable<string>? DealerFilters { get; set; }
    }
}
