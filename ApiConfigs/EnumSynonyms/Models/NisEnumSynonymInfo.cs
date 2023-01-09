using SB.Common.Logics.SynonymProviders;

namespace ApiConfigs.EnumSynonyms.Models
{
    public class NisEnumSynonymInfo :  EnumSynonymInfo
    {
        [EnumSynonymProperty("uz")]
        public string Uz { get; set; }

        [EnumSynonymProperty("ru")]
        public string Ru { get; set; }

        [EnumSynonymProperty("en")]
        public string En { get; set; }

        [EnumSynonymProperty("kr")]
        public string Kr { get; set; }

        [EnumSynonymProperty("version")]
        public string Version { get; set; }

        public override string ToString()
        {
            return $"CurrentLanguage: {Key}, Version: {Version}, Uz: {Uz}, Ru: {Ru}, En: {En}";
        }
    }
}
