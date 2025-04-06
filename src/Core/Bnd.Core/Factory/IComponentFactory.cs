namespace Bnd.Core.Factory
{
    using Umbraco.Cms.Core.Models.PublishedContent;

    public interface IComponentFactory
    {
        /// <summary>
        /// Gets all the available components.
        /// </summary>
        /// <returns>A list of component aliases.</returns>
        IEnumerable<string> GetAvailableComponents();

        /// <summary>
        /// Based on alias passed in, gets the matching component representation.
        /// </summary>
        /// <param name="componentName"></param>
        /// <returns>The component's dto.</returns>
        object? GetComponent(string componentName, IPublishedContent component);
    }

}
