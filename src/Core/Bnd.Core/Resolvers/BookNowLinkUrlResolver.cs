using AutoMapper;
using Bnd.DTO.Models;
using Umbraco.Cms.Core.Models;
using Umbraco.Cms.Web.Common.PublishedModels;

namespace Bnd.Core.Resolvers
{
    public class BookNowLinkUrlResolver : IValueResolver<FindAdealer, CtaDto, string>
    {
        public string Resolve(FindAdealer source, CtaDto destination, string destMember, ResolutionContext context)
        {
            var result = source.BookNowLink.Url;

            if (source is null)
                return result;

            //Media items should not be relative
            if (source.BookNowLink.Url.Contains("media"))
            {
                return result;
            }
            //make sure external link starts with https, otherwise browsers can resolve as relative links
            if (source.BookNowLink.Url.StartsWith("www"))
            {
                return $"https://{result}";
            }

            if (source.BookNowLink.Url.Contains("-admin") || source.BookNowLink.Url.Contains(".api.") || source.BookNowLink.Url.Contains("admin.bnd") || source.BookNowLink.Url.Contains("localhost"))
            {
                var url = new Uri(source.BookNowLink.Url);

                if (url is null)
                    return result;

                result = url.PathAndQuery.StartsWith("/") ? url.PathAndQuery : $"/{url.PathAndQuery}";
            }


            return result;
        }
    }
}
