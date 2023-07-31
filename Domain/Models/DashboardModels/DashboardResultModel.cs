using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models.Models;

namespace Domain.Models.DashboardModels
{
    public class DashboardResultModel
    {
        public OrgReportModel GovernmentOrganizations { get; set; }
        public OrgReportModel FarmOrganizations { get; set; }
        public OrgReportModel AdministrationOrganizations { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class OrgReportModel
    {
        public string Category { get; set; }
        public int Count { get; set; }
        
        public List<ReportBySpheresModel> OrganizationsReport { get; set; }
    }
}