using SB.Common.Logics.SynonymProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [EnumSynonym]
    public enum UIErrors
    {
        /// <uz></uz>
        /// <ru></ru>
        /// <en></en>
        /// <kr></kr>

        /// <summary>
        /// Невозможно редактировать доску. Доска в процессе
        /// </summary>
        /// <uz>Muddat tugagan</uz>
        /// <ru>Крайний срок истек</ru>
        /// <en>Deadline is ower</en>
        /// <kr>Муддат тугаган</kr>
        DeadlineNotFound = -1,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Tashkilot topilmadi</uz>
        /// <ru>Организация не найдена</ru>
        /// <en>Organization Not found</en>
        /// <kr>Ташкилот топилмади</kr>
        OrganizationNotFound = -2,
    }
}
