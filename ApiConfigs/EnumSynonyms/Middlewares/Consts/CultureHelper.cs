using System.Globalization;

namespace CoreDomain.Helpers
{
    public class CultureHelper
    {
        public const string UzLanguageName = "uz-Latn-UZ";

        public const string RuLanguageName = "ru-RU";

        public const string EnLanguageName = "en";

        public const string KrLanguageName = "uz-Cyrl-UZ";


        public static CultureInfo UzLanguage = CultureInfo.GetCultureInfo(UzLanguageName);

        public static CultureInfo RuLanguage = CultureInfo.GetCultureInfo(RuLanguageName);

        public static CultureInfo EnLanguage = CultureInfo.GetCultureInfo(EnLanguageName);

        public static CultureInfo KrLanguage = CultureInfo.GetCultureInfo(KrLanguageName);
    }
}
