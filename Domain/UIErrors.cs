using SB.Common.Logics.SynonymProviders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    [EnumSynonym]
    public enum UIErrors
    {

        /// <summary>
        /// Невозможно редактировать доску. Доска в процессе
        /// </summary>
        /// <uz>Doskani tahrirlab bo'lmaydi</uz>
        /// <kr>Доскани таҳрирлаб бўлмайди</kr>
        /// <ru>Невозможно редактировать доску. Доска в процессе</ru>
        /// <en>Cannot edit board. Board has already started</en>
        DeadlineNotFound = -1,
        
    }
}
