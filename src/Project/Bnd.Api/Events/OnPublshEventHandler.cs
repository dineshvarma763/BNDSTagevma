namespace Bnd.Api.Events
{
    using Bnd.Core.Models;
    using Microsoft.Extensions.Logging;
    using System;
    using Umbraco.Cms.Core.Events;
    using Umbraco.Cms.Core.Notifications;
    using Umbraco.Cms.Infrastructure.Examine;

    public class OnPublshEventHandler : INotificationHandler<ContentPublishedNotification>
    {
        private readonly IIndexRebuilder _indexRebuilder;
        private readonly ILogger<OnPublshEventHandler> _logger;
        public OnPublshEventHandler(IIndexRebuilder indexRebuilder, ILogger<OnPublshEventHandler> logger)
        {
            _indexRebuilder = indexRebuilder;
            _logger = logger;
        }
       
        public void Handle(ContentPublishedNotification notification)
        {
            _logger.LogInformation($"Rebuilding custom index {Constants.RedirectsIndexLabel}");

            if(_indexRebuilder.CanRebuild(Constants.RedirectsIndexLabel))
            {
                _logger.LogInformation($"Umbraco can rebuild the custom index {Constants.RedirectsIndexLabel}");
                _indexRebuilder.RebuildIndex(Constants.RedirectsIndexLabel, null, true);
                _logger.LogInformation($"Initiated rebuild of {Constants.RedirectsIndexLabel}.");
            }
        }
    }
}
