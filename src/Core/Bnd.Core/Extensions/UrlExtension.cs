namespace Bnd.Core.Extensions
{
    using Bnd.DTO.Models;
    using HtmlAgilityPack;
    using Microsoft.AspNetCore.Http;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Cms.Web.Common.PublishedModels;
    using Umbraco.Extensions;

    public static class UrlExtension
    {
        public static string GetDomain(HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.PathBase}";
        }

        public static string TrimUrl(this string absoluteUrl)
        {
            var result = absoluteUrl;
            // process if full URL is included
            if (absoluteUrl.ToLower().StartsWith("https") || absoluteUrl.ToLower().StartsWith("http"))
            {
                var uri = new Uri(absoluteUrl);
                result = uri.PathAndQuery;
            }
            return result;
        }

        public static bool HasMatchingAlias(this string umbracoUrlAliases, string pageName)
        {
            var aliasPagePath = pageName.TrimStart('/');
            if (!umbracoUrlAliases.Contains(','))
                return umbracoUrlAliases.Equals(aliasPagePath);

            var aliasList = umbracoUrlAliases.Split(',').ToList();

            return aliasList.Contains(aliasPagePath);
        }

        public static string FormatNavigationUrl(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;
            return !url.EndsWith('/') ? $"{url}/" : url;
        }

        public static string FormatRootRelativeLink(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;
            return !url.StartsWith('/') ? $"/{url}" : url;
        }

        public static string RemoveTrailingSlash(this string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;
            return url.EndsWith('/') ? url[..(url.Length - 1)] : url;
        }

        public static CtaDto? CheckAlias(this CtaDto ctaDto, IUmbracoContextAccessor umbracoContextAccessor)
        {
            if (ctaDto is null || ctaDto.Url.StartsWith("http") || ctaDto.Url.StartsWith("https") || ctaDto.Url.Contains("tel:") || ctaDto.Url.StartsWith("#"))
                return ctaDto;

            IPublishedContent targetPage;
            if (ctaDto.Url.Equals("/"))
            {
                targetPage = umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByRoute($"/{Home.ModelTypeAlias}");
            }
            else
            {
                targetPage = umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByRoute(ctaDto?.Url?.RemoveTrailingSlash());
            }

            if (targetPage is null)
                return ctaDto;

            var aliases = targetPage.AliasUrl();

            if (string.IsNullOrEmpty(aliases))
                return ctaDto;

            var aliasList = aliases.Split(',');

            if (!aliasList.Any())
                return ctaDto;

            var primaryAlias = aliasList.First();

            if (string.IsNullOrEmpty(primaryAlias))
                return ctaDto;

            var formattedUrl = primaryAlias.FormatNavigationUrl();
            ctaDto.Url = formattedUrl.StartsWith("/") ? formattedUrl : $"/{formattedUrl}";

            return ctaDto;
        }

        public static string? FormatRelativeUrl(this string fullUrl)
        {
            if (fullUrl.Contains("tel:"))
                return fullUrl;
            //Exclude media items
            if (fullUrl.Contains("media"))
                return fullUrl;
            //Exclude external Urls
            if (!fullUrl.Contains("admin") && !fullUrl.Contains("api") && !fullUrl.Contains("localhost"))
                return fullUrl;

            var result = string.Empty;

            try
            {
                var fullUri = new Uri(fullUrl);
                if (fullUri is not null)
                {

                    result = fullUri.PathAndQuery;
                }
            }
            catch (Exception ex)
            {
                result = fullUrl;
            }

            return result;
        }

        public static string CheckRichTextAlias(this string html, IUmbracoContextAccessor umbracoContextAccessor)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var anchors = doc.DocumentNode.SelectNodes("//a[@href]");

            if (anchors is null || !anchors.Any())
                return doc.DocumentNode.InnerHtml;

            foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//a[@href]"))
            {
                HtmlAttribute att = img.Attributes["href"];

                //exclude media items as they should be absolute
                if (att.Value.Contains("media"))
                    continue;
                if (att.Value.Contains("api") || att.Value.Contains("admin") || att.Value.Contains("-admin") || att.Value.Contains("admin.bnd"))
                {
                    var absoluteUrl = new Uri(att.Value);
                    var relativeUrl = absoluteUrl.PathAndQuery;

                    if (string.IsNullOrEmpty(relativeUrl))
                        continue;

                    att.Value = relativeUrl.CheckStringAlias(umbracoContextAccessor).StartsWith('/') ? relativeUrl?.CheckStringAlias(umbracoContextAccessor) : "/" + relativeUrl?.CheckStringAlias(umbracoContextAccessor);
                }
            }

            return doc.DocumentNode.InnerHtml;
        }

        public static string CheckLazyLoading(this string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            var images = doc.DocumentNode.SelectNodes("//img");

            if (images is null || !doc.DocumentNode.SelectNodes("//img").Any())
                return doc.DocumentNode.InnerHtml;

            // add lazy loading attribute to the image tags
            foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//img"))
            {
                //exclude media items as they should be absolute
                if (!img.Attributes.Contains("loading"))
                {
                    img.Attributes.Add("loading", "lazy");
                }
            }

            return doc.DocumentNode.InnerHtml;
        }

        public static List<NavigationItemDto> CheckUtilityAlias(this List<NavigationItemDto> utilityItems, IUmbracoContextAccessor umbracoContextAccessor)
        {
            if (!utilityItems.Any())
                return utilityItems;

            foreach (var utilityItem in utilityItems)
            {
                if (utilityItem is null)
                    continue;

                if (string.IsNullOrEmpty(utilityItem.Url))
                    continue;

                if (utilityItem.Url.Contains("tel:"))
                    continue;
                //exclude absolute links
                if (utilityItem.Url.StartsWith("https") || utilityItem.Url.StartsWith("http"))
                    continue;
                //exclude media links
                if (utilityItem.Url.Contains("media"))
                    continue;
                var aliasedUrl = utilityItem.Url.CheckStringAlias(umbracoContextAccessor);
                utilityItem.Url = aliasedUrl.StartsWith("/") ? aliasedUrl : $"/{aliasedUrl}";
            }


            return utilityItems;
        }

        public static IEnumerable<FooterNavigationSectionDto> CheckFooterAlias(this IEnumerable<FooterNavigationSectionDto> footerItems, IUmbracoContextAccessor umbracoContextAccessor)
        {
            if (!footerItems.Any())
                return footerItems;

            foreach(var footerSections in footerItems)
            {
                if (footerSections is null || !footerSections.FooterNavigationItems.Any())
                    continue;

                foreach(var footerNav in footerSections.FooterNavigationItems)
                {

                    if (string.IsNullOrEmpty(footerNav.Url))
                        continue;

                    var url = footerNav.Url.StartsWith("http") ? new Uri(footerNav.Url).PathAndQuery : footerNav.Url;
                    var aliasedUrl = url.CheckStringAlias(umbracoContextAccessor);

                    footerNav.Url = aliasedUrl.StartsWith("/") ? aliasedUrl : $"/{aliasedUrl}";
                }
            }

            return footerItems;
        }

        public static string? CheckStringAlias(this string stringUrl, IUmbracoContextAccessor umbracoContextAccessor)
        {
            IPublishedContent targetPage = null;
            if (stringUrl.Equals("/"))
            {
                targetPage = umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByRoute($"/{Home.ModelTypeAlias}");
            }
            else
            {
                targetPage = umbracoContextAccessor.GetRequiredUmbracoContext().Content.GetByRoute(stringUrl.RemoveTrailingSlash());
            }

            if (targetPage is null)
                return stringUrl;

            var aliases = targetPage.AliasUrl();

            if (string.IsNullOrEmpty(aliases))
                return stringUrl;

            var aliasList = aliases.Split(',');

            if (!aliasList.Any())
                return stringUrl;

            var primaryAlias = aliasList.First();

            if (string.IsNullOrEmpty(primaryAlias))
                return stringUrl;

            stringUrl = primaryAlias
                .FormatNavigationUrl()
                .FormatRootRelativeLink();

            return stringUrl;
        }
    }
}
