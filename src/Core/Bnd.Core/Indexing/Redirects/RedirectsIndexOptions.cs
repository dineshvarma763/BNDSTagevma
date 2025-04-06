namespace Bnd.Core.Indexing.Redirects
{
    using Examine;
    using Examine.Lucene;
    using Lucene.Net.Analysis.Standard;
    using Lucene.Net.Index;
    using Lucene.Net.Util;
    using Microsoft.Extensions.Options;
    using Umbraco.Cms.Core.Configuration.Models;
    using Umbraco.Cms.Core.Scoping;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Infrastructure.Examine;
    using Constants = Bnd.Core.Models.Constants;

    public class RedirectsIndexOptions : IConfigureNamedOptions<LuceneDirectoryIndexOptions>
    {
        private readonly IOptions<IndexCreatorSettings> _settings;
        private readonly IPublicAccessService _publicAccessService;
        private readonly IScopeProvider _scopeProvider;

        public RedirectsIndexOptions(
            IOptions<IndexCreatorSettings> settings,
            IPublicAccessService publicAccessService,
            IScopeProvider scopeProvider)
        {
            _settings = settings;
            _publicAccessService = publicAccessService;
            _scopeProvider = scopeProvider;
        }

        public void Configure(string name, LuceneDirectoryIndexOptions options)
        {
            if (name.Equals(Constants.RedirectsIndexName))
            {

                options.Analyzer = new StandardAnalyzer(LuceneVersion.LUCENE_48);

                options.FieldDefinitions = new(
                    new(Constants.HasRedirectField, FieldDefinitionTypes.FullText),
                    new(Constants.OldUrlField, FieldDefinitionTypes.FullText),
                    new(Constants.NewUrlField, FieldDefinitionTypes.FullText),
                    new(Constants.TypeField, FieldDefinitionTypes.FullText)
                    );

                options.UnlockIndex = true;
                options.Validator = new ContentValueSetValidator(true, false, _publicAccessService, _scopeProvider, includeItemTypes: new[] { "redirect" });

                if (_settings.Value.LuceneDirectoryFactory == LuceneDirectoryFactory.SyncedTempFileSystemDirectoryFactory)
                {
                    // if this directory factory is enabled then a snapshot deletion policy is required
                    options.IndexDeletionPolicy = new SnapshotDeletionPolicy(new KeepOnlyLastCommitDeletionPolicy());
                }
            }
        }

        public void Configure(LuceneDirectoryIndexOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
