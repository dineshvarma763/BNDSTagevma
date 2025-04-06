using System;
using System.Linq;
using Umbraco.Cms.Core.Events;
using Umbraco.Cms.Core.Models.ContentEditing;
using Umbraco.Cms.Core.Notifications;
using Umbraco.Cms.Core.Services;
using Umbraco.Extensions;

namespace Bnd.Api.Events
{
    public class EditorModelEventHandler : INotificationHandler<ContentSavingNotification>
    {
        private readonly IContentService _contentService;
        public EditorModelEventHandler(IContentService contentService)
        {
            _contentService = contentService;
        }
        public void Handle(ContentSavingNotification notification)
        {

            var savedPages = notification.SavedEntities.Where(x => x.ContentType.Alias.Equals("page"));

            if (savedPages is null || !savedPages.Any())
                return;

            foreach(var savedPage in savedPages)
            {
                DateTime? listingCreatedDateField = savedPage.GetValue<DateTime?>("listingCreatedDate");
                if(listingCreatedDateField is null || listingCreatedDateField == DateTime.MinValue)
                {
                    savedPage.SetValue("listingCreatedDate", DateTime.Now.ToLocalTime());
                    _contentService.Save(savedPage);
                }
            }
        }
    }
}
