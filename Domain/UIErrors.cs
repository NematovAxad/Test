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


        /// <summary>
        /// 
        /// </summary>
        /// <uz>Malumotlarni kiritishga qo'yilgan muddat tugadi</uz>
        /// <ru>Срок заполнения данных истек</ru>
        /// <en>Deadline to fill data is expired</en>
        /// <kr>Срок заполнения данных истек</kr>
        DeadlineExpired = -6,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Bunday ma'lumot mavjud</uz>
        /// <ru>Данные с этими параметрами существуют</ru>
        /// <en>Data with this parameters is exist</en>
        /// <kr>Данные с этими параметрами существуют</kr>
        DataWithThisParametersIsExist = -7,


        /// <summary>
        /// 
        /// </summary>
        /// <uz>O'zgartirish uchun ma'lumot topilmadi</uz>
        /// <ru>Данные для изменения не найдены</ru>
        /// <en>Data to change not found</en>
        /// <kr>Данные для изменения не найдены</kr>
        DataToChangeNotFound = -8,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Yetarli ma;lumot berilmadi</uz>
        /// <ru>Не предоставлено достаточно данных</ru>
        /// <en>Enough data not provided</en>
        /// <kr>Не предоставлено достаточно данных</kr>
        EnoughDataNotProvided = -9,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Bu vaqt oralig'ida ma'lumotlar yo'q</uz>
        /// <ru>Данные за этот период не найдены</ru>
        /// <en>Data for this perio not found</en>
        /// <kr>Данные за этот период не найдены</kr>
        DataForThisPeriodNotFound = -10,


        /// <summary>
        /// 
        /// </summary>
        /// <uz>Bunday Section yo'q</uz>
        /// <ru>Этот раздел не существует</ru>
        /// <en>This section don't exist</en>
        /// <kr>Этот раздел не существует</kr>
        IncorrectSection = -11,


        /// <summary>
        /// 
        /// </summary>
        /// <uz>Yo'nalis id si kiritilmagan</uz>
        /// <ru>Идентификатор поля не указан</ru>
        /// <en>Field Id not provided</en>
        /// <kr>Идентификатор поля не указан</kr>
        FieldIdNotProvided = -12,



        /// <summary>
        /// 
        /// </summary>
        /// <uz>Sub Yo'nalis id si kiritilmagan</uz>
        /// <ru>Идентификатор подполя не указан</ru>
        /// <en>SubField Id not provided</en>
        /// <kr>Идентификатор подполя не указан</kr>
        SubFieldIdNotProvided = -13,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Xizmat faqat davlat tashkilotlariga</uz>
        /// <ru>Услуга только для государственных организаций</ru>
        /// <en>Service for only government organizations</en>
        /// <kr>Услуга только для государственных организаций</kr>
        ApiNotForThisTypeOfOrganization = -14,

        /// <summary>
        /// 
        /// </summary>
        /// <uz>Ma'lumotlar qo'shish cheklangan</uz>
        /// <ru>Ограничение на добавление данных</ru>
        /// <en>Limit to add data </en>
        /// <kr>Ограничение на добавление данных</kr>
        LimitToAddFull = -15,
    }
}
