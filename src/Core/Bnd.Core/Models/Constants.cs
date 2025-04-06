namespace Bnd.Core.Models
{
    public class Constants
    {
        public const string DesktopBackgroundImage = "DesktopBackgroundImage";
        public const string MobileBackgroundImage = "MobileBackgroundImage";
        public const string BackgroundVideo = "BackgroundVideo";
        public const string MobileBackgroundVideo = "MobileBackgroundVideo";

        public const string DesktopImage = "DesktopImage";
        public const string MobileImage = "MobileImage";

        public const string FirstDescription = "FirstDescription";
        public const string FirstDesktopImage = "FirstDesktopImage";
        public const string FirstMobileImage = "FirstMobileImage";

        public const string SecondDescription = "SecondDescription";
        public const string SecondDesktopImage = "SecondDesktopImage";
        public const string SecondMobileImage = "SecondMobileImage";

        public const string ThirdDescription = "ThirdDescription";
        public const string ThirdDesktopImage = "ThirdDesktopImage";
        public const string ThirdMobileImage = "ThirdMobileImage";

        public const string ListingDesktopImage = "ListingDesktopImage";
        public const string ListingMobileImage = "ListingMobileImage";

        public const string DesktopProductImage = "DesktopProductImage";
        public const string MobileProductImage = "MobileProductImage";
        public const string MegamenuImage = "MegamenuImage";
        public const string SiteLogo = "SiteLogo";
        public const string OpenGraphImage = "OpenGraphImage";

        public const string LinkImage = "LinkImage";
        public const string Image = "Image";

        public const string DealersFolderXPath = "//dealersFolder";
        public const string MobileShowroomsXPath = "//mobileShowroomsFolder";
        public const string DealerLogo = "Logo";
        

        // Endpoints
        public const string AllDealersEndpoint = "/api/dealers/get";
        public const string SearchDealersEndpoint = "/api/dealers/search";
        public const string SuggestDealersEndpoint = "/api/dealers/suggest";
        public const string MobileShowroomsEndpoint = "/api/dealers/mobileshowrooms";

        // Rich Text Editor Fields
        public const string Content = "Content";
        public const string HtmlContent = "HtmlContent";
        public const string Description = "Description";
        public const string CardDescription = "CardDescription";
        public const string TabContent = "TabContent";
        public const string OpeningHours = "OpeningHours";



        /**
         * 
         * 
         * Lucene Custom Index
         * 
         * 
         */
        public const string RedirectsIndexName = "Redirects";
        public const string RedirectsIndexLabel = "RedirectsIndex";
        public const string RedirectsIndexType = "redirect";
        // property fields
        public const string IdField = "id";
        public const string HasRedirectField = "hasRedirect";
        public const string OldUrlField = "oldUrl";
        public const string NewUrlField = "newUrl";
        public const string TypeField = "type";

        public const string CtaForm = "CtaForm";

        //Dealer Default Settings
        public const string DefaultSearchSetting = "//website";
        public const string DealerNearMeFindDealerXPath = "//page/components/findADealer";
        public const string DealerNearMeFindDealerRoute = "///contact-us/dealers-near-me/dealer-overview-page/find-a-dealer";
    }
}
