namespace Bnd.Core.Services.Navigation
{
    using Bnd.DTO.Models;
    public interface INavigationService
    {
        NavigationDto? GetNavigation();
        FooterDto? GetFooter();
    }
}
