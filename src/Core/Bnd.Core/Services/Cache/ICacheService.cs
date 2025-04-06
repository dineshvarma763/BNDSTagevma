using Bnd.DTO.Models.Cache;
namespace Bnd.Core.Services.Cache
{
    public interface ICacheService
    {
        Task<string> GetCache(string key);
        Task<string> UpdateCache(DealerCache dealers);
        void PurgeCache(string key);
        void PurgeAllCache();
    }
}
