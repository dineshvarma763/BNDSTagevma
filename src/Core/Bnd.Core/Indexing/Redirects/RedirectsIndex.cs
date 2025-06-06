﻿namespace Bnd.Core.Indexing.Redirects
{
    using Examine.Lucene;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.Options;
    using Umbraco.Cms.Core.Hosting;
    using Umbraco.Cms.Core.Services;
    using Umbraco.Cms.Infrastructure.Examine;

    public class RedirectsIndex : UmbracoExamineIndex
    {
        public RedirectsIndex(ILoggerFactory loggerFactory, string name, IOptionsMonitor<LuceneDirectoryIndexOptions> indexOptions, IHostingEnvironment hostingEnvironment, IRuntimeState runtimeState) 
            : base(loggerFactory, name, indexOptions, hostingEnvironment, runtimeState)
        {

        }
    }
}
