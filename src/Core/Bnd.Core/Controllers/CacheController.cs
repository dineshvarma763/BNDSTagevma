namespace Bnd.Core.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Umbraco.Cms.Web.Common.Attributes;
    using Umbraco.Cms.Web.Common.Controllers;
    using Bnd.DTO.Models.Cache;
    using Bnd.Core.Services.Cache;

    [PluginController("v1")]
    public class CacheController : UmbracoApiController
    {
        private readonly ICacheService _cacheService;
        public CacheController(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<string> GetCache([FromQuery] string key)
        {
            return await _cacheService.GetCache(key);
        }

        public async Task<string> UpdateCache([FromBody] DealerCache cache)
        {
            return await _cacheService.UpdateCache(cache);
        }

        public void PurgeCache([FromQuery] string key)
        {
            _cacheService.PurgeCache(key);
        }

        public void PurgeAllCache()
        {
            _cacheService.PurgeAllCache();
        }
    }
}
