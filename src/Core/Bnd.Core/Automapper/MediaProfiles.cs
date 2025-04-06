namespace Bnd.Core.Automapper
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using Umbraco.Cms.Core.Models.PublishedContent;
    public class MediaProfiles : Profile
    {
        public MediaProfiles()
        {
            //Umbraco SVG to ImageDto
            CreateMap<UmbracoMediaVectorGraphics, ImageDto>()
               .ForMember(d => d.Src, o => o.MapFrom(s => s.Url(null, UrlMode.Absolute)))
               .ForMember(d => d.AltText, o => o.MapFrom(s => s.Name))
               ;

            //Umbraco Video to VideoDto
            CreateMap<UmbracoMediaVideo, VideoDto>()
               .ForMember(d => d.Type, o => o.MapFrom(s => $"video/{s.UmbracoExtension}"))
               .ForMember(d => d.Src, o => o.MapFrom(s => s.Url(null, UrlMode.Absolute)))
               ;

            //Image to ImageDto
            CreateMap<Image, ImageDto>()
                .ForMember(d => d.AltText, o => o.MapFrom(s => s.AltText))
                .ForMember(d => d.Src, o => o.MapFrom(s => $"{s.Url(null, UrlMode.Absolute)}?rmode=max&width={s.UmbracoWidth}&height={s.UmbracoHeight}"))
                ;

            
        }
    }
}
