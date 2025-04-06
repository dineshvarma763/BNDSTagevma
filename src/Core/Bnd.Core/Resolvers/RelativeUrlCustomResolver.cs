using AutoMapper;
using Bnd.DTO.Models;
using Umbraco.Cms.Core.Models;

namespace Bnd.Core.Resolvers
{
    public class RelativeUrlCustomResolver : IValueResolver<Link, CtaDto, string>
    {
        public string Resolve(Link source, CtaDto destination, string destMember, ResolutionContext context)
        {
            var result = source.Url;

            if (source is null)
                return result;

            //Media items should not be relative
            if (source.Url.Contains("media")){
                return result;
            }
            //make sure external link starts with https, otherwise browsers can resolve as relative links
            if (source.Url.StartsWith("www"))
            {
                return $"https://{result}";
            }

            if (source.Url.Contains("-admin") || source.Url.Contains(".api.") || source.Url.Contains("admin.bnd") || source.Url.Contains("localhost"))
            {
                var url = new Uri(source.Url);

                if (url is null)
                    return result;

                result = url.PathAndQuery.StartsWith("/") ? url.PathAndQuery : $"/{url.PathAndQuery}";
            }


            return result;
        }
    }
}
