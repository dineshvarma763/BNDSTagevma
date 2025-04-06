namespace Bnd.Core.Composers
{
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;
    using Umbraco.Cms.Infrastructure.PublishedCache;

    public class DisableNucacheDatabaseComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            var settings = new PublishedSnapshotServiceOptions()
            {
                IgnoreLocalDb = true
            };
            builder.Services.AddSingleton(settings);
        }
    }
}
