using ApiConfigs.EnumSynonyms.Managers;
using ApiConfigs.EnumSynonyms.Models;
using Microsoft.AspNetCore.Builder;
using SB.Common.Helpers;
using SB.Common.Logics.MemberDocumentations;
using SB.Common.Logics.SynonymProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ApiConfigs.EnumSynonyms.Extensions
{
    public static class EnumSynonymExtension
    {
        public static void InitializeSynonymStorage(this IApplicationBuilder app)
        {
            var enumMigrateManager = new EnumSynonymMigrateManager();
            enumMigrateManager.Migrate<EnumSynonymMigrator, NisEnumSynonymInfo>();

            var nisManager = new NisEnumSynonymMigrateManager();
            nisManager.InitializeProperties<NisEnumSynonymInfo>();

            var synonymInfos = nisManager.GetEnumSynonymInfos<NisEnumSynonymInfo>();
            foreach (var synonymInfo in synonymInfos)
            {
                DefaultSynonymStorage.Synonyms.Add(new SynonymInfo
                {
                    Key = synonymInfo.Key,
                    En = synonymInfo.En,
                    Uz = synonymInfo.Uz,
                    Ru = synonymInfo.Ru,
                    Kr = synonymInfo.Kr
                });
            }
        }

    }
}
