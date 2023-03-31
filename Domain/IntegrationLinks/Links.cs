using System;
using System.Collections.Generic;
using System.Security.AccessControl;
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
        public static string MyGovServices = "https://my.gov.uz/oz/api/nis-uz/download-service-deadline-file?";

        public static List<string> Sections = new List<string> { "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", 
                                                                    "2.1", "2.2" , "2.3", "2.4", "2.5", "2.6" , "2.7", "2.8",
                                                                    "3.1", "3.2" , "3.3", "3.4",
                                                                    "5.1", 
                                                                    "6.1", "6.2" , "6.3", "6.4",
                                                                    "7.1", "7.2" ,
                                                                    "8.1", "8.2" };
        
    }
}
