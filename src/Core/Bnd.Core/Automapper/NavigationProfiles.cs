namespace Bnd.Core.Automapper
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Core.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Bnd.Core.Resolvers;
    using Bnd.Core.Extensions;
    using Bnd.Core.Models;

    public class NavigationProfiles : Profile
    {
        public NavigationProfiles()
        {
            CreateMap<Link, CtaDto>()
                .ForMember(d => d.Url, o => o.MapFrom<RelativeUrlCustomResolver>())
                .ForMember(d => d.Name, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Target, o => o.MapFrom(s => s.Target))
                ;

            CreateMap<ConfigurableLink, ConfigurableLinkDto>()
                .ForMember(d => d.Link, o => o.MapFrom(s => s.Link))
                ;

            CreateMap<ImageLink, LinkImageDto>()
                .ForMember(d => d.LinkImage, o => o.MapFrom(new ImageCustomResolver<ImageLink, LinkImageDto>(Constants.LinkImage)))
                .ForMember(d => d.Link, o => o.MapFrom(s => s.ImageUrl))
                ;
            CreateMap<IEnumerable<ImageLink>, List<LinkImageDto>>();

            CreateMap<Link, NavigationItemDto>()
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url.FormatRelativeUrl()))
                .ForMember(d => d.Target, o => o.MapFrom(s => s.Target))
                ;

            CreateMap<Page, NavigationItemDto>()
                .ForMember(d => d.Url, o => o.MapFrom(s => s.Url(null, UrlMode.Relative)))
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Image, o => o.MapFrom(new ImageCustomResolver<Page, NavigationItemDto>(Constants.MegamenuImage)))
                .ForMember(d => d.Children, o => o.Ignore())
                .ForMember(d => d.ViewAllCta, o => o.MapFrom(s => s.MegamenuViewAllLink))
                ;

            CreateMap<DoorVisualiserLandingPage, NavigationItemDto>()
                 .ForMember(d => d.Url, o => o.MapFrom(s => s.Url(null, UrlMode.Relative)))
                .ForMember(d => d.Label, o => o.MapFrom(s => s.Name))
                .ForMember(d => d.Image, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserLandingPage, NavigationItemDto>(Constants.MegamenuImage)))
                .ForMember(d => d.Children, o => o.Ignore())
                .ForMember(d => d.ViewAllCta, o => o.MapFrom(s => s.MegamenuViewAllLink))
                ;

            //Footer Contact info
            CreateMap<Website, FooterContactDto>()
                .ForMember(d => d.ContactTitle, o => o.MapFrom(s => s.ContactInfoTitle))
                .ForMember(d => d.ContactPhone, o => o.MapFrom(s => s.ContactInfoNumber))
                .ForMember(d => d.WeekdayOpeningTimes, o => o.MapFrom(s => s.WeekdayOpeningTimes))
                .ForMember(d => d.WeekendOpeningTimes, o => o.MapFrom(s => s.WeekendOpeningTimes))
                ;

            // Footer Navigation Sections
            CreateMap<NavigationSection, FooterNavigationSectionDto>()
                .ForMember(d => d.FooterNavigationItems, o => o.MapFrom(s => s.NavigationSectionItems))
                ;

            // Footer Social and External Link icons
            CreateMap<ImageLink, LinkImageDto>()
                .ForMember(d => d.Link, o => o.MapFrom(s => s.ImageUrl))
                .ForMember(d => d.LinkImage, o => o.MapFrom(new ImageCustomResolver<ImageLink, LinkImageDto>(Constants.LinkImage)))
                ;


        }
    }
}
