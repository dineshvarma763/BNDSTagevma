namespace Bnd.DTO.Models
{
    using System;
    using System.Collections.Generic;
    public class PageConfigurationDto : PageBaseDto
    {       
        public ImageDto ProductMobileImage { get; set; } = new ImageDto();
        public ImageDto ProductDesktopImage { get; set; } = new ImageDto();
        public string? ModalId { get; set; }
        public string? CampaignFormTitle { get; set; }
        public string? CTadesktopContent { get; set; }
        public int? ProductPriority { get; set; }
        public List<ComponentDto>? Components { get; set; }
        public NavigationDto? Navigation { get; set; }
        public FooterDto? Footer { get; set; }
        public List<NavigationItemDto>? Breadcrumbs { get; set; }
        public NavigationItemDto? UtilityNavigationItemsOverride { get; set; }
        public bool IsDoorVisualizer { get; set; }
        public DoorVisualiserLandingPageDto DoorVisualiser { get;set; }
        public DoorVisualiserPanelsPageDto? DoorVisualiserPanels { get; set; }

        public bool EnableEyebrowBanner { get; set; }
        public string? EyebrowBannerText { get; set; }
        public CtaDto? EyebrowBannerLink { get; set; }

        public bool? EnableOverride { get; set; }
        public List<NavigationItemDto>? MobileStickyNavigationItemsOverride { get; set; }
    }
}
