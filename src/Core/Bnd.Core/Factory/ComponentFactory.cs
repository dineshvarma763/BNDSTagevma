namespace Bnd.Core.Factory
{
    using AutoMapper;
    using Bnd.Core.Extensions;
    using Bnd.DTO.Models;
    using System.Linq;
    using Newtonsoft.Json;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;
    using Bnd.Core.Models;

    public class ComponentFactory : IComponentFactory
    {
        private readonly Dictionary<string, Delegate> _componentList;
        private readonly IUmbracoContextAccessor _umbracoContextAccessor;
        private readonly IMapper _mapper;
        public ComponentFactory(IUmbracoContextAccessor umbracoContextAccessor, IMapper mapper)
        {
            _umbracoContextAccessor = umbracoContextAccessor;
            _mapper = mapper;
            _componentList = new Dictionary<string, Delegate>()
            {
                { BasicContent.ModelTypeAlias, new Func<IPublishedContent, object?>(GetBasicContent) },
                { HeroBanner.ModelTypeAlias, new Func<IPublishedContent, object?>(GetHeroBanner) },
                { AnimatedBanner.ModelTypeAlias, new Func<IPublishedContent, object?>(GetAnimatedBanner) },
                { Accordion.ModelTypeAlias, new Func<IPublishedContent, object?>(GetAccordion) },
                { TabbedContent.ModelTypeAlias, new Func<IPublishedContent, object?>(GetTabbedContent) },
                { CardListing.ModelTypeAlias, new Func<IPublishedContent, object?>(GetCardListing) },
                { CardList.ModelTypeAlias, new Func<IPublishedContent, object?>(GetCardList) },
                { SearchResult.ModelTypeAlias, new Func<IPublishedContent, object?>(GetSearchResult) },
                { Video.ModelTypeAlias, new Func<IPublishedContent, object?>(GetInlineVideo) },
                { InlineBanner.ModelTypeAlias, new Func<IPublishedContent, object?>(GetInlineBanner) },
                { ZigZag.ModelTypeAlias, new Func<IPublishedContent, object?>(GetZigZag)  },
                { OnThisPage.ModelTypeAlias, new Func<IPublishedContent, object?>(GetOnThisPage) },
                { ResourceHub.ModelTypeAlias, new Func<IPublishedContent, object?>(GetResourceHub) },
                { TestimonialCarousel.ModelTypeAlias, new Func<IPublishedContent, object?>(GetTestimonialCarousel) },
                { SmallCarousel.ModelTypeAlias, new Func<IPublishedContent, object?>(GetSmallCarousel) },
                { FeatureCarousel.ModelTypeAlias, new Func<IPublishedContent, object?>(GetFeatureCarousel) },
                { ImageGallery.ModelTypeAlias, new Func<IPublishedContent, object?>(GetImageGallery) },
                { DealerMap.ModelTypeAlias, new Func<IPublishedContent, object?>(GetDealerMap) },
                { FindAdealer.ModelTypeAlias, new Func<IPublishedContent, object?>(GetFindADealer) },
                { ProductFilters.ModelTypeAlias, new Func<IPublishedContent, object?>(GetProductFilters) },
                { Iframe.ModelTypeAlias, new Func<IPublishedContent, object?>(GetIframe) },
                { LookingFor.ModelTypeAlias, new Func<IPublishedContent, object?>(GetLookingFor) },
                { CampaignHeroBanner.ModelTypeAlias, new Func<IPublishedContent, object?>(GetCampaignHeroBanner) },
                { CampaignBanner.ModelTypeAlias, new Func<IPublishedContent, object?>(GetCampaignBanner) },
            };
        }

        public IEnumerable<string> GetAvailableComponents()
        {
            return _componentList.Keys;
        }

        public object? GetComponent(string componentName, IPublishedContent component)
        {
            return _componentList[componentName].DynamicInvoke(component);
        }

        private object? GetBasicContent(IPublishedContent component)
        {
            var basicContentComponent = component as BasicContent;
            var result = _mapper.Map<BasicContentDto>(basicContentComponent);

            // Consider alias
            if (result is not null && result.Cta is not null)
            {
                result.Cta = result.Cta.CheckAlias(_umbracoContextAccessor);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.HtmlContent))
            {
                result.HtmlContent = result.HtmlContent.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }
        private object? GetIframe(IPublishedContent component)
        {
            var iframeComponent = component as Iframe;
            var result = _mapper.Map<IframeDto>(iframeComponent);

            return result;
        }

        private object? GetHeroBanner(IPublishedContent component)
        {
            var heroBannerComponent = component as HeroBanner;
            var result = _mapper.Map<HeroBannerDto>(heroBannerComponent);

            // Consider alias
            if (result.Links is not null)
                result.Links = GetAliasedConfigurableLinks(result.Links);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetAnimatedBanner(IPublishedContent component)
        {
            var animatedBannerComponent = component as AnimatedBanner;
            var result = _mapper.Map<AnimatedBannerDto>(animatedBannerComponent);

            if (result is not null && result.Cta is null)
                return result;

            // Consider alias
            if (result.Cta.Link is null)
                return result;

            result.Cta.Link = result.Cta.Link.CheckAlias(_umbracoContextAccessor);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetAccordion(IPublishedContent component)
        {
            var accordionComponent = component as Accordion;
            var result = _mapper.Map<AccordionDto>(accordionComponent);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor);
            }

            if(result.AccordionContent.Any())
            {
                foreach(var accordionContent in result.AccordionContent)
                {
                    // Consider relative vs aliased url
                    if (!string.IsNullOrEmpty(accordionContent.Content))
                    {
                        accordionContent.Content = accordionContent.Content.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
                    }
                }
            }

            return result;
        }

        private object? GetTabbedContent(IPublishedContent component)
        {
            var tabbedContentComponent = component as TabbedContent;
            var result = _mapper.Map<TabbedContentDto>(tabbedContentComponent);

            // Cconsider tab panel relative vs aliased url
            if(result is not null && result.TabPanels.Any())
            {
                foreach(var panel in result.TabPanels)
                {
                    // Consider relative vs aliased url
                    if (!string.IsNullOrEmpty(panel.TabContent))
                    {
                        panel.TabContent = panel.TabContent.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
                    }
                }
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetCardListing(IPublishedContent component)
        {
            var cardListingComponent = component as CardListing;
            var result = _mapper.Map<CardListingDto>(cardListingComponent);

            if (result is not null && result.ListingContents is null)
                return result;

            // Consider alias
            foreach (var listingItem in result.ListingContents)
            {
                if (string.IsNullOrEmpty(listingItem.ListingLink))
                    continue;

                listingItem.ListingLink = listingItem.ListingLink.CheckStringAlias(_umbracoContextAccessor);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetCardList(IPublishedContent component)
        {
            var cardListComponent = component as CardList;
            var result = _mapper.Map<CardListDto>(cardListComponent);
            
            if(result.Links is not null)
                // Consider alias for component cta
                result.Links = GetAliasedConfigurableLinks(result.Links);

            if (result.CardItems is null)
                return result;

            // Consider card links for alias
            foreach(var cardItem in result.CardItems)
            {
                if (cardItem.ConfigurableLink is null || cardItem.ConfigurableLink.Link is null)
                    continue;
                cardItem.ConfigurableLink.Link = cardItem.ConfigurableLink.Link.CheckAlias(_umbracoContextAccessor);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetInlineVideo(IPublishedContent component)
        {
            var videoComponent = component as Video;
            var result = _mapper.Map<InlineVideoDto>(videoComponent);
            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }
            return result;
        }

        private object? GetInlineBanner(IPublishedContent component)
        {
            var inlineBannerComponent = component as InlineBanner;
            var result = _mapper.Map<InlineBannerDto>(inlineBannerComponent);

            if (result.Links is not null)
                // Consider alias for component cta
                result.Links = GetAliasedConfigurableLinks(result.Links);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetZigZag(IPublishedContent component)
        {
            var zigZagComponent = component as ZigZag;
            var result = _mapper.Map<ZigZagDto>(zigZagComponent);

            if (result.ZigZagItems is null)
                return result;

            foreach(var zigZagItem in result.ZigZagItems)
            {
                if (zigZagItem.CardLinks is null)
                    continue;

                // Consider relative vs aliased url
                if (!string.IsNullOrEmpty(zigZagItem.CardDescription))
                {
                    zigZagItem.CardDescription = zigZagItem.CardDescription.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
                }

                zigZagItem.CardLinks = GetAliasedConfigurableLinks(zigZagItem.CardLinks);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetOnThisPage(IPublishedContent component)
        {
            var onThisPageComponent = component as OnThisPage;
            var result = _mapper.Map<OnThisPageDto>(onThisPageComponent);

            var parent = component.Parent as Components;
            if (parent is null)
                return result;

            var filteredChildren = parent.Children.Where(s => s.ContentType.Alias != OnThisPage.ModelTypeAlias);
            if (!filteredChildren.Any())
                return result;
            var onthisPageEnabledComponents = filteredChildren.Where(s => s.EnableOnThisPage());
            if (!onthisPageEnabledComponents.Any())
                return result;
            result.Items = _mapper.Map<IEnumerable<OnThisPageItemDto>>(onthisPageEnabledComponents);

            return result;
        }
        private object? GetResourceHub(IPublishedContent component)
        {
            var resourceHubComponent = component as ResourceHub;
            var result = _mapper.Map<ResourceHubDto>(resourceHubComponent);

            if (resourceHubComponent.ResourceHubRoot is null)
            {
                return result;
            }

            var childPages = resourceHubComponent.ResourceHubRoot
                .Descendants()
                .Where(x => x.IsPage());

            if (childPages is null)
            {
                return result;
            }
            var pages = childPages.Cast<Page>();

            if (pages is null)
            {
                return result;
            }
            result.CardItems = _mapper.Map<IEnumerable<CardListingItemDto>>(pages);
            result.CardItems = result.CardItems.OrderByDescending(c => c.ListingCreatedDate);

            // Builds filters based on the content list
            var filters = new List<object>();
            var tagConfig = new object();
            var contentTypes = pages.Where(page => page.ContentType is not null)
                .SelectMany(page => page.ContentType).Distinct();
            var topics = pages.Where(page => page.Topic is not null)
                .SelectMany(page => page.Topic).Distinct();

            if (topics.Any())
            {
                var topicFilterLabels = new List<object>();
                foreach (var topic in topics)
                {
                    topicFilterLabels.Add(new
                    {
                        label = topic.Name
                    });
                }
                filters.Add(new
                {
                    label = "topic",
                    tag = "topic",
                    children = topicFilterLabels
                });
            }

            if (contentTypes.Any())
            {
                var contentTypeFilterLabels = new List<object>();
                foreach (var contentType in contentTypes)
                {
                    contentTypeFilterLabels.Add(new
                    {
                        label = contentType.Name
                    });
                }
                filters.Add(new
                {
                    label = "content type",
                    tag = "contentType",
                    children = contentTypeFilterLabels
                });
            }

            result.FilterConfig = JsonConvert.SerializeObject(filters);

            if (result.CardItems is null)
                return result;

            foreach(var card in result.CardItems)
            {
                if (string.IsNullOrEmpty(card.ListingLink))
                    continue;
                card.ListingLink = card.ListingLink.CheckStringAlias(_umbracoContextAccessor);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetSmallCarousel(IPublishedContent component)
        {
            var smallCarouselComponent = component as SmallCarousel;
            if (smallCarouselComponent is null)
                return null;
            var result = _mapper.Map<SmallCarouselDto>(smallCarouselComponent);

            // Consider alias
            if (result.Link is null)
                return result;

            result.Link = GetAliasedConfigurableLinks(result.Link);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetImageGallery(IPublishedContent component)
        {
            var imageGalleryComponent = component as ImageGallery;
            if (imageGalleryComponent is null)
                return null;
            var result = _mapper.Map<ImageGalleryDto>(imageGalleryComponent);

            if (imageGalleryComponent.Images is null || !imageGalleryComponent.Images.Any())
                return result;

            var images = imageGalleryComponent.Images;
            var mediaImageItems = new List<Image>();
            foreach (var image in images)
            {
                var media = _umbracoContextAccessor.GetRequiredUmbracoContext().Media.GetById(image.Content.Id) as Image;
                if (media is null)
                    continue;
                mediaImageItems.Add(media);
            }

            result.Images = _mapper.Map<IEnumerable<ImageDto>>(mediaImageItems);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetTestimonialCarousel(IPublishedContent component)
        {
            var testimonialCarouselComponent = component as TestimonialCarousel;
            if (testimonialCarouselComponent is null)
                return null;

            var result = _mapper.Map<TestimonialCarouselDto>(testimonialCarouselComponent);

            // Consider alias
            if (result.Link is null)
                return result;

            result.Link = GetAliasedConfigurableLinks(result.Link);


            return result;
        }

        private object? GetFeatureCarousel(IPublishedContent component)
        {
            var featureCarouselComponent = component as FeatureCarousel;
            if (featureCarouselComponent is null)
                return null;

            var result = _mapper.Map<FeatureCarouselDto>(featureCarouselComponent);

            if (featureCarouselComponent.ProductLandingPage is null)
            {
                result.Panels = _mapper.Map<IEnumerable<FeatureCarouselPanelDto>>(featureCarouselComponent.Panels);
            }
            else
            {
                var productLandingPage = featureCarouselComponent.ProductLandingPage as Page;
                var productPages = productLandingPage.Descendants().Where(p => p.IsProductPage()).Cast<Page>();
                result.Panels = _mapper.Map<IEnumerable<FeatureCarouselPanelDto>>(productPages);
            }


            // Consider alias
            if (result.Panels is null)
                return result;

            foreach(var panel in result.Panels)
            {
                if (panel.Link is null)
                    continue;
                panel.Link = GetAliasedConfigurableLinks(panel.Link);

                // Consider feature carousel panel
                if (!string.IsNullOrEmpty(panel.Description))
                {
                    panel.Description = panel.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
                }
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetDealerMap(IPublishedContent component)
        {
            var dealerMapComponent = component as DealerMap;
            if (dealerMapComponent is null)
                return null;

            var result = _mapper.Map<DealerMapDto>(dealerMapComponent);

            if (dealerMapComponent.Dealer is null)
                return result;

            var dealer = dealerMapComponent.Dealer as Dealer;
            if (dealer is not null)
                result.Dealer = _mapper.Map<DealerDto>(dealer);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Dealer.OpeningHours))
            {
                result.Dealer.OpeningHours = result.Dealer.OpeningHours.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetFindADealer(IPublishedContent component)
        {
            var findADealerComponent = component as FindAdealer;
            if (findADealerComponent is null)
                return null;

            var result = _mapper.Map<FindADealerDto>(findADealerComponent);

            if (result is null)
            {
                return result;
            }

            var apiDomain = _umbracoContextAccessor.GetRequiredUmbracoContext().OriginalRequestUrl.Host;

            result.ApiEndpointsConfig = JsonConvert.SerializeObject(new
            {
                get = Constants.AllDealersEndpoint,
                mobileShowrooms = Constants.MobileShowroomsEndpoint,
                search = Constants.SearchDealersEndpoint,
                suggest = Constants.SuggestDealersEndpoint,
                mobileShowroomLabel = result.MobileShowroomPopupTitle,
                bookNowLink = result.BookNowLink,
                resultMessage = result.ResultText,
                dealerFilters = result.DealerFilters
            });

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            // Consider aliases in mobile showroom popup
            if (!string.IsNullOrEmpty(result.MobileShowroomPopup))
            {
                result.MobileShowroomPopup = result.MobileShowroomPopup.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private object? GetProductFilters(IPublishedContent component)
        {
            var productFilterComponent = component as ProductFilters;
            if (productFilterComponent is null)
                return null;
            var result = _mapper.Map<ProductFiltersDto>(productFilterComponent);

            if (result is null)
                return null;


            var rootNode = _umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByXPath("//website").First();
            var productPages = rootNode
                .DescendantsOrSelf()
                .Where(x => x.IsProductPage());

            var mappedProductPages = productPages.Cast<Page>();

            if (mappedProductPages is null)
            {
                return result;
            }

            result.Items = _mapper.Map<IEnumerable<ProductListingItemDto>>(mappedProductPages);

            if (result.FilterGroups is null || !result.FilterGroups.Any())
                return result;

            var filterConfigs = new List<object>();
            foreach (var filterGroup in result.FilterGroups)
            {
                var filter = new
                {
                    label = filterGroup.Title,
                    children = BuildProductFilters(filterGroup.FilterItems)
                };
                filterConfigs?.Add(filter);
            }

            result.FilterConfig = JsonConvert.SerializeObject(filterConfigs);

            if (result.ProductCategories is not null)
            {
                var categories = result.ProductCategories;
                result.Items = result.Items.Where(i => i.ProductCategories.Any(pc => categories.Contains(pc))).ToList();
            }

            if (result.Items is null)
                return result;


            // Consider alias
            foreach(var item in result.Items)
            {
                if (string.IsNullOrEmpty(item.ListingLink))
                    continue;
                item.ListingLink = item.ListingLink.CheckStringAlias(_umbracoContextAccessor);
            }

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }

        private List<object>? BuildProductFilters(IEnumerable<string> filters)
        {
            var result = new List<object>();
            foreach (var filter in filters)
            {
                var mappedFilter = new
                {
                    label = filter
                };
                result.Add(mappedFilter);
            }
            return result;
        }

        // Start search
        private object? GetSearchResult(IPublishedContent component)
        {
            SearchResultDto? result = null;
            var searchResultComponent = component as SearchResult;

            if (searchResultComponent is null)
            {
                return result;
            }

            result = _mapper.Map<SearchResultDto>(searchResultComponent);

            return result;
        }

        private object GetTopicsFilters(string? topicLabel)
        {
            var result = new object();
            var filters = new List<object>();
            var dataSources = _umbracoContextAccessor?.GetRequiredUmbracoContext()?.Content.GetAtRoot().FirstOrDefault(c => c.ContentType.Alias.Equals(DataFolder.ModelTypeAlias));

            if (dataSources.Children.FirstOrDefault(d => d.ContentType.Alias.Equals(TopicsFolder.ModelTypeAlias)) is not TopicsFolder topicsFolder)
            {
                return result;
            }

            var topics = topicsFolder.Children;

            if (!topics.Any())
            {
                return result;
            }

            foreach (var topic in topics)
            {
                var topicItem = topic as Topic;
                filters.Add(new
                {
                    label = topicItem.Name
                });
            }

            result = new
            {
                label = topicLabel,
                children = filters
            };

            return result;
        }

        private object GetContentTypeFilters(string? contentTypeLabel)
        {
            var result = new object();
            var filters = new List<object>();
            var dataSources = _umbracoContextAccessor?.GetRequiredUmbracoContext()?.Content.GetAtRoot().FirstOrDefault(c => c.ContentType.Alias.Equals(DataFolder.ModelTypeAlias));
            if (dataSources.Children.FirstOrDefault(d => d.ContentType.Alias.Equals(ContentTypesFolder.ModelTypeAlias)) is not ContentTypesFolder contentTypesFolder)
            {
                return result;
            }

            var contentTypes = contentTypesFolder.Children;

            if (!contentTypes.Any())
            {
                return result;
            }

            foreach (var contentType in contentTypes)
            {
                var contentTypeItem = contentType as Category;
                filters.Add(new
                {
                    label = contentTypeItem.Name
                });
            }

            result = new
            {
                label = contentTypeLabel,
                children = filters
            };

            return result;
        }

        // end seaarch


        /// <summary>
        /// Checks for aliases in configurable links
        /// </summary>
        /// <param name="configurableLinks"></param>
        /// <returns></returns>
        private IEnumerable<ConfigurableLinkDto>? GetAliasedConfigurableLinks(IEnumerable<ConfigurableLinkDto> configurableLinks)
        {
            // Consider alias
            foreach (var link in configurableLinks)
            {
                if (link.Link is null)
                    continue;

                link.Link = link.Link.CheckAlias(_umbracoContextAccessor);
            }
            return configurableLinks;
        }

        private object? GetLookingFor(IPublishedContent component)
        {
            var lookingForComponent = component as LookingFor;
            var result = _mapper.Map<LookingForDto>(lookingForComponent);

            return result;
        }

        private object? GetCampaignHeroBanner(IPublishedContent component)
        {
            var campaignheroBannerComponent = component as CampaignHeroBanner;
            var result = _mapper.Map<CampaignHeroBannerDto>(campaignheroBannerComponent);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }
            return result;
        }

        private object? GetCampaignBanner(IPublishedContent component)
        {
            var campaignBannerComponent = component as CampaignBanner;
            var result = _mapper.Map<CampaignBannerDto>(campaignBannerComponent);

            // Consider relative vs aliased url
            if (!string.IsNullOrEmpty(result.Description))
            {
                result.Description = result.Description.CheckRichTextAlias(_umbracoContextAccessor).CheckLazyLoading();
            }

            return result;
        }
    }
}
