using ApiConfigs.EnumSynonyms.Models;
using SB.Common.Logics.SynonymProviders;
using System.Collections.Generic;

namespace ApiConfigs.EnumSynonyms
{
    public class EnumSynonymMigrator : IEnumSynonymMigrator<NisEnumSynonymInfo>
    {
        public bool IsActual()
        {
            return false;
        }

        public void Migrate(List<NisEnumSynonymInfo> synonymInfos)
        {
            //foreach (var synonymInfo in synonymInfos)
            //{
            //    DefaultSynonymStorage.Synonyms.Add(new SynonymInfo
            //    {
            //        Key = synonymInfo.Key,
            //        En = synonymInfo.En,
            //        Ru = synonymInfo.Ru,
            //        Uz = synonymInfo.Uz
            //    });
            //}
        }
    }
}
