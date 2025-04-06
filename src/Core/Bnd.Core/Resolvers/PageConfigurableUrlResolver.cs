using AutoMapper;
using Bnd.DTO.Models;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Web.Common.PublishedModels;
using Umbraco.Extensions;

namespace Bnd.Core.Resolvers
{
    public class PageConfigurableUrlResolver : IValueResolver<Page, FeatureCarouselPanelDto, IEnumerable<ConfigurableLinkDto>>
    {
        public IEnumerable<ConfigurableLinkDto> Resolve(Page source, FeatureCarouselPanelDto destination, IEnumerable<ConfigurableLinkDto> destMember, ResolutionContext context)
        {
            var result = new List<ConfigurableLinkDto>();

            if (source is null)
                return result;

            var cta = new CtaDto()
            {
                Url = source.Url(null, UrlMode.Relative),
                Target = null,
                Name = source.Name
            };

            var link = new ConfigurableLinkDto()
            {
                Link = cta,
                LinkType = "Primary"
            };
            result.Add(link);
            return result;
        }
    }
}
