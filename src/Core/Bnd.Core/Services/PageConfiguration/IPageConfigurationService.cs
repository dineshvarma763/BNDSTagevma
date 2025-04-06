namespace Bnd.Core.Services.PageConfiguration
{
    using Bnd.DTO.Models;
    public interface IPageConfigurationService
    {
        PageConfigurationDto GetPageConfiguration(string pageName, string serverUrl);
        PageConfigurationDto GetErrorPage(string serverUrl);
    }
}
