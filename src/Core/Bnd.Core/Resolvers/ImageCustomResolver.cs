namespace Bnd.Core.Resolvers
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Core.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using System.Reflection;
    public class ImageCustomResolver<TSrc, TDst> : IValueResolver<TSrc, TDst, ImageDto?>
    {
        private readonly string _property;
        public ImageCustomResolver(string property)
        {
            _property = property;
        }

        public ImageDto? Resolve(TSrc source, TDst destination, ImageDto? destMember, ResolutionContext context)
        {
            if (source is null)
                return null;

            var property = source.GetType().GetProperty(_property);

            if (property is null)
                return null;

            dynamic sourceProperty = property.GetValue(source, null);

            if (sourceProperty is null)
                return null;

            var sourceImage = (Image) sourceProperty;

            if (sourceImage is null)
                return null;

            
            return new ImageDto()
            {
                Src = $"{sourceImage.Url(null, Umbraco.Cms.Core.Models.PublishedContent.UrlMode.Absolute)}?rmode=max&width={sourceImage.UmbracoWidth}&height={sourceImage.UmbracoHeight}",
                AltText = sourceImage.AltText
            };
        }
    }
}
