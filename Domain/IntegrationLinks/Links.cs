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
        public static string MibService = "https://nisreport.egov.uz/nisreport/mip2?";


        public static List<string> Sections = new List<string> { "1.1", "1.2", "1.3", "1.4", "1.5", "1.6", 
                                                                    "2.1", "2.2" , "2.3", "2.4", "2.5", "2.6" , "2.7", "2.8",
                                                                    "3.1", "3.2" , "3.3", "3.4",
                                                                    "5.1", 
                                                                    "6.1", "6.2" , "6.3", "6.4",
                                                                    "7.1", "7.2" ,
                                                                    "8.1", "8.2" };
        public static List<Tuple<string, double, double>> listGos = new List<Tuple<string, double, double>> { 
            new Tuple<string, double, double>("1.1", 0.1, 0.1),
            new Tuple<string, double, double>("1.2", 0.1, 0.1),
            new Tuple<string, double, double>("1.3", 0.1, 0.1),
            new Tuple<string, double, double>("1.4", 0.1, 0.1),
            new Tuple<string, double, double>("1.5", 0.03, 0.03),
            new Tuple<string, double, double>("1.6", 0.1, 0.1),
            new Tuple<string, double, double>("2.1", 0.1, 0.1),
            new Tuple<string, double, double>("2.2", 0.01, 0.01),
            new Tuple<string, double, double>("2.4", 0.1, 0.1),
            new Tuple<string, double, double>("2.5", 0.1, 0.1),
            new Tuple<string, double, double>("2.7", 0.01, 0.01),
            new Tuple<string, double, double>("2.8", 0.1, 0.1),
            new Tuple<string, double, double>("3.2", 0.05, 0.05),
            new Tuple<string, double, double>("5.1", 0.1, 0.1),
            new Tuple<string, double, double>("5.2", 0.1, 0.1),
            new Tuple<string, double, double>("5.3", 0.1, 0.1),
            new Tuple<string, double, double>("6.1", 0.1, 0.1),
            new Tuple<string, double, double>("6.2", 0.1, 0.1),
            new Tuple<string, double, double>("6.3", 0.1, 0.1),
            new Tuple<string, double, double>("7.1", 0.1, 0.1),
            new Tuple<string, double, double>("7.2", 0.1, 0.1),
            new Tuple<string, double, double>("8.1", 0.1, 0.1),
            new Tuple<string, double, double>("8.2", 0.1, 0.1)
        };
        public static List<Tuple<string, double, double>> listXoz = new List<Tuple<string, double, double>> {
            new Tuple<string, double, double>("1.1", 0.1, 0.2),
            new Tuple<string, double, double>("1.2", 0.1, 0.1),
            new Tuple<string, double, double>("1.3", 0.1, 0.1),
            new Tuple<string, double, double>("1.4", 0.1, 0.1),
            new Tuple<string, double, double>("1.5", 0.03, 0.03),
            new Tuple<string, double, double>("1.6", 0.2, 0.2),
            new Tuple<string, double, double>("2.1", 0.1, 0.1),
            new Tuple<string, double, double>("2.2", 0.01, 0.01),
            new Tuple<string, double, double>("2.4", 0.1, 0.1),
            new Tuple<string, double, double>("2.5", 0.1, 0.1),
            new Tuple<string, double, double>("2.7", 0.01, 0.01),
            new Tuple<string, double, double>("2.8", 0.1, 0.1),
            new Tuple<string, double, double>("5.1", 0.2, 0.2),
            new Tuple<string, double, double>("5.2", 0.2, 0.2),
            new Tuple<string, double, double>("5.3", 0.2, 0.2),
            new Tuple<string, double, double>("6.1", 0.1, 0.1),
            new Tuple<string, double, double>("6.2", 0.2, 0.2),
            new Tuple<string, double, double>("6.3", 0.1, 0.1),
            new Tuple<string, double, double>("7.1", 0.1, 0.1),
            new Tuple<string, double, double>("7.2", 0.1, 0.1),
            new Tuple<string, double, double>("8.1", 0.1, 0.1),
            new Tuple<string, double, double>("8.2", 0.1, 0.1)
        };
    };  
}
