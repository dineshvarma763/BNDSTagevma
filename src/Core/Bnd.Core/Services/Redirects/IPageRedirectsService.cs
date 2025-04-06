namespace Bnd.Core.Services.Redirects
{
    using Bnd.DTO.Models;

    public interface IPageRedirectsService
    {
        Task<RedirectsDto> GetRedirects(string pagePath);
    }
}
