namespace Bnd.Core.Services.PageConfiguration
{
    using AutoMapper;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Extensions;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Bnd.Core.Factory;
    using Bnd.Core.Extensions;

    public class PageConfigurationService : IPageConfigurationService
    {
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IMapper _mapper;
        private readonly IComponentFactory _componentFactory;
        private readonly INavigationFactory _navigationFactory;
        public PageConfigurationService(IUmbracoContextAccessor umbracoContextAccessor, IMapper mapper, IComponentFactory componentFactory, INavigationFactory navigationFactory)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _mapper = mapper;
            _componentFactory = componentFactory;
            _navigationFactory = navigationFactory;
        }

        /// <summary>
        /// Gets page configuration including components
        /// </summary>
        /// <param name="pageName"></param>
        /// <param name="serverUrl"></param>
        /// <returns></returns>
        public PageConfigurationDto? GetPageConfiguration(string pageName, string serverUrl)
        {
            pageName = pageName.RemoveTrailingSlash();

            PageConfigurationDto? result;
            if (string.IsNullOrEmpty(pageName) || pageName.ToLower().Equals($"/{Home.ModelTypeAlias}"))
            {
                var pageItem = _umbracoContextAccessor?.GetRequiredUmbracoContext()?.Content?.GetByRoute($"/{Home.ModelTypeAlias}");
                var homePage = pageItem as Home;
                result = _mapper.Map<Home, PageConfigurationDto>(homePage);
                result = GetComponents(result, pageItem);
            }
            else
            {
                var pageItem = _umbracoContextAccessor?.GetRequiredUmbracoContext()?.Content?.GetByRoute($"{pageName}");

                // For URL aliases
                if (pageItem is null)
                {
                    var websiteRoot = _umbracoContextAccessor.GetRequiredUmbracoContext()?.Content?.GetByXPath("//website").First();
                    var nodes = websiteRoot.DescendantsOrSelf().Where(x => x.IsPage());
                    pageItem = nodes.FirstOrDefault(x => x.Value<string>("umbracoUrlAlias").HasMatchingAlias(pageName));
                }

                switch (pageItem)
                {
                    case Page page:
                        result = _mapper.Map<Page, PageConfigurationDto>(page);
                        result.Breadcrumbs = _navigationFactory.GetBreadcrumbs(page);
                        result = GetComponents(result, pageItem);
                        break;
                    case DoorVisualiserLandingPage doorVisualiserPage:
                        result = _mapper.Map<DoorVisualiserLandingPage, PageConfigurationDto>(doorVisualiserPage);
                        break;
                    case DoorVisualiserPanelsPage doorVisualiserPanelsPage:
                        result = _mapper.Map<DoorVisualiserPanelsPage, PageConfigurationDto>(doorVisualiserPanelsPage);
                        break;
                    default:
                        result = null;
                        break;
                }
            }
            return result != null ? result.Id is null ? null : result : result;
        }

        /// <summary>
        /// Gets error page configuration
        /// </summary>
        /// <param name="serverUrl"></param>
        /// <returns></returns>
        public PageConfigurationDto GetErrorPage(string serverUrl)
        {
            var result = new PageConfigurationDto();
            var rootNode = _umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByXPath("//website").First();
            var pageItem = rootNode.Children.FirstOrDefault(x => x.ContentType.Alias.Equals(Error404.ModelTypeAlias));
            var errorPage = pageItem as Error404;
            if (errorPage is not null)
            {
                result = _mapper.Map<Error404, PageConfigurationDto>(errorPage);
                result = GetComponents(result, pageItem);
            }
            return result;
        }

        /// <summary>
        /// Gets all component under the page node
        /// </summary>
        /// <param name="pageConfiguration"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        private PageConfigurationDto GetComponents(PageConfigurationDto pageConfiguration, IPublishedContent page)
        {

            if (page is null && pageConfiguration is null)
            {
                return new PageConfigurationDto();
            }

            var result = new List<ComponentDto>();
            var pageComponents = page?.Children.FirstOrDefault(x => x.ContentType.Alias.Equals(Components.ModelTypeAlias)) as Components;

            if (pageComponents is null || !pageComponents.Children.Any())
            {
                return pageConfiguration;
            }

            foreach (var component in pageComponents.Children)
            {
                var componentDto = new ComponentDto()
                {
                    ComponentName = component.ContentType.Alias,
                    ComponentData = GetComponentData(component)
                };
                result.Add(componentDto);
            }

            pageConfiguration.Components = result;

            return pageConfiguration;
        }

        /// <summary>
        /// Determine concrete type mapping
        /// </summary>
        /// <param name="component"></param>
        /// <returns></returns>
        private object? GetComponentData(IPublishedContent component)
        {
            object? result = null;

            if (_componentFactory.GetAvailableComponents().Contains(component.ContentType.Alias))
            {
                result = _componentFactory.GetComponent(component.ContentType.Alias, component);
            }
            
            return result;
        }

    }
}
