using Bnd.DTO.Models.Cache;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;


namespace Bnd.Core.Services.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly ILogger<CacheService> _logger;        
        private static readonly List<string> entries = new();

        public CacheService(IMemoryCache memoryCache, ILogger<CacheService> logger)
        {
            _memoryCache = memoryCache;
            _logger = logger;
        }

        public async Task<string> GetCache(string key)
        {
            
            var result = string.Empty;
            _logger.LogInformation($"Rertrieving cache for: {key}", this);
            if (string.IsNullOrEmpty(key))
                return result;

            if(_memoryCache.TryGetValue(key, out string dealers))
            {
                result = dealers;
            }

            return result;
        }

        public async Task<string> UpdateCache(DealerCache dealer)
        {
            var result = string.Empty;
            _logger.LogInformation($"Updating cache for {dealer.CacheKey}", this);
            if (dealer is not null)
            {
                var serializedData = JsonConvert.SerializeObject(dealer.Dealers);
               
                // Define cache entry options with expiration
                var cacheEntryOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(6)// Expires in 6 hour
                };

                // Add key to tracked entries if it doesn't already exist
                if (!entries.Contains(dealer.CacheKey))
                {
                    entries.Add(dealer.CacheKey);
                }

                _memoryCache.Set(dealer.CacheKey, JsonConvert.SerializeObject(dealer.Dealers), cacheEntryOptions);
                result = JsonConvert.SerializeObject(_memoryCache.Get(dealer.CacheKey));
            }
            return result;
        }

        public void PurgeCache(string key)
        {
            if (string.IsNullOrEmpty(key))
                return;
            _logger.LogInformation($"Purging cache for {key}", this);
            _memoryCache.Remove(key);

            // Remove the key from the entries list if it exists
            entries.Remove(key);
        }

        public void PurgeAllCache()
        {
            _logger.LogInformation("Purging all cache entries", this);
            if(entries.Count > 0)
            {
                foreach (var key in entries)
                {
                    _memoryCache.Remove(key);
                }

                entries.Clear();
            }
      
        }


    }
}
