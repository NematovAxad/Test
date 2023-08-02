using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models.Models;

namespace Domain.Models.DashboardModels
{
    public class DashboardResultModel
    {
        public RatedOrganizationServices RatedServicesReport { get; set; }
        
        public DigitalEconomyProjectsReport DigitalEconomyProjectsReport { get; set; }
        
        public RatedReestrProjects ReestrProjectsReport { get; set; }
        public OrgReportModel GovernmentOrganizations { get; set; }
        public OrgReportModel FarmOrganizations { get; set; }
        public OrgReportModel AdministrationOrganizations { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public class RatedOrganizationServices
    {
        public int Count { get; set; }
        public int MyGovServices { get; set; }
        public int OtherAppServices { get; set; }
    }

    public class DigitalEconomyProjectsReport
    {
        public int Count { get; set; }
        public int Completed { get; set; }
        public int Ongoing { get; set; }
        public int NotFinished { get; set; }
    }
    
    public class RatedReestrProjects
    {
        public int Count { get; set; }
        public int ConfirmedProjectPassports { get; set; }
        public int ExpertDecision { get; set; }
        public int WorkingStage { get; set; }
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