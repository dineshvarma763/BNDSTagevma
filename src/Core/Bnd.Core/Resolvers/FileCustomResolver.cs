namespace Bnd.Core.Resolvers
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Core.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using System.Reflection;
    public class FileCustomResolver<TSrc, TDst> : IValueResolver<TSrc, TDst, IEnumerable<FileDto>?>
    {
        private readonly string _property;
        public FileCustomResolver(string property)
        {
            _property = property;
        }

        public IEnumerable<FileDto>? Resolve(TSrc source, TDst destination, IEnumerable<FileDto>? destMember, ResolutionContext context)
        {
            if (source is null)
                return null;

            var property = source.GetType().GetProperty(_property);

            if (property is null)
                return null;

            dynamic sourceProperty = property.GetValue(source, null);

            if (sourceProperty is null)
                return null;

            var listMedia = sourceProperty as List<MediaWithCrops>;

            if (listMedia is null)
                return null;

            var result =  new List<FileDto>();

            foreach(var item in listMedia)
            {
                if (item is null)
                    continue;
                var media = item.Content.SafeCast<UmbracoMediaArticle>();
                if (media is null)
                    continue;
                result.Add(new FileDto()
                {
                    Src = media.Url(null, Umbraco.Cms.Core.Models.PublishedContent.UrlMode.Absolute),
                    AltText = media.AltText,
                    Name = media.Name,
                });
            }

            return result;
        }
    }
}
