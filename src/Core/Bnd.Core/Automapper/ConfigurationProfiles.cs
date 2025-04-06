using AutoMapper;
using Bnd.Core.Models;
using Bnd.Core.Resolvers;
using Bnd.DTO.Models;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Bnd.Core.Automapper
{
    public class ConfigurationProfiles : Profile
    {
        public ConfigurationProfiles()
        {

            CreateMap<Website, SiteConfigurationDto>()
                .ForMember(d => d.SiteLogo, o => o.MapFrom(new ImageCustomResolver<Website, SiteConfigurationDto>(Constants.SiteLogo)))
                .ForMember(d => d.SearchResultsPage, o => o.MapFrom(s => s.SearchResultsPage))
                ;

            // Home page mapper
            CreateMap<Home, PageConfigurationDto>()
               .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.Title))
               .ForMember(d => d.PageDescription, o => o.MapFrom(s => s.Description))
               .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.OpenGraphTitle))
               .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.OpenGraphDescription))
               .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageKeywords))
               .ForMember(d => d.OgType, o => o.MapFrom(s => s.OpenGraphType))
               .ForMember(d => d.SearchContentType, o => o.MapFrom(s => s.SearchContentType != null ? s.SearchContentType.Select(ct => ct.Name).ToList() : new List<string>()))
               .ForMember(d => d.SearchTopics, o => o.MapFrom(s => s.SearchTopics != null ? s.SearchTopics.Select(ct => ct.Name).ToList() : new List<string>()))
               .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
               .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
               .ForMember(d => d.BodyClosingScripts, o => o.MapFrom(s => s.BeforeBodyClosingScripts))
               .ForMember(d => d.OgImage, o => o.MapFrom(new ImageCustomResolver<Home, PageConfigurationDto>(Constants.OpenGraphImage)))
               .ForMember(d => d.ProductDesktopImage, o => o.MapFrom(new ImageCustomResolver<Home, PageConfigurationDto>(Constants.DesktopProductImage)))
               .ForMember(d => d.ProductMobileImage, o => o.MapFrom(new ImageCustomResolver<Home, PageConfigurationDto>(Constants.MobileProductImage)))
               .ForMember(d => d.ProductPriority, o => o.Ignore())
               ;

            //Page Mapper
            CreateMap<Page, PageConfigurationDto>()
                .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PageDescription, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.OpenGraphTitle))
                .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.OpenGraphDescription))
                .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageKeywords))
                .ForMember(d => d.OgType, o => o.MapFrom(s => s.OpenGraphType))
                .ForMember(d => d.SearchContentType, o => o.MapFrom(s => s.SearchContentType != null ? s.SearchContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.SearchTopics, o => o.MapFrom(s => s.SearchTopics != null ? s.SearchTopics.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategory != null ? s.ProductCategory.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductFilters, o => o.MapFrom(s => s.ProductFilters != null ? s.ProductFilters.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
                .ForMember(d => d.BodyClosingScripts, o => o.MapFrom(s => s.BeforeBodyClosingScripts))
                .ForMember(d => d.OgImage, o => o.MapFrom(new ImageCustomResolver<Page, PageConfigurationDto>(Constants.OpenGraphImage)))
                .ForMember(d => d.ProductDesktopImage, o => o.MapFrom(new ImageCustomResolver<Page, PageConfigurationDto>(Constants.DesktopProductImage)))
                .ForMember(d => d.ProductMobileImage, o => o.MapFrom(new ImageCustomResolver<Page, PageConfigurationDto>(Constants.MobileProductImage)))
                .ForMember(d => d.ProductPriority, o => o.Ignore())
                .ForMember(d => d.Alias, o => o.MapFrom(s => s.UmbracoUrlAlias));
            ;

            CreateMap<Error404, PageConfigurationDto>()
              .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.Title))
              .ForMember(d => d.PageDescription, o => o.MapFrom(s => s.Description))
              .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.OpenGraphTitle))
              .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.OpenGraphDescription))
              .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageKeywords))
              .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
              .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
              .ForMember(d => d.BodyClosingScripts, o => o.MapFrom(s => s.BeforeBodyClosingScripts))
              // Image Mapper
              .ForMember(d => d.OgImage, o => o.MapFrom(new ImageCustomResolver<Error404, PageConfigurationDto>(Constants.OpenGraphImage)))
              .ForMember(d => d.ProductDesktopImage, o => o.MapFrom(new ImageCustomResolver<Error404, PageConfigurationDto>(Constants.DesktopProductImage)))
              .ForMember(d => d.ProductMobileImage, o => o.MapFrom(new ImageCustomResolver<Error404, PageConfigurationDto>(Constants.MobileProductImage)))
              .ForMember(d => d.ProductPriority, o => o.Ignore())
              ;

            //Door Visualizer Mapper
            CreateMap<DoorVisualiserLandingPage, PageConfigurationDto>()
                .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PageDescription, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.OpenGraphTitle))
                .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.OpenGraphDescription))
                .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageKeywords))
                .ForMember(d => d.OgType, o => o.MapFrom(s => s.OpenGraphType))
                .ForMember(d => d.SearchContentType, o => o.MapFrom(s => s.SearchContentType != null ? s.SearchContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.SearchTopics, o => o.MapFrom(s => s.SearchTopics != null ? s.SearchTopics.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategory != null ? s.ProductCategory.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductFilters, o => o.MapFrom(s => s.ProductFilters != null ? s.ProductFilters.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
                .ForMember(d => d.BodyClosingScripts, o => o.MapFrom(s => s.BeforeBodyClosingScripts))
                .ForMember(d => d.OgImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserLandingPage, PageConfigurationDto>(Constants.OpenGraphImage)))
                .ForMember(d => d.ProductDesktopImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserLandingPage, PageConfigurationDto>(Constants.DesktopProductImage)))
                .ForMember(d => d.ProductMobileImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserLandingPage, PageConfigurationDto>(Constants.MobileProductImage)))
                .ForMember(d => d.ProductPriority, o => o.Ignore())
                .ForMember(d => d.DoorVisualiser, o => o.MapFrom(s => s))
                .ForMember(d => d.IsDoorVisualizer, o => o.MapFrom(s => s.IsDoorVisualiser))
                ;

            CreateMap<DoorVisualiserPanelsPage, PageConfigurationDto>()
                .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.Title))
                .ForMember(d => d.PageDescription, o => o.MapFrom(s => s.Description))
                .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.OpenGraphTitle))
                .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.OpenGraphDescription))
                .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageKeywords))
                .ForMember(d => d.OgType, o => o.MapFrom(s => s.OpenGraphType))
                .ForMember(d => d.SearchContentType, o => o.MapFrom(s => s.SearchContentType != null ? s.SearchContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.SearchTopics, o => o.MapFrom(s => s.SearchTopics != null ? s.SearchTopics.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ContentType, o => o.MapFrom(s => s.ContentType != null ? s.ContentType.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductCategories, o => o.MapFrom(s => s.ProductCategory != null ? s.ProductCategory.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.ProductFilters, o => o.MapFrom(s => s.ProductFilters != null ? s.ProductFilters.Select(ct => ct.Name).ToList() : new List<string>()))
                .ForMember(d => d.Topics, o => o.MapFrom(s => s.Topic != null ? s.Topic.Select(t => t.Name).ToList() : new List<string>()))
                .ForMember(d => d.BodyClosingScripts, o => o.MapFrom(s => s.BeforeBodyClosingScripts))
                .ForMember(d => d.OgImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserPanelsPage, PageConfigurationDto>(Constants.OpenGraphImage)))
                .ForMember(d => d.ProductDesktopImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserPanelsPage, PageConfigurationDto>(Constants.DesktopProductImage)))
                .ForMember(d => d.ProductMobileImage, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserPanelsPage, PageConfigurationDto>(Constants.MobileProductImage)))
                .ForMember(d => d.ProductPriority, o => o.Ignore())
                .ForMember(d => d.DoorVisualiserPanels, o => o.MapFrom(s => s))
                .ForMember(d => d.IsDoorVisualizer, o => o.MapFrom(s => s.IsDoorVisualiser))
                ;

            CreateMap<DoorVisualiserLandingPage, DoorVisualiserLandingPageDto>()
                .ForMember(d => d.BackgroundImage, o => o.MapFrom(s => s.BackgroundImage.Content.SafeCast<Image>()))
                .ForMember(d => d.LogoImage, o => o.MapFrom(s => s.LogoImage.Content.SafeCast<Image>()))
                .ForMember(d => d.Title, o => o.MapFrom(s => s.VisualiserTitle))
                .ForMember(d => d.Subtitle, o => o.MapFrom(s => s.VisualiserSubTitle))
                .ForMember(d => d.BasePageCta, o => o.MapFrom(s => s.VisualiserCta))
                .ForMember(d => d.Disclaimer, o => o.MapFrom(s => s.VisualiserDisclaimer))
                ;

            CreateMap<DoorVisualiserPanel, DoorVisualiserPanelDto>()
                .ForMember(d => d.Image, o => o.MapFrom(new ImageCustomResolver<DoorVisualiserPanel, DoorVisualiserPanelDto>(Constants.Image)))
                .ForMember(d => d.Cta, o => o.MapFrom(s => s.CTA))
                ;
            CreateMap<DoorVisualiserPanelsPage, DoorVisualiserPanelsPageDto>()
                .ForMember(d => d.PageTitle, o => o.MapFrom(s => s.VisualiserPanelsPageHeading))
                .ForMember(d => d.Panels, o => o.MapFrom(s => s.VisualiserPanels))
                .ForMember(d => d.Iframe, o => o.MapFrom(s => s.VisualiserIframe))
                ;
        }
    }
}
