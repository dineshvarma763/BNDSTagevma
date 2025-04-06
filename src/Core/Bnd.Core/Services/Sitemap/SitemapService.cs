namespace Bnd.Core.Services.Sitemap
{
    using Bnd.Core.Extensions;
    using Microsoft.Extensions.Logging;
    using System;
    using System.Xml.Linq;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Extensions;

    public class SitemapService : ISitemapService
    {
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        private static readonly XNamespace Xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        private readonly ILogger<SitemapService> _logger;
        public SitemapService(IUmbracoContextFactory umbracoContextFactory, ILogger<SitemapService> logger)
        {
            _umbracoContextFactory = umbracoContextFactory;
            _logger = logger;
        }

        public string GetSitemap(string requestUrl)
        {
            _logger.LogInformation($"Sitemap Service: {requestUrl}");

            using var context = _umbracoContextFactory.EnsureUmbracoContext();
            var rootNode = context.UmbracoContext.Content.GetByXPath("//website").First();
            var nodes = rootNode
            .DescendantsOrSelf()
            .Where(x => x.HasTemplate())
            .Where(x => x.IsInSiteMap());
            
            XElement root = new(Xmlns + "urlset");

            foreach (var node in nodes)
            {
                var canonicalUrl = node.Value<string>("canonicalUrl");
                var aliasUrl = node.Value<string>("umbracoUrlAlias").Split(',').FirstOrDefault();
                //account for home item
                var relativeNodeUrl = node.Url(mode: UrlMode.Relative);
                var nodeUrl = requestUrl + (relativeNodeUrl.Equals("/home/",StringComparison.CurrentCultureIgnoreCase) ? "/" : relativeNodeUrl);

                if(!string.IsNullOrEmpty(aliasUrl))
                {
                    nodeUrl = string.Format("{0}/{1}/", requestUrl, aliasUrl);
                }

                if(!string.IsNullOrEmpty(canonicalUrl))
                {
                    nodeUrl = canonicalUrl;
                }

                var urlElement = new XElement(Xmlns + "url",
                    new XElement(Xmlns + "loc", nodeUrl),
                    new XElement(Xmlns + "lastmod", node.UpdateDate.ToString("yyyy-MM-dd")));

                var changeFreqency = node.Value<string>("searchEngineChangeFrequency");

                if (string.IsNullOrWhiteSpace(changeFreqency) == false)
                {
                    urlElement.Add(new XElement(Xmlns + "changefreq", changeFreqency.ToLower()));
                }

                var priority = node.Value<decimal>("searchEngineRelativePriority");

                if (priority > 0)
                {
                    urlElement.Add(new XElement(Xmlns + "priority", priority));
                }

                root.Add(urlElement);
            }

            XDocument document = new XDocument(root);

            var sitemapXml = document.ToString();

            return sitemapXml;
        }

    }
}
