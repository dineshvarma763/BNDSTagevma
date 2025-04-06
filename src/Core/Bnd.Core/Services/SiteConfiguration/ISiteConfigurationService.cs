namespace Bnd.Core.Services.SiteConfiguration
{
    using Bnd.DTO.Models;
    public interface ISiteConfigurationService
    {
        SiteConfigurationDto GetSiteConfiguration();
        string? GetRobots();
    }
}
