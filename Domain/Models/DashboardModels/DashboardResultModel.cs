using System.Collections.Generic;
using System.Linq;
using Domain.Enums;
using Domain.Models.Models;
using Domain.Models.SixthSection;

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
        public int ElectronicServices { get; set; }
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
        
        public List<ReportBySpheresModelDashboard> OrganizationsReport { get; set; }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ReportBySpheresModelDashboard
    {
        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public string OrgNameRu { get; set; }
        public int UserServiceId { get; set; }
        public OrgCategory Category { get; set; }
        public OrgHeadModel OrgHeadModel { get; set; }
        public ItDepartmentModel ItDepartmentModel { get; set; }
        public string OrgWebsite { get; set; }
        public List<SphereRateElement> Spheres { get; set; }
        public DigitalProjectsModel DigitalProjectsModel { get; set; }
        public int ReestrProjectCount { get; set; }
        public double RateSum { get; set; }
        public double RatePercent { get; set; }
    }

    public class OrgHeadModel
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string MiddleName { get; set; }
        
        public string Position { get; set; }
        
        public string Phone { get; set; }
        
        public string Email { get; set; }
        
        public string PhotoLink { get; set; }
    }

    public class ItDepartmentModel
    {
        public string DepartmentName { get; set; }
        public string FullNameDirector { get; set; }
        public string DirectorPosition { get; set; }
        public string WorkPhone { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string PhotoLink { get; set; }
    }

    public class DigitalProjectsModel
    {
        public int AllProjects { get; set; }
        public List<OrganizationDigitalEconomyProjectsDetail> AllProjectsList { get; set; }
        public int CompletedProjects { get; set; }
        public List<OrganizationDigitalEconomyProjectsDetail> CompletedProjectsList { get; set; }
        public int OngoinProjects { get; set; }
        public List<OrganizationDigitalEconomyProjectsDetail> OngoinProjectsList { get; set; }
        public int NotCompletedProjects { get; set; }
        public List<OrganizationDigitalEconomyProjectsDetail> NotCompletedProjectsList { get; set; }
    }
}