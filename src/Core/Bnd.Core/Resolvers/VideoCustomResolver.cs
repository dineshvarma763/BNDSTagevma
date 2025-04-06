namespace Bnd.Core.Resolvers
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Core.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using System.Reflection;
    public class VideoCustomResolver<TSrc, TDst> : IValueResolver<TSrc, TDst, VideoDto?>
    {
        private readonly string _property;
        public VideoCustomResolver(string property)
        {
            _property = property;
        }

        public VideoDto? Resolve(TSrc source, TDst destination, VideoDto? destMember, ResolutionContext context)
        {
            if (source is null)
                return null;

            var property = source.GetType().GetProperty(_property);

            if (property is null)
                return null;

            dynamic sourceProperty = property.GetValue(source, null);

            if (sourceProperty is null)
                return null;

            var sourceMedia = (UmbracoMediaVideo)sourceProperty;

            if (sourceMedia is null)
                return null;

            return new VideoDto()
            {
                Src = sourceMedia.Url(null, Umbraco.Cms.Core.Models.PublishedContent.UrlMode.Absolute),
                Type = $"video/{sourceMedia.UmbracoExtension}"
            };
        }
    }
}
