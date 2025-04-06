namespace Bnd.Core.Extensions
{
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;

    public static class PublishedContentExtensions
    {
        public static bool HasTemplate(this IPublishedContent content) => content.ContentType.Alias.Equals(Home.ModelTypeAlias) || content.ContentType.Alias.Equals(Page.ModelTypeAlias) || content.ContentType.Alias.Equals(DoorVisualiserLandingPage.ModelTypeAlias);

        public static bool IsInSiteMap(this IPublishedContent content) => !content.Value<bool>("hideFromSitemap", defaultValue: false);

        public static bool IncludeInMegamenu(this IPublishedContent content) => content.Value<bool>("includeInMegamenu", defaultValue: false);
        public static bool IsPage(this IPublishedContent content) => content.ContentType.Alias.Equals(Page.ModelTypeAlias) || content.ContentType.Alias.Equals(DoorVisualiserLandingPage.ModelTypeAlias) || content.ContentType.Equals(DoorVisualiserPanelsPage.ModelTypeAlias);
        public static bool IsProductPage(this IPublishedContent content) => content.ContentType.Alias.Equals(Page.ModelTypeAlias) && content.Value<bool>("isProductPage", defaultValue: false);
        public static bool HasChildPages(this IPublishedContent content) => content.Children.Any();

        public static bool EnableOnThisPage(this IPublishedContent content) => content.Value<bool>("enableOnThisPage", defaultValue: false);
        public static string OnThisPageLabel(this IPublishedContent content) => content.Value<string>("onThisPageLabel", defaultValue: string.Empty);
        public static string OnThisPageAnchor(this IPublishedContent content) => content.Value<string>("onThisPageAnchor", defaultValue: string.Empty);
        public static string AliasUrl(this IPublishedContent content) => content.Value<string>("umbracoUrlAlias", defaultValue: string.Empty);
    }
}
