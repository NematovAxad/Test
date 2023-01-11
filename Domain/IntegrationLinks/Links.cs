using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.IntegrationLinks
{
    public static class Links
    {
        public static string OpenDataurl => "https://data.egov.uz/apiPartner/Partner/NisUzApi";
        public static string ReesterFirstLink => "https://reestr.uz/api/apiProject/Integration/nisgetorgprojects";
        public static string ReesterSecondLink => "https://reestr.uz/api/apiProject/Integration/getproject";
        public static string CyberSecurityUrl = "https://sm.csec.uz/api/v1/nis/rating";
        public static string AuthOrgGetUrl = "https://auth.egov.uz/api/Organization/GetOrg";
    }
}
