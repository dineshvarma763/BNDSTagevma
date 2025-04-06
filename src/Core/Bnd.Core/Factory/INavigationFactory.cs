namespace Bnd.Core.Factory
{
    using Bnd.DTO.Models;
    using Umbraco.Cms.Web.Common.PublishedModels;

    public interface INavigationFactory
    {
        NavigationDto? GetNavigation();
        FooterDto? GetFooter();
        List<NavigationItemDto>? GetBreadcrumbs(Page page);
    }
}
