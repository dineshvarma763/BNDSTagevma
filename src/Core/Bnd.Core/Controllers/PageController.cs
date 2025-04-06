namespace Bnd.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Logging;
    using Umbraco.Cms.Web.Common.Attributes;
    using Umbraco.Cms.Web.Common.Controllers;
    using Bnd.Core.Services.PageConfiguration;
    using Bnd.Core.Services.Redirects;
    using Bnd.Core.Services.SiteConfiguration;
    using Bnd.Core.Services.Navigation;
    using static Bnd.Core.Extensions.UrlExtension;
    using static Bnd.Core.Extensions.ResponseExtension;
    using Bnd.DTO.Models;

    [PluginController("v1")]
    public class PageController : UmbracoApiController
    {
        private readonly IPageConfigurationService _pageConfigurationService;
        private readonly IPageRedirectsService _pageRedirectsService;
        private readonly ISiteConfigurationService _siteConfigurationService;
        private readonly INavigationService _navigationService;
        private readonly ILogger<PageController> _logger;
        public PageController(IPageConfigurationService pageConfigurationService, IPageRedirectsService pageRedirectsService, ISiteConfigurationService siteConfigurationService, INavigationService navigationService, ILogger<PageController> logger)
        {
            _pageConfigurationService = pageConfigurationService;
            _pageRedirectsService = pageRedirectsService;
            _siteConfigurationService = siteConfigurationService;
            _navigationService = navigationService;
            _logger = logger;
        }

        public JsonResult GetPageConfiguration([FromQuery] string pageName)
        {
            try
            {
                pageName = pageName.FormatNavigationUrl();
                var domain = GetDomain(HttpContext);
                var siteConfiguration = _siteConfigurationService.GetSiteConfiguration();
                var response = _pageConfigurationService.GetPageConfiguration(pageName, domain);


                if (_navigationService.GetFooter() != null)
                    response.Footer = _navigationService.GetFooter();
                response.Navigation = _navigationService.GetNavigation();


                response.EyebrowBannerLink = siteConfiguration.EyebrowBannerLink;
                response.EyebrowBannerText = siteConfiguration.EyebrowBannerText;
                response.EnableEyebrowBanner = siteConfiguration.EnableEyebrowBanner;

                response.GlobalHeaderScripts = siteConfiguration.HeaderScripts;
                response.GlobalBodyClosingScripts = siteConfiguration.BeforeBodyClosingScripts;

                return new JsonResult(FormatResponse(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null", ex.Message));
            }

        }

        public async Task<JsonResult> GetRedirect([FromQuery] string pageName)
        {
            try
            {
                var response = await _pageRedirectsService.GetRedirects(pageName);
                return new JsonResult(FormatResponse(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null", ex.Message));
            }
        }

        public JsonResult GetErrorPage()
        {
            try
            {
                var domain = GetDomain(HttpContext);
                var siteConfiguration = _siteConfigurationService.GetSiteConfiguration();
                var response = _pageConfigurationService.GetErrorPage(domain);
                response.Footer = _navigationService.GetFooter();
                response.Navigation = _navigationService.GetNavigation();
                response.GlobalHeaderScripts = siteConfiguration.HeaderScripts;
                response.GlobalBodyClosingScripts = siteConfiguration.BeforeBodyClosingScripts;
                return new JsonResult(FormatResponse(response));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null", ex.Message));
            }
        }
    }
}
