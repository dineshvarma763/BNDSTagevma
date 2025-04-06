namespace Bnd.DTO.Models
{
    using Newtonsoft.Json;
    public class SiteConfigurationDto
    {
        public string? SiteName { get; set; }
        public ImageDto? SiteLogo { get; set; }
        public string? RobotsTxt { get; set; }
        public string? HeaderScripts { get; set; }
        public string? BeforeBodyClosingScripts { get; set; }
        public string? SearchAccountId { get; set; }
        public string? SearchCollectionId { get; set; }
        public string? SearchDomain { get; set; }
        public string? SearchPlaceholderText { get; set; }
        public string? ApplicationId { get; set; }
        public string? ApiKey { get; set; }
        public string? SearchIndexName { get; set; }
        public string? QueryIndexName { get; set; }
        public CtaDto? SearchResultsPage { get; set; }
        public string? SearchConfigurationJson
        {
            get
            {
                return JsonConvert.SerializeObject(new
                {
                    searchAccountId = SearchAccountId,
                    searchCollectionId = SearchCollectionId,
                    searchDomain = SearchDomain,
                    applicationId = ApplicationId,
                    apiKey = ApiKey,
                    queryIndexName = QueryIndexName,
                    searchIndexName = SearchIndexName
                });
            }
        }

        public bool EnableEyebrowBanner { get; set; }
        public string? EyebrowBannerText { get; set; }
        public CtaDto? EyebrowBannerLink { get; set; }
    }
}
