namespace Bnd.Core.Automapper
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using Resolvers;
    using static Extensions.ThemeExtensions;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Bnd.Core.Models;
    using Bnd.Core.Extensions;
    using System.Reflection;

    public class ComponentProfiles : Profile
    {
        public ComponentProfiles()
        {
            CreateMap<BasicContent, BasicContentDto>()
                .ForMember(d => d.Cta, o => o.MapFrom(s => s.CtaLink))
                .ForMember(d => d.HtmlContent, o => o.MapFrom(new RichTextUrlResolver<BasicContent, BasicContentDto>(Constants.HtmlContent)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<BasicContent, BasicContentDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<BasicContent, BasicContentDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<BasicContent, BasicContentDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<BasicContent, BasicContentDto>(Constants.MobileBackgroundVideo)))
                ;
            CreateMap<Iframe, IframeDto>()
                .ForMember(d => d.IframeUrl, o => o.MapFrom(s => s.IframeUrl));

            // Hero Banner
            CreateMap<HeroBanner, HeroBannerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<HeroBanner, HeroBannerDto>(Constants.Description)))
                .ForMember(d => d.Links, o => o.MapFrom(s => s.Links))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<HeroBanner, HeroBannerDto>(Constants.DesktopImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<HeroBanner, HeroBannerDto>(Constants.MobileImage)))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<HeroBanner, HeroBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<HeroBanner, HeroBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<HeroBanner, HeroBannerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<HeroBanner, HeroBannerDto>(Constants.MobileBackgroundVideo)))
                ;

            // Animated banner
            CreateMap<AnimatedBanner, AnimatedBannerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<AnimatedBanner, AnimatedBannerDto>(Constants.Description)))
                .ForMember(d => d.Cta, o => o.MapFrom(s => s.Link != null ? s.Link.FirstOrDefault() : null))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.DesktopImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.MobileImage)))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<AnimatedBanner, AnimatedBannerDto>(Constants.MobileBackgroundVideo)))
                ;

            //Accordion
            CreateMap<AccordionPanel, AccordionPanelDto>()
                .ForMember(d => d.Content, o => o.MapFrom(new RichTextUrlResolver<AccordionPanel, AccordionPanelDto>(Constants.Content)));
            CreateMap<Accordion, AccordionDto>()
                 .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<Accordion, AccordionDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<Accordion, AccordionDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<Accordion, AccordionDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<Accordion, AccordionDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<Accordion, AccordionDto>(Constants.MobileBackgroundVideo)))
                ;

            // Tabbed Content
            CreateMap<TabPanel, TabPanelDto>()
                .ForMember(d => d.TabContent, o => o.MapFrom(new RichTextUrlResolver<TabPanel, TabPanelDto>(Constants.TabContent)));
            CreateMap<TabbedContent, TabbedContentDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<TabbedContent, TabbedContentDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<TabbedContent, TabbedContentDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<TabbedContent, TabbedContentDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<TabbedContent, TabbedContentDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<TabbedContent, TabbedContentDto>(Constants.MobileBackgroundVideo)))
                ;

            // Card Listing (homepage)
            CreateMap<Page, CardListingItemDto>()
                .ForMember(d => d.ListingTitle, o => o.MapFrom(s => !string.IsNullOrEmpty(s.ListingTitle) ? s.ListingTitle : s.Title))
                .ForMember(d => d.ListingLink, o => o.MapFrom(s => s.Url(null, UrlMode.Relative)))
                .ForMember(d => d.ListingDesktopImage, o => o.MapFrom(new ImageCustomResolver<Page, CardListingItemDto>(Constants.ListingDesktopImage)))
                .ForMember(d => d.ListingMobileImage, o => o.MapFrom(new ImageCustomResolver<Page, CardListingItemDto>(Constants.ListingMobileImage)))
                .ForMember(d => d.ContentTypes, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
                ;
            CreateMap<CardListing, CardListingDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<CardListing, CardListingDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CardListing, CardListingDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CardListing, CardListingDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CardListing, CardListingDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CardListing, CardListingDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.ListingContents, o => o.MapFrom(s => s.ListingContents != null ? s.ListingContents.Cast<Page>() : new List<Page>()))
                ;

            // Card list (with downloads)
            CreateMap<Card, CardDto>()
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<Card, CardDto>("MobileImage")))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<Card, CardDto>("DesktopImage")))
                .ForMember(d => d.Files, o => o.MapFrom(new FileCustomResolver<Card, CardDto>("Files")))
                .ForMember(d => d.ConfigurableLink, o => o.MapFrom(s => s.Link != null ? s.Link.FirstOrDefault() : null))
                ;
            CreateMap<CardList, CardListDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<CardList, CardListDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CardList, CardListDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CardList, CardListDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CardList, CardListDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CardList, CardListDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.Links, o => o.MapFrom(s => s.Links))
                //.ForMember(d => d.CardItems, o => o.Ignore())
                .ForMember(d => d.CardItems, o => o.MapFrom(s => s.CardItems != null ? s.CardItems.Cast<Card>() : new List<Card>()))
                ;

            // Search Result
            CreateMap<SearchResult, SearchResultDto>();

            // Inline Video
            CreateMap<Video, InlineVideoDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<Video, InlineVideoDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<Video, InlineVideoDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<Video, InlineVideoDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<Video, InlineVideoDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<Video, InlineVideoDto>(Constants.MobileBackgroundVideo)))
                ;

            // Inline Banner
            CreateMap<InlineBanner, InlineBannerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<InlineBanner, InlineBannerDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<InlineBanner, InlineBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<InlineBanner, InlineBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<InlineBanner, InlineBannerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<InlineBanner, InlineBannerDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.Links, o => o.MapFrom(s => s.Links))
                ;

            //Zigzag
            CreateMap<ZigzagCard, ZigZagCardDto>()
                .ForMember(d => d.CardDescription, o => o.MapFrom(new RichTextUrlResolver<ZigzagCard, ZigZagCardDto>(Constants.CardDescription)))
                .ForMember(d => d.CardMobileImage, o => o.MapFrom(new ImageCustomResolver<ZigzagCard, ZigZagCardDto>("CardMobileImage")))
                .ForMember(d => d.CardDesktopImage, o => o.MapFrom(new ImageCustomResolver<ZigzagCard, ZigZagCardDto>("CardDesktopImage")))
                .ForMember(d => d.CardLinks, o => o.MapFrom(s => s.CardLinks))
                ;
            CreateMap<ZigZag, ZigZagDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<ZigZag, ZigZagDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ZigZag, ZigZagDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ZigZag, ZigZagDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ZigZag, ZigZagDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ZigZag, ZigZagDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.ZigZagItems, o => o.MapFrom(s => s.ZigZagItems))
                ;

            // On This Page
            CreateMap<IPublishedContent, OnThisPageItemDto>()
                .ForMember(d => d.OnThisPageLabel, o => o.MapFrom(s => s.OnThisPageLabel()))
                .ForMember(d => d.OnThisPageAnchor, o => o.MapFrom(s => s.OnThisPageAnchor()))
                ;
            CreateMap<OnThisPage, OnThisPageDto>()
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<OnThisPage, OnThisPageDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<OnThisPage, OnThisPageDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<OnThisPage, OnThisPageDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<OnThisPage, OnThisPageDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.Items, o => o.Ignore());

            // Resource Hub
            CreateMap<ResourceHub, ResourceHubDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<ResourceHub, ResourceHubDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ResourceHub, ResourceHubDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ResourceHub, ResourceHubDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ResourceHub, ResourceHubDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ResourceHub, ResourceHubDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.CardItems, o => o.Ignore());

            // Testimonial Carousel
            CreateMap<TestimonialPanel, TestimonialItemDto>();
            CreateMap<TestimonialCarousel, TestimonialCarouselDto>()
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.MobileImage)))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<TestimonialCarousel, TestimonialCarouselDto>(Constants.DesktopImage)))
                .ForMember(d => d.Link, o => o.MapFrom(s => s.Link))
                .ForMember(d => d.Panels, o => o.MapFrom(s => s.Testimonials));

            // Small Carousel
            CreateMap<SmallCarouselPanels, SmallCarouselPanelDto>()
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<SmallCarouselPanels, SmallCarouselPanelDto>(Constants.DesktopImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<SmallCarouselPanels, SmallCarouselPanelDto>(Constants.MobileImage)))
                ;
            CreateMap<SmallCarousel, SmallCarouselDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<SmallCarousel, SmallCarouselDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<SmallCarousel, SmallCarouselDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<SmallCarousel, SmallCarouselDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<SmallCarousel, SmallCarouselDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<SmallCarousel, SmallCarouselDto>(Constants.MobileBackgroundVideo)))
                ;

            // Feature Carousel
            //CreateMap<Page, ConfigurableLink>()
            //    .ForMember(d => d.LinkType, o => o.MapFrom("Primary"))
            //    ;
            CreateMap<Page, FeatureCarouselPanelDto>()
                .ForMember(d => d.Link, o => o.MapFrom<PageConfigurableUrlResolver>())
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<Page, FeatureCarouselPanelDto>(Constants.DesktopProductImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<Page, FeatureCarouselPanelDto>(Constants.MobileProductImage)))
                ;
            CreateMap<CarouselPanel, FeatureCarouselPanelDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<CarouselPanel, FeatureCarouselPanelDto>(Constants.Description)))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<CarouselPanel, FeatureCarouselPanelDto>(Constants.DesktopImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<CarouselPanel, FeatureCarouselPanelDto>(Constants.MobileImage)))
                ;
            CreateMap<FeatureCarousel, FeatureCarouselDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<FeatureCarousel, FeatureCarouselDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<FeatureCarousel, FeatureCarouselDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<FeatureCarousel, FeatureCarouselDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<FeatureCarousel, FeatureCarouselDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<FeatureCarousel, FeatureCarouselDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.Panels, o => o.Ignore()) // ignored, please refer to the factory
                ;


            //Image gallery
            CreateMap<ImageGallery, ImageGalleryDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<ImageGallery, ImageGalleryDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ImageGallery, ImageGalleryDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ImageGallery, ImageGalleryDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ImageGallery, ImageGalleryDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ImageGallery, ImageGalleryDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.Images, o => o.Ignore());

            // Dealer Map
            CreateMap<Dealer, DealerDto>()
                .ForMember(d => d.OpeningHours, o => o.MapFrom(new RichTextUrlResolver<Dealer, DealerDto>(Constants.OpeningHours)))
                .ForMember(d => d.IsMobileShowroom, o => o.MapFrom(s => s.ContentType.Alias.Equals(MobileShowroomItem.ModelTypeAlias)))
                .ForMember(d => d.Logo, o => o.MapFrom(new ImageCustomResolver<Dealer, DealerDto>(Constants.DealerLogo)));
            CreateMap<DealerMap, DealerMapDto>()
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<DealerMap, DealerMapDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<DealerMap, DealerMapDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<DealerMap, DealerMapDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<DealerMap, DealerMapDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.IsDealerMap, o => o.MapFrom(s => s.ContentType.Alias.Equals(DealerMap.ModelTypeAlias)))
                .ForMember(d => d.Dealer, o => o.Ignore());

            // Find a dealer            
            CreateMap<MobileShowroomItem, DealerDto>()           
              .ForMember(d => d.IsMobileShowroom, o => o.MapFrom(s => s.ContentType.Alias.Equals(MobileShowroomItem.ModelTypeAlias)))
             ;

            CreateMap<FindAdealer, FindADealerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<FindAdealer, FindADealerDto>(Constants.Description)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<FindAdealer, FindADealerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<FindAdealer, FindADealerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<FindAdealer, FindADealerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<FindAdealer, FindADealerDto>(Constants.MobileBackgroundVideo)))
                .ForMember(d => d.DealerFilters, o => o.MapFrom(s => s.DealerFilters != null ? s.DealerFilters.Select(ct => ct.Name).ToList() : new List<string>()))
                ;

            // Product Filters
            CreateMap<Page, ProductListingItemDto>()
               .ForMember(d => d.ListingTitle, o => o.MapFrom(s => !string.IsNullOrEmpty(s.ListingTitle) ? s.ListingTitle : s.Title))
               .ForMember(d => d.ListingLink, o => o.MapFrom(s => s.Url(null, UrlMode.Relative)))
               .ForMember(d => d.ListingDesktopImage, o => o.MapFrom(new ImageCustomResolver<Page, ProductListingItemDto>(Constants.ListingDesktopImage)))
               .ForMember(d => d.ListingMobileImage, o => o.MapFrom(new ImageCustomResolver<Page, ProductListingItemDto>(Constants.ListingMobileImage)))
               .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategory != null ? s.ProductCategory.Select(ct => ct.Name).ToList() : new List<string>()))
               .ForMember(d => d.ProductFilters, o => o.MapFrom(s => s.ProductFilters != null ? s.ProductFilters.Select(ct => ct.Name).ToList() : new List<string>()))
               .ForMember(d => d.ProductPopularity, o => o.MapFrom(s => string.IsNullOrEmpty(s.ProductPriority) ? 0.0 : double.Parse(s.ProductPriority)))
               ;
            CreateMap<ProductFilterGroup, ProductFilterGroupDto>()
                .ForMember(d => d.FilterItems, o => o.MapFrom(s => s.Filters != null ? s.Filters.Select(ct => ct.Name).ToList() : new List<string>()))
                ;
            CreateMap<ProductFilters, ProductFiltersDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<ProductFilters, ProductFiltersDto>(Constants.Description)))
                .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategories != null ? s.ProductCategories.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.FilterGroups, o => o.MapFrom(s => s.ProductFilterGroups))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ProductFilters, ProductFiltersDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<ProductFilters, ProductFiltersDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ProductFilters, ProductFiltersDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<ProductFilters, ProductFiltersDto>(Constants.MobileBackgroundVideo)))
                ;
            //I am Looking For
            CreateMap<LookingFor, LookingForDto>()
                .ForMember(d => d.SmallTitle, o => o.MapFrom(s => s.SmallTitle))
                .ForMember(d => d.LargeTitle, o => o.MapFrom(s => s.LargeTitle))
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<LookingFor, LookingForDto>(Constants.Description)))
                .ForMember(d => d.CardImagesOnLeft, o => o.MapFrom(s => s.CardImagesOnLeft))
                .ForMember(d => d.CardItems, o => o.MapFrom(s => s.CardItems))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<LookingFor, LookingForDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<LookingFor, LookingForDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<LookingFor, LookingForDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<LookingFor, LookingForDto>(Constants.MobileBackgroundVideo)))
               ;

            // Campaign Hero Banner
            CreateMap<CampaignHeroBanner, CampaignHeroBannerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.Description)))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CampaignHeroBanner, CampaignHeroBannerDto>(Constants.MobileBackgroundVideo)))
                ;

            // Campaign Banner
            CreateMap<CampaignBanner, CampaignBannerDto>()
                .ForMember(d => d.Description, o => o.MapFrom(new RichTextUrlResolver<CampaignBanner, CampaignBannerDto>(Constants.Description)))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.DesktopImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.DesktopImage)))
                .ForMember(d => d.MobileImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.MobileImage)))
                .ForMember(d => d.BackgroundColour, o => o.MapFrom(s => s.BackgroundColour.ToFriendlyBackgroundColour()))
                .ForMember(d => d.FontColour, o => o.MapFrom(s => s.FontColour.ToFriendlyFontColour()))
                .ForMember(d => d.DesktopBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.DesktopBackgroundImage)))
                .ForMember(d => d.MobileBackgroundImage, o => o.MapFrom(new ImageCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.MobileBackgroundImage)))
                .ForMember(d => d.BackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.BackgroundVideo)))
                .ForMember(d => d.MobileBackgroundVideo, o => o.MapFrom(new VideoCustomResolver<CampaignBanner, CampaignBannerDto>(Constants.MobileBackgroundVideo)));

            //MobileShowRoom
            CreateMap<MobileShowroomItem, MobileShowRoomDto>();
            CreateMap<FindAdealer, CtaDto>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.BookNowLink.Name))
                .ForMember(d => d.Url, o => o.MapFrom<BookNowLinkUrlResolver>())
                .ForMember(d => d.Target, o => o.MapFrom(s => s.BookNowLink.Target))
                ;

            //CreateMap<MobileShowRoomDto, DealerDto>()
            //    .ForMember(d => d.Address, o => o.MapFrom(s => s.Address))
            //    .ForMember(d => d.DealerName, o => o.MapFrom(s => s.DealerName))
            //    .ForMember(d => d.DealerName, o => o.MapFrom(s => s.DealerName))
            //    ;

        }
    }
}
