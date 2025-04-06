namespace Bnd.Core.Composers
{
    using Bnd.Core.Indexing.Redirects;
    using Bnd.Core.Models;
    using Examine;
    using Microsoft.Extensions.DependencyInjection;
    using Umbraco.Cms.Core.Composing;
    using Umbraco.Cms.Core.DependencyInjection;
    using Umbraco.Cms.Infrastructure.Examine;
    using Umbraco.Cms.Infrastructure.PublishedCache;

    public class IndexComposer : IComposer
    {
        public void Compose(IUmbracoBuilder builder)
        {
            builder.Services.AddExamineLuceneIndex<RedirectsIndex, ConfigurationEnabledDirectoryFactory>(Constants.RedirectsIndexLabel);

            builder.Services.AddSingleton<RedirectsIndexValueBuilder>();

            builder.Services.AddSingleton<IIndexPopulator, RedirectsIndexPopulator>();
        }
    }
}
