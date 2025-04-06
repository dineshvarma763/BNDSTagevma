namespace Bnd.Core.Services.Navigation
{
    using Bnd.DTO.Models;
    using Bnd.Core.Factory;

    public class NavigationService : INavigationService
    {
        private readonly INavigationFactory _navigationFactory;
        public NavigationService(INavigationFactory navigationFactory)
        {
            _navigationFactory = navigationFactory;
        }

        /// <summary>
        /// Gets all navigation itemsB
        /// </summary>
        /// <returns></returns>
        public NavigationDto? GetNavigation()
        {
            return _navigationFactory.GetNavigation();
        }

        /// <summary>
        /// Gets the footer and all of its associated content from Umbraco
        /// </summary>
        /// <returns></returns>
        public FooterDto? GetFooter()
        {
            return _navigationFactory.GetFooter();
        }
    }
}
