namespace Bnd.Core.Services.Sitemap
{
    using Bnd.Core.Models;
    public interface ISitemapService
    {
        string GetSitemap(string requestUrl);
    }
}
