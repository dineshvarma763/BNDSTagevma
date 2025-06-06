//------------------------------------------------------------------------------
// <auto-generated>
//   This code was generated by a tool.
//
//    Umbraco.ModelsBuilder.Embedded v10.8.6+c17d4e1
//
//   Changes to this file will be lost if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Linq.Expressions;
using Umbraco.Cms.Core.Models.PublishedContent;
using Umbraco.Cms.Core.PublishedCache;
using Umbraco.Cms.Infrastructure.ModelsBuilder;
using Umbraco.Cms.Core;
using Umbraco.Extensions;

namespace Umbraco.Cms.Web.Common.PublishedModels
{
	// Mixin Content Type with alias "pageBase"
	/// <summary>_page base</summary>
	public partial interface IPageBase : IPublishedElement
	{
		/// <summary>Content type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ContentType { get; }

		/// <summary>Desktop product image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.MediaWithCrops DesktopProductImage { get; }

		/// <summary>Include in megamenu</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		bool IncludeInMegamenu { get; }

		/// <summary>Is product page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		bool IsProductPage { get; }

		/// <summary>Is video page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		bool IsVideoPage { get; }

		/// <summary>Megamenu image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.MediaWithCrops MegamenuImage { get; }

		/// <summary>Megamenu view all link</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.Link MegamenuViewAllLink { get; }

		/// <summary>Mobile product image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::Umbraco.Cms.Core.Models.MediaWithCrops MobileProductImage { get; }

		/// <summary>Product category</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ProductCategory { get; }

		/// <summary>Product filters</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ProductFilters { get; }

		/// <summary>Product priority</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		string ProductPriority { get; }

		/// <summary>Search content type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> SearchContentType { get; }

		/// <summary>Search topics</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> SearchTopics { get; }

		/// <summary>Topics</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> Topic { get; }

		/// <summary>Use page heading</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		bool UsePageHeading { get; }
	}

	/// <summary>_page base</summary>
	[PublishedModel("pageBase")]
	public partial class PageBase : PublishedElementModel, IPageBase
	{
		// helpers
#pragma warning disable 0109 // new is redundant
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public new const string ModelTypeAlias = "pageBase";
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public new const PublishedItemType ModelItemType = PublishedItemType.Content;
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public new static IPublishedContentType GetModelContentType(IPublishedSnapshotAccessor publishedSnapshotAccessor)
			=> PublishedModelUtility.GetModelContentType(publishedSnapshotAccessor, ModelItemType, ModelTypeAlias);
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static IPublishedPropertyType GetModelPropertyType<TValue>(IPublishedSnapshotAccessor publishedSnapshotAccessor, Expression<Func<PageBase, TValue>> selector)
			=> PublishedModelUtility.GetModelPropertyType(GetModelContentType(publishedSnapshotAccessor), selector);
#pragma warning restore 0109

		private IPublishedValueFallback _publishedValueFallback;

		// ctor
		public PageBase(IPublishedElement content, IPublishedValueFallback publishedValueFallback)
			: base(content, publishedValueFallback)
		{
			_publishedValueFallback = publishedValueFallback;
		}

		// properties

		///<summary>
		/// Content type: The content type used by the resource hub component to filter search result
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("contentType")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ContentType => GetContentType(this, _publishedValueFallback);

		/// <summary>Static getter for Content type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetContentType(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "contentType");

		///<summary>
		/// Desktop product image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("desktopProductImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops DesktopProductImage => GetDesktopProductImage(this, _publishedValueFallback);

		/// <summary>Static getter for Desktop product image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.MediaWithCrops GetDesktopProductImage(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(publishedValueFallback, "desktopProductImage");

		///<summary>
		/// Include in megamenu: Will be included in the header megamenu if toggled.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[ImplementPropertyType("includeInMegamenu")]
		public virtual bool IncludeInMegamenu => GetIncludeInMegamenu(this, _publishedValueFallback);

		/// <summary>Static getter for Include in megamenu</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public static bool GetIncludeInMegamenu(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "includeInMegamenu");

		///<summary>
		/// Is product page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[ImplementPropertyType("isProductPage")]
		public virtual bool IsProductPage => GetIsProductPage(this, _publishedValueFallback);

		/// <summary>Static getter for Is product page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public static bool GetIsProductPage(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "isProductPage");

		///<summary>
		/// Is video page
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[ImplementPropertyType("isVideoPage")]
		public virtual bool IsVideoPage => GetIsVideoPage(this, _publishedValueFallback);

		/// <summary>Static getter for Is video page</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public static bool GetIsVideoPage(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "isVideoPage");

		///<summary>
		/// Megamenu image: Image used for the third level pages in the megamenu.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("megamenuImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops MegamenuImage => GetMegamenuImage(this, _publishedValueFallback);

		/// <summary>Static getter for Megamenu image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.MediaWithCrops GetMegamenuImage(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(publishedValueFallback, "megamenuImage");

		///<summary>
		/// Megamenu view all link: View all link (only applies to the second level page).
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("megamenuViewAllLink")]
		public virtual global::Umbraco.Cms.Core.Models.Link MegamenuViewAllLink => GetMegamenuViewAllLink(this, _publishedValueFallback);

		/// <summary>Static getter for Megamenu view all link</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.Link GetMegamenuViewAllLink(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.Link>(publishedValueFallback, "megamenuViewAllLink");

		///<summary>
		/// Mobile product image
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("mobileProductImage")]
		public virtual global::Umbraco.Cms.Core.Models.MediaWithCrops MobileProductImage => GetMobileProductImage(this, _publishedValueFallback);

		/// <summary>Static getter for Mobile product image</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::Umbraco.Cms.Core.Models.MediaWithCrops GetMobileProductImage(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::Umbraco.Cms.Core.Models.MediaWithCrops>(publishedValueFallback, "mobileProductImage");

		///<summary>
		/// Product category: The categories selected from the product categories data source. Add as many as required.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("productCategory")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ProductCategory => GetProductCategory(this, _publishedValueFallback);

		/// <summary>Static getter for Product category</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetProductCategory(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "productCategory");

		///<summary>
		/// Product filters: The filters selected from the product filters data source. Add as many as required. This value is used by the Product Filters component
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("productFilters")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> ProductFilters => GetProductFilters(this, _publishedValueFallback);

		/// <summary>Static getter for Product filters</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetProductFilters(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "productFilters");

		///<summary>
		/// Product priority: Enter a value between 0 and 1 (e.g. 0.5)
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("productPriority")]
		public virtual string ProductPriority => GetProductPriority(this, _publishedValueFallback);

		/// <summary>Static getter for Product priority</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static string GetProductPriority(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<string>(publishedValueFallback, "productPriority");

		///<summary>
		/// Search content type: The content types used by the search component to filter search result
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("searchContentType")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> SearchContentType => GetSearchContentType(this, _publishedValueFallback);

		/// <summary>Static getter for Search content type</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetSearchContentType(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "searchContentType");

		///<summary>
		/// Search topics: The topics used by the search component to filter search result
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("searchTopics")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> SearchTopics => GetSearchTopics(this, _publishedValueFallback);

		/// <summary>Static getter for Search topics</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetSearchTopics(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "searchTopics");

		///<summary>
		/// Topics: The topics used by the resource hub component to filter search result
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[global::System.Diagnostics.CodeAnalysis.MaybeNull]
		[ImplementPropertyType("topic")]
		public virtual global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> Topic => GetTopic(this, _publishedValueFallback);

		/// <summary>Static getter for Topics</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[return: global::System.Diagnostics.CodeAnalysis.MaybeNull]
		public static global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent> GetTopic(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<global::System.Collections.Generic.IEnumerable<global::Umbraco.Cms.Core.Models.PublishedContent.IPublishedContent>>(publishedValueFallback, "topic");

		///<summary>
		/// Use page heading: Use the title as a H1 heading on the page.
		///</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		[ImplementPropertyType("usePageHeading")]
		public virtual bool UsePageHeading => GetUsePageHeading(this, _publishedValueFallback);

		/// <summary>Static getter for Use page heading</summary>
		[global::System.CodeDom.Compiler.GeneratedCodeAttribute("Umbraco.ModelsBuilder.Embedded", "10.8.6+c17d4e1")]
		public static bool GetUsePageHeading(IPageBase that, IPublishedValueFallback publishedValueFallback) => that.Value<bool>(publishedValueFallback, "usePageHeading");
	}
}
