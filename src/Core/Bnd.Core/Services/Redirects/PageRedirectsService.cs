namespace Bnd.Core.Services.Redirects
{
    using Bnd.DTO.Models;
    using Bnd.Core.Extensions;
    using UrlTracker.Core;
    using Umbraco.Cms.Core.Models.PublishedContent;
    using Umbraco.Extensions;
    using Examine;
    using Bnd.Core.Models;
    using Microsoft.Extensions.Logging;

    public class PageRedirectsService : IPageRedirectsService
    {
        private readonly IRedirectService _redirectsService;
        private readonly IExamineManager _examineManager;
        private readonly ILogger<PageRedirectsService> _logger;
        public PageRedirectsService(IRedirectService redirectService, IExamineManager examineManager, ILogger<PageRedirectsService> logger)
        {
            _redirectsService = redirectService;
            _examineManager = examineManager;
            _logger = logger;
        }

        public async Task<RedirectsDto> GetRedirects(string pagePath)
        {
            // TODO: Add private method below to retrieve from index, if there is no result, retrieve from the database.
            // Use Examine Lucene index to get the redirects by the page path.
            var fromIndex = await GetFromIndex(pagePath);
            RedirectsDto? result;
            if (fromIndex is not null)
            {
                result = fromIndex;
            }
            else
            {
                result = await GetFromDatabase(pagePath);
            }

            return result;
        }

        /// <summary>
        /// Retrieves a redirect using IExaminManager from the custom redirects index.
        /// </summary>
        /// <param name="pagePath"></param>
        /// <returns></returns>
        private async Task<RedirectsDto?> GetFromIndex(string pagePath)
        {
            var result = new RedirectsDto();
            try
            {
                if (!_examineManager.TryGetIndex(Constants.RedirectsIndexLabel, out IIndex redirectsIndex))
                {
                    _logger.LogError($"No index with the name {Constants.RedirectsIndexLabel} can be found.");
                    return null;
                }

                var redirectResults = redirectsIndex.Searcher.CreateQuery()
                    .Field(Constants.OldUrlField, pagePath)
                    .Execute();

                if (!redirectResults.Any())
                {
                    _logger.LogInformation($"No redirects found for {pagePath}.");
                    return null;
                }

                // Check if it ends with / or does not. All redirects require a check against this. 
                if (!pagePath.EndsWith("/"))
                {
                    pagePath = pagePath + "/";
                }

                // Find exact match
                var firstMatch = redirectResults.FirstOrDefault(x => x.Values[Constants.OldUrlField].Equals(pagePath));

                if (firstMatch is null)
                {
                    _logger.LogInformation($"Unable to map redirect to its model.");
                    return result;
                }

                result = new RedirectsDto()
                {
                    HasRedirect = Convert.ToBoolean(firstMatch.Values[Constants.HasRedirectField]),
                    OldUrl = firstMatch.Values[Constants.OldUrlField],
                    NewUrl = firstMatch.Values[Constants.NewUrlField],
                    Type = firstMatch.Values[Constants.TypeField]
                };
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return null; // Only return null when the service fails. 
            }
            return result;
        }

        /// <summary>
        /// Retrieves a redirect using the page path directly from the redirects database
        /// </summary>
        /// <param name="pagePath"></param>
        /// <returns></returns>
        /// 
        [Obsolete("This method cannot handle excessive queries to the API.  Using it may result in performance issues")]
        private async Task<RedirectsDto> GetFromDatabase(string pagePath)
        {
            var result = new RedirectsDto();
            pagePath = pagePath.FormatNavigationUrl();

            var allRedirects = await _redirectsService.GetAsync();

            if (allRedirects is null || !allRedirects.Any())
                return result;


            var redirectFound = allRedirects.FirstOrDefault(r => r.SourceUrl.Equals(pagePath)) is not null;

            if (!redirectFound)
                return result;

            var redirectedUrl = allRedirects.FirstOrDefault(r => r.SourceUrl.Equals(pagePath));

            if (redirectedUrl is null)
                return result;

            var targetUrl = redirectedUrl.TargetUrl;
            var targetRelativePath = string.Empty;
            // if no redirect URL is supplied, check for the umbraco node
            if (!string.IsNullOrEmpty(targetUrl))
            {
                targetRelativePath = $"{targetUrl}/";
            }
            else
            {
                var targetNode = redirectedUrl.TargetNode;
                targetRelativePath = targetNode.Url(null, UrlMode.Relative).FormatNavigationUrl();
            }

            result = new RedirectsDto()
            {
                HasRedirect = true,
                OldUrl = pagePath,
                NewUrl = targetRelativePath,
                Type = redirectedUrl.TargetStatusCode.ToString()
            };


            return result;
        }
    }
}
