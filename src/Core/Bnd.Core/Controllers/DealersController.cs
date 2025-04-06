namespace Bnd.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Web.Common.Attributes;
    using Umbraco.Cms.Web.Common.Controllers;
    using static Bnd.Core.Extensions.UrlExtension;
    using static Bnd.Core.Extensions.ResponseExtension;
    using Bnd.Core.Services.PageConfiguration;
    using Bnd.Core.Services.Redirects;
    using Bnd.Core.Services.SiteConfiguration;
    using Bnd.DTO.Models;
    using Bnd.Core.Services.Navigation;
    using Microsoft.Extensions.Logging;
    using Bnd.Core.Services.Dealers;
    using System.Drawing.Printing;

    [PluginController("v1")]
    public class DealersController : UmbracoApiController
    {
        private readonly ILogger<DealersController> _logger;
        private readonly IDealersService _dealersService;
        public DealersController(ILogger<DealersController> logger, IDealersService dealersService)
        {
            _logger = logger;
            _dealersService = dealersService;
        }

        public JsonResult? Get(double lat, double lng,bool freeMeasureQuote, bool serviceOrRepair, bool commercial)
        {
            try
            {
                var result = _dealersService.GetAllDealers(lat, lng, freeMeasureQuote, serviceOrRepair, commercial);
                return new JsonResult(FormatResponse(result.Result));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null"));
            }
        }

        public JsonResult? Search([FromQuery] string q)
        {
            try
            {
                var result = _dealersService.SearchDealers(q, -37.8017621, 145.1268438,false,false,false);
                return new JsonResult(FormatResponse(result));
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new JsonResult(FormatResponse("null"));
            }
        }

        public IEnumerable<string>? Suggest([FromQuery] string q)
        {
            
            try
            {
                var result = _dealersService.GetSuggestedDealers(q);

                return result;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message, this);
                return new List<string>();
            }
        }
    }
}
