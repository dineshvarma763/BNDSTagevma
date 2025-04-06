/*
 * Maps images in RTE to have absolute URL
 */

using AutoMapper;
using Bnd.Core.Extensions;
using HtmlAgilityPack;
using static Umbraco.Cms.Core.Constants;

namespace Bnd.Core.Resolvers
{
    public class RichTextUrlResolver<TSrc, TDst> : IValueResolver<TSrc, TDst, string?>
    {
        private readonly string _property;
        public RichTextUrlResolver(string property)
        {
            _property = property;
        }

        public string? Resolve(TSrc source, TDst destination, string? destMember, ResolutionContext context)
        {
            var result = string.Empty;

            if (source is null)
                return null;

            var property = source.GetType().GetProperty(_property);

            if (property is null)
                return null;

            string sourceProperty = property.GetValue(source, null).ToString();

            if (string.IsNullOrEmpty(sourceProperty))
                return result;

            //var resolvedHtml = RemoveAbsoluteLinkAbsolutePath(sourceProperty);

            var resolvedHtml = sourceProperty;

            return resolvedHtml;
        }

        /// <summary>
        /// Add an absolute path to all the img tags in the html of an e-mail.
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        private static string RemoveAbsoluteLinkAbsolutePath(string html)
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(html);

            if (doc.DocumentNode.SelectNodes("//a[@href]") != null)
            {
                foreach (HtmlNode img in doc.DocumentNode.SelectNodes("//a[@href]"))
                {
                    HtmlAttribute att = img.Attributes["href"];
                    if (att.Value.Contains("api") || att.Value.Contains("admin") || att.Value.Contains("-admin") || att.Value.Contains("admin.bnd") )
                    {
                        var absoluteUrl = new Uri(att.Value);
                        var relativeUrl = absoluteUrl.PathAndQuery;
                        att.Value = relativeUrl.FormatNavigationUrl();
                    }
                }
            }

            return doc.DocumentNode.InnerHtml;
        }
    }
}
