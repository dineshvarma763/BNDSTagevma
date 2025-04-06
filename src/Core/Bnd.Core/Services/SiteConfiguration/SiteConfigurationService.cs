namespace Bnd.Core.Services.SiteConfiguration
{
    using Umbraco.Cms.Core;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using AutoMapper;
    using Umbraco.Cms.Core.Web;

    public class SiteConfigurationService : ISiteConfigurationService
    {
        private readonly IPublishedContentQuery _contentQuery;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IMapper _mapper;
        public SiteConfigurationService(IPublishedContentQuery contentQuery, IUmbracoContextAccessor umbracoContextAccessor, IMapper mapper)
        {
            _contentQuery = contentQuery;
            _umbracoContextAccessor = umbracoContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the website configuration primary node
        /// </summary>
        /// <returns></returns>
        public SiteConfigurationDto GetSiteConfiguration()
        {
            SiteConfigurationDto? siteConfiguration = new();

            if (_contentQuery
                .ContentAtRoot()
                .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is Website rootItem)
            {
                siteConfiguration = _mapper.Map<Website, SiteConfigurationDto>(rootItem);
            }

            return siteConfiguration; 
        }

        /// <summary>
        /// Gets the robots.txt content from the website
        /// </summary>
        /// <returns></returns>
        public string? GetRobots()
        {
            var robots = string.Empty;

            if (_contentQuery
               .ContentAtRoot()
               .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is Website rootItem)
            {

                robots = rootItem.Robotstxt;
            }

            return robots;
        }

    }
}
