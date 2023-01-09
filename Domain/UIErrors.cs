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
        /// 
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

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Bunday nomerli fayl bor</uz>
        /// <ru>Документ с этим номером существует</ru>
        /// <en>With this number document exist</en>
        /// <kr>Документ с этим номером существует</kr>
        BasedDocExist = -3,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Foydalanuvchi bunday huquqga ega emas</uz>
        /// <ru>Пользователь не имеет такого права</ru>
        /// <en>User don't have this right</en>
        /// <kr>Пользователь не имеет такого права</kr>
        UserPermissionsNotAllowed = -4,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>O'zgartirish uchun ma'lumotlar topilmadi</uz>
        /// <ru>Данные для изменения не найдены</ru>
        /// <en>Data to change not found</en>
        /// <kr>Данные для изменения не найдены</kr>
        BasedDocNotFound = -5,
    }
}
