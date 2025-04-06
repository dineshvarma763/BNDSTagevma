namespace Bnd.Core.Indexing.Redirects
{
    using Bnd.Core.Models;
    using Bnd.DTO.Models;
    using Examine;
    using Umbraco.Cms.Infrastructure.Examine;

    public class RedirectsIndexValueBuilder : IValueSetBuilder<RedirectsDto>
    {
        public IEnumerable<ValueSet> GetValueSets(params RedirectsDto[] redirects)
        {
            foreach (var redirect in redirects)
            {
                if (redirect is null)
                    continue;

                var indexValues = new Dictionary<string, object>
                {
                    [Constants.IdField] = redirect.Id,
                    [Constants.HasRedirectField] = redirect.HasRedirect.ToString(),
                    [Constants.OldUrlField] = redirect.OldUrl,
                    [Constants.NewUrlField] = redirect.NewUrl,
                    [Constants.TypeField] = redirect.Type,
                };

                yield return new ValueSet(redirect.Id.ToString(), Constants.RedirectsIndexType, indexValues);
            }
        }
    }
}
