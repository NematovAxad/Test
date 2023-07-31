using System.Collections.Generic;
using Domain.Enums;
using Domain.Models.Models;

namespace Domain.Models.DashboardModels
{
    public class DashboardResultModel
    {
        public List<OrgReportModel> GovernmentOrganizations { get; set; }
        public List<OrgReportModel> FarmOrganizations { get; set; }
        public List<OrgReportModel> AdministrationOrganizations { get; set; }
    }

    public class OrgReportModel
    {
        public OrgCategory Category { get; set; }
        public int Count { get; set; }
        public List<ReportBySpheresModel> OrganizationsReport { get; set; }
    }
}