namespace Bnd.DTO.Models
{
    using System.Collections.Generic;

    public class PageBaseDto
    {
        public int? Id { get; set; }
        public string? ApiUrl { get; set; }
        public string? PageTitle { get; set; }
        public string? PageDescription { get; set; }
        public string? PageKeywords { get; set; }
        public string? CanonicalUrl { get; set; }
        public string? PageSiteName { get; set; }
        public string? OgTitle { get; set; }
        public string? OgDescription { get; set; }
        public string? OgKeywords { get; set; }
        public string? OgType { get; set; }
        public ImageDto OgImage { get; set; } = new ImageDto();
        public bool IsVideoPage { get; set; }
        public bool IsProductPage { get; set; }
        public bool UsePageHeading { get; set; }
        public List<string>? Topics { get; set; }
        public List<string>? ContentType { get; set; }
        public List<string>? SearchContentType { get; set; }
        public List<string>? SearchTopics { get; set; }
        public List<string>? ProductCategories { get; set; }
        public List<string>? ProductFilters { get; set; }
        public bool NoIndex { get; set; }
        public bool NoFollow { get; set; }

        public string? HeaderScripts { get; set; }
        public string? BodyClosingScripts { get; set; }

        public string? GlobalHeaderScripts { get; set; }
        public string? GlobalBodyClosingScripts { get; set; }

        public string? Alias { get; set; }

        public bool DisableUtilityAndMegamenu { get; set; }
        public bool DisableEyebrowBanner { get; set; }
    }
}
