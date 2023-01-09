namespace OpenBudgetDomain.Consts
{
    public class LanguageConsts
    {
        public const string Uz = "uz";
        public const string Ru = "ru";
        public const string En = "en";
        public const string Kr = "kr";

        private static string _key { get; set; }
        public static string CurrentLanguage
        {
            get => string.IsNullOrEmpty(_key) ? Ru : _key;
            set => _key = value;
        }
    }
}
