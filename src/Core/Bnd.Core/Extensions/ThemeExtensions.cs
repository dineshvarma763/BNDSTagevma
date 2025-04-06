namespace Bnd.Core.Extensions
{
    public static class ThemeExtensions
    {
        private static readonly string Dark = "Dark";
        private static readonly string Light = "Light";
        public static string ToFriendlyBackgroundColour(this string backgroundColour)
        {
            var result = Light;

            if(string.IsNullOrEmpty(backgroundColour))
            {
                return result;
            }

            if(backgroundColour == "f2f2f2")
            {
                result = Dark;
            }

            return result;
        }

        public static string ToFriendlyFontColour(this string fontColour)
        {
            var result = Dark;

            if(string.IsNullOrEmpty(fontColour))
            {
                return result;
            }

            if(fontColour == "ffffff")
            {
                result = Light;
            }

            return result;
        }
    }
}
