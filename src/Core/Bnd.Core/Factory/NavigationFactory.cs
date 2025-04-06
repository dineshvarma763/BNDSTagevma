namespace Bnd.Core.Factory
{
    using AutoMapper;
    using Bnd.Core.Extensions;
    using Bnd.DTO.Models;
    using Umbraco.Cms.Core;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;

    public class NavigationFactory : INavigationFactory
    {
        private readonly IMapper _mapper;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IPublishedContentQuery _publishedContentQuery;
        public NavigationFactory(IPublishedContentQuery publishedContentQuery, IUmbracoContextAccessor umbracoContextAccessor, IMapper mapper)
        {
            _publishedContentQuery = publishedContentQuery;
            _umbracoContextAccessor = umbracoContextAccessor;
            _mapper = mapper;
        }

        /// <summary>
        /// Builds the navigation dto using Umbraco context
        /// </summary>
        /// <returns></returns>
        public NavigationDto? GetNavigation()
        {
            var utilityNavigation = GetUtilityNavigation();
            var mobileStickyNavigation = GetMobileStickyNavigation();
            var navigationItems = GetNavigationItems();
            var siteLogo = GetSiteLogo();
            var showroomImage = GetShowroomImage();
            var showroomLink = GetShowroomLink();

            var result = new NavigationDto()
            {
                UtilityNavigationItems = utilityNavigation,
                MobileStickyNavigationItems = mobileStickyNavigation,
                NavigationItems = navigationItems,
                SiteLogo = siteLogo,
                ShowroomImage = showroomImage,
                ShowroomLink = showroomLink
            };
            result = GetSearchConfiguration(result);

            return result;
        }

        /// <summary>
        /// Builds the footer dto using Umbraco context
        /// </summary>
        /// <returns></returns>
        public FooterDto? GetFooter()
        {
            FooterDto? result = null;
            var rootNode = _umbracoContextAccessor
                          .GetRequiredUmbracoContext()
                          .Content
                          .GetByXPath("//website")
                          .First();

            if (rootNode is null)
                return result;

            var websiteConfig = (rootNode as Website);

            if (websiteConfig is null)
                return result;

            result = new FooterDto()
            {
                FooterNavigationSections = _mapper.Map<IEnumerable<FooterNavigationSectionDto>>(websiteConfig.FooterNavigationSections),
                UtilityNavigationItems = _mapper.Map<IEnumerable<NavigationItemDto>>(websiteConfig.UtilityFooterLinks).ToList(),
                AppStoreLink = _mapper.Map<CtaDto>(websiteConfig.AppStoreLink),
                PlayStoreLink = _mapper.Map<CtaDto>(websiteConfig.PlayStoreLink),
                CopyrightText = websiteConfig.CopyrightText.ToString(),
                ExternalLinks = _mapper.Map<IEnumerable<LinkImageDto>>(websiteConfig.ExternalLinks),
                SocialLinks = _mapper.Map<IEnumerable<LinkImageDto>>(websiteConfig.SocialLinks),
                Contact = _mapper.Map<FooterContactDto>(websiteConfig)
            };

            result.FooterNavigationSections = result.FooterNavigationSections.CheckFooterAlias(_umbracoContextAccessor);
            result.UtilityNavigationItems = result.UtilityNavigationItems.CheckUtilityAlias(_umbracoContextAccessor);
            return result;
        }
        public List<NavigationItemDto>? GetBreadcrumbs(Page page)
        {
            var result = new List<NavigationItemDto>();
            var publishedItems = page.AncestorsOrSelf();

            if (publishedItems is null)
                return result;

            var publishedPages = publishedItems.Where(pI => pI.IsPage());

            if(publishedPages is null || !publishedPages.Any())
                return result;

            result = GetBreadcrumbItems(publishedPages);

            result.Add(new NavigationItemDto()
            {
                Label = "Home",
                Url = "/"
            });

            result.Reverse();

            return result;
        }

        private List<NavigationItemDto> GetUtilityNavigation()
        {
            var result = new List<NavigationItemDto>();
            if (_publishedContentQuery
                .ContentAtRoot()
                .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is Website rootItem)
            {
                result = _mapper.Map<IEnumerable<NavigationItemDto>>(rootItem.UtilityNavigationItems).ToList();
            }
            return result;
        }

        private List<NavigationItemDto> GetMobileStickyNavigation()
        {
            var result = new List<NavigationItemDto>();
            if (_publishedContentQuery
                .ContentAtRoot()
                .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is Website rootItem)
            {
                result = _mapper.Map<IEnumerable<NavigationItemDto>>(rootItem.MobileStickyNavigationItems).ToList();
            }
            return result;
        }

        private List<NavigationItemDto> GetNavigationItems()
        {
            var result = new List<NavigationItemDto>();

            var rootNode = _umbracoContextAccessor
                .GetRequiredUmbracoContext()
                .Content
                .GetByXPath("//website")
                .First();


            if (!rootNode.HasChildPages())
                return result;

            var children = rootNode.Children
                .Where(c => c.IsPage() && c.IncludeInMegamenu());

            result = GetNavigationItems(children);
            
            return result;
        }

        private List<NavigationItemDto> GetBreadcrumbItems(IEnumerable<IPublishedContent> pages)
        {
            var result = new List<NavigationItemDto>();
            foreach (var page in pages)
            {
                var pageItem = page as Page;
                var navItem = _mapper.Map<NavigationItemDto>(page);
                var canonicalUrl = pageItem.CanonicalUrl;
                var aliasedUrl = pageItem.UmbracoUrlAlias.Split(',').FirstOrDefault();

                if (!string.IsNullOrEmpty(aliasedUrl))
                {
                    var trimmedAlias = aliasedUrl.FormatNavigationUrl();
                    navItem.Url = string.Format("/{0}", trimmedAlias);
                }

                if (!string.IsNullOrEmpty(canonicalUrl))
                {
                    // remove trailing slash for navigation to avoid API error
                    navItem.Url = canonicalUrl.FormatNavigationUrl();
                }

                if (pageItem is null)
                {
                    continue;
                }
                result.Add(navItem);
            }

            result.First().IsCurrentPage = true;

            return result;
        }


        private List<NavigationItemDto> GetNavigationItems(IEnumerable<IPublishedContent> pages)
        {
            var result = new List<NavigationItemDto>();
            foreach (var page in pages)
            {
                NavigationItemDto navItem = null;
                var aliasedUrl = string.Empty;
                var canonicalUrl = string.Empty;
                var pageItem = page as Page;
                
                if (pageItem is null)
                {
                    var doorVisualiserPage = page as DoorVisualiserLandingPage;
                    navItem = _mapper.Map<NavigationItemDto>(doorVisualiserPage);
                    canonicalUrl = doorVisualiserPage.CanonicalUrl;
                    aliasedUrl = doorVisualiserPage.UmbracoUrlAlias.Split(',').FirstOrDefault();
                } 
                else
                {
                    navItem = _mapper.Map<NavigationItemDto>(page);
                    canonicalUrl = pageItem.CanonicalUrl;
                    aliasedUrl = pageItem.UmbracoUrlAlias.Split(',').FirstOrDefault();
                }


                if(!string.IsNullOrEmpty(aliasedUrl))
                {
                    var trimmedAlias = aliasedUrl.FormatNavigationUrl();
                    navItem.Url = string.Format("/{0}", trimmedAlias);
                }

                if (!string.IsNullOrEmpty(canonicalUrl))
                {
                    // remove trailing slash for navigation to avoid API error
                    navItem.Url = canonicalUrl.FormatNavigationUrl();
                }

                if (page.HasChildPages())
                {
                    var children = page.Children
                        .Where(c => c.IsPage() && c.IncludeInMegamenu());
                    navItem.Children = GetNavigationItems(children);
                }

                if (navItem.ViewAllCta is not null)
                {
                    navItem.ViewAllCta = navItem.ViewAllCta.CheckAlias(_umbracoContextAccessor);
                }

                result.Add(navItem);
            }
            return result;
        }
        private ImageDto? GetSiteLogo()
        {
            ImageDto? result = null;

            if (_publishedContentQuery
               .ContentAtRoot()
               .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is not Website rootItem)
            {
                return result;
            }

            var siteLogo = rootItem.SiteLogo;

            if (siteLogo is not null)
            {
                var image = _umbracoContextAccessor.GetRequiredUmbracoContext().Media.GetById(siteLogo.Id) as Image;
                result = _mapper.Map<ImageDto>(image);
            }

            return result;
        }
        private ImageDto? GetShowroomImage()
        {
            ImageDto? result = null;
            if (_publishedContentQuery
               .ContentAtRoot()
               .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is not Website rootItem)
            {
                return result;
            }

            var showroomImage = rootItem.ShowroomLinkImage;
            if (showroomImage is not null)
            {
                result = _mapper.Map<ImageDto>(showroomImage.Content.SafeCast<UmbracoMediaVectorGraphics>());
            }
            return result;
        }

        private CtaDto? GetShowroomLink()
        {
            CtaDto? result = null;
            if (_publishedContentQuery
                .ContentAtRoot()
                .FirstOrDefault(i => i.ContentType.Alias.Equals(Website.ModelTypeAlias)) is not Website rootItem)
            {
                return result;
            }

            var showroomLink = rootItem.ShowroomLink;

            if (showroomLink is not null)
            {
                result = _mapper.Map<CtaDto>(showroomLink);
            }

            return result;
        }

        private NavigationDto? GetSearchConfiguration(NavigationDto navigationModel)
        {
            if (navigationModel is null)
            {
                return null;
            }
            var result = navigationModel;
            var rootNode = _umbracoContextAccessor
               .GetRequiredUmbracoContext()
               .Content
               .GetByXPath("//website")
               .First() as Website;
            if (rootNode is null)
                return result;

            Uri? isValidUrl = null;
            if (Uri.TryCreate(rootNode.SearchResultsPage.Url, UriKind.Absolute, out isValidUrl))
            {
                result.SearchResultPage = isValidUrl is not null ? isValidUrl.PathAndQuery : rootNode.SearchResultsPage.Url;
            }

            result.SearchPlaceholderText = rootNode.SearchPlaceholderText;
            result.SearchPlacholderTextMobile = rootNode.MobileSearchPlaceholderText;
            return result;
        }

        
    }
}
