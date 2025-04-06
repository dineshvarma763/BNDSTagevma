namespace Bnd.Core.Indexing.Redirects
{
    using Bnd.Core.Extensions;
    using Bnd.DTO.Models;
    using Examine;
    using Bnd.Core.Models;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Cms.Core.Web;
    using Umbraco.Cms.Infrastructure.Examine;
    using Umbraco.Extensions;
    using UrlTracker.Core;

    public class RedirectsIndexPopulator : IndexPopulator
    {
        private readonly IRedirectService _redirectsService;
        private readonly RedirectsIndexValueBuilder _redirectsIndexValueBuilder;
        private readonly IUmbracoContextFactory _umbracoContextFactory;
        public RedirectsIndexPopulator(IRedirectService redirectsService, RedirectsIndexValueBuilder redirectsIndexValueBuilder, IUmbracoContextFactory umbracoContextFactory)
        {
            _redirectsService = redirectsService;
            _redirectsIndexValueBuilder = redirectsIndexValueBuilder;
            _umbracoContextFactory = umbracoContextFactory;
            RegisterIndex(Constants.RedirectsIndexLabel);
        }


        protected override void PopulateIndexes(IReadOnlyList<IIndex> indexes)
        {
            foreach (var index in indexes)
            {
                var redirects = GetAllRedirects();
                index.IndexItems(_redirectsIndexValueBuilder.GetValueSets(redirects));
            }
        }

        private RedirectsDto[] GetAllRedirects()
        {

            var result = new List<RedirectsDto>();
            using (var umbCxt = _umbracoContextFactory.EnsureUmbracoContext())
            {
                var allRedirects = Task.Run(async () => await _redirectsService.GetAsync()).GetAwaiter().GetResult();

                if (allRedirects is null || !allRedirects.Any())
                    return result.ToArray();

                var id = 0;
                foreach (var redirect in allRedirects)
                {
                    var targetUrl = redirect.TargetUrl;
                    var targetRelativePath = string.Empty;
                    if (!string.IsNullOrEmpty(targetUrl))
                    {
                        targetRelativePath = $"{targetUrl}/";
                    }
                    else
                    {
                        var targetNode = redirect.TargetNode;
                        targetRelativePath = targetNode is null ? string.Empty : targetNode.Url(null, UrlMode.Relative).FormatNavigationUrl();
                    }

                    var redirectDto = new RedirectsDto()
                    {
                        Id = id.ToString(),
                        HasRedirect = true,
                        OldUrl = redirect.SourceUrl,
                        NewUrl = targetRelativePath,
                        Type = redirect.TargetStatusCode.ToString()
                    };
                    result.Add(redirectDto);
                    id++;
                }
            }
            return result.ToArray();
        }
    }
}
