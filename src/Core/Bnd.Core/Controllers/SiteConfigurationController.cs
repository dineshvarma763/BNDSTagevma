namespace Bnd.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Umbraco.Cms.Web.Common.Controllers;
    using Umbraco.Cms.Web.Common.Attributes;
    using Bnd.Core.Services.SiteConfiguration;
    using Bnd.Core.Services.Sitemap;
    using System.Text;
    using static Bnd.Core.Extensions.ResponseExtension;
    using Microsoft.AspNetCore.Authorization;
    using Umbraco.Cms.Web.Common.Authorization;

    [PluginController("v1")]
    public class SiteConfigurationController : UmbracoApiController
    {
        private readonly ISiteConfigurationService _siteConfigurationService;
        private readonly ISitemapService _sitemapService;
        private readonly ILogger<SiteConfigurationController> _logger;
        public SiteConfigurationController(ISiteConfigurationService siteConfigurationService, ISitemapService sitemapService, ILogger<SiteConfigurationController> logger)
        {
            _siteConfigurationService = siteConfigurationService;
            _sitemapService = sitemapService;
            _logger = logger;
        }

        public JsonResult GetSiteConfiguration()
        {
            try
            {
                var response = _siteConfigurationService.GetSiteConfiguration();
                return new JsonResult(FormatResponse(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null", ex.Message));
            }
        }

        public string? GetRobotsTxt()
        {
            try
            {
                var response = _siteConfigurationService.GetRobots();
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return string.Empty;
            }
        }

        public ContentResult GetSitemapXml()
        {
            try
            {
                var request = HttpContext.Request.Headers.ContainsKey("X-BND-FE-Host") 
                    ? HttpContext.Request.Headers["X-BND-FE-Host"] :
                    HttpContext.Request.Headers.Origin;
                return Content(_sitemapService.GetSitemap(request), "text/string", Encoding.UTF8);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return Content(string.Empty, "text/string", Encoding.UTF8);
            }
        }

    }
}
