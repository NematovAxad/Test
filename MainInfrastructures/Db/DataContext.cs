using Domain.Models;
using Domain.Models.EighthSection;
using Domain.Models.FifthSection;
using Domain.Models.FifthSection.ReestrModels;
using Domain.Models.FirstSection;
using Domain.Models.Organization;
using Domain.Models.Ranking;
using Domain.Models.Ranking.Administrations;
using Domain.Models.SecondSection;
using Domain.Models.SeventhSection;
using Domain.Models.SixthSection;
using Domain.Models.ThirdSection;
using Domain.MonitoringModels.Models;
using EntityRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MainInfrastructures.Db
{
    public class DataContext : DbContext, IDataContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);

            base.OnConfiguring(optionsBuilder);
        }
        public DbContext Context => this;
        public DbSet<Organizations> Organizations { get; set; }
        public DbSet<BasedDocuments> BasedDocuments { get; set; }
        public DbSet<SubOrganizations> SubOrganizations { get; set; }
        public DbSet<EmployeeStatistics> EmployeeStatistics { get; set; }
        public DbSet<SubOrgStatistics> SubOrgStatistics { get; set; }
        public DbSet<OrganizationDocuments> OrganizationDocuments { get; set; }
        public DbSet<ReplacerOrgHead> ReplacerOrgHeads { get; set; }
        public DbSet<Regions> Regions { get; set; }
        public DbSet<OrganizationApps> OrganziationApps { get; set; }
        public DbSet<ContentManager> ContentManager { get; set; }
        public DbSet<Deadline> Deadline { get; set; }
        
        public DbSet<OrganizationSocials> OrganizationSocials { get; set; }
        public DbSet<HelplineInfo> HelplineInfo { get; set; }
        public DbSet<OrgDataFiller> OrgDataFiller { get; set; }
        public DbSet<OrgHelpline> OrgHelpline { get; set; }
        public DbSet<OrganizationPublicServices> OrganizationPublicServices { get; set; }
        public DbSet<OrgInformationSystems> OrgInformationSystems { get; set; }
        public DbSet<DelaysOnProjects> DelaysOnProjects { get; set; }
        public DbSet<OrganizationIctSpecialForces> OrganizationIctSpecialForces { get; set; }
        public DbSet<OrganizationEvents> OrganizationEvents { get; set; }
        public DbSet<OrgFutureYearsStrategies> OrgFutureYearsStrategies { get; set; }
        public DbSet<OrganizationComputers> OrganizationComputers { get; set; }
        public DbSet<OrganizationServers> OrganizationServers { get; set; }
        public DbSet<IsFilledTable> IsFilledTable { get; set; }
        public DbSet<GSphere> GSphere { get; set; }
        public DbSet<GField> GField { get; set; }
        public DbSet<GSubField> GSubField { get; set; }
        public DbSet<XSphere> XSphere { get; set; }
        public DbSet<XField> XField { get; set; }
        public DbSet<XSubField> XSubField { get; set; }
        public DbSet<XRankTable> XRankTable { get; set; }
        public DbSet<ASphere> ASphere { get; set; }
        public DbSet<AField> AField { get; set; }
        public DbSet<ASubField> ASubField { get; set; }
        public DbSet<ARankTable> ARankTable { get; set; }
        public DbSet<GRankTable> GRankTable { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<Cooworkers> Cooworkers { get; set; }
        public DbSet<FileStage> FileStage { get; set; }
        public DbSet<Financier> Financier { get; set; }
        public DbSet<NormativeLegalDocument> NormativeLegalDocument { get; set; }
        public DbSet<Performencer> Performencer { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<Stage> Stage { get; set; }
        public DbSet<ProjectFinanciers> ProjectFinanciers { get; set; }
        public DbSet<ProjectComment> ProjectComment { get; set; }
        public DbSet<FileProject> FileProject { get; set; }
        public DbSet<OrgProcesses> OrgProcesses { get; set; }
        public DbSet<OrgFinance> OrgFinance { get; set; }
        public DbSet<WebSiteAvailability> WebSiteAvailabilitie { get; set; }
        public DbSet<WebSiteRequirements> WebSiteRequirements { get; set; }
        public DbSet<SiteFailsTable> SiteFailsTable { get; set; }
        public DbSet<SiteRequirementsSample> SiteRequirementsSamples { get; set; }
        public DbSet<ReestrProjectPosition> ReestrProjectPosition { get; set; }
        public DbSet<ReestrProjectExpertDecision> ReestrProjectExpertDecision { get; set; }
        public DbSet<ReestrProjectCyberSecurityExpertDecision> ReestrProjectCyberSecurityExpertDecision  { get; set; }
        public DbSet<ReestrProjectIdentities> ReestrProjectIdentities { get; set; }
        public DbSet<ProjectIdentities> ProjectIdentities { get; set; }
        public DbSet<ReestrProjectConnection> ReestrProjectConnection { get; set; }
        public DbSet<ProjectConnections> ProjectConnections { get; set; }
        public DbSet<ReestrProjectClassifications> ReestrProjectClassifications { get; set; }
        public DbSet<ProjectClassifications> ProjectClassifications { get; set; }
        public DbSet<SiteFailComments> SiteFailComments { get; set; }
        public DbSet<ReestrProjectAuthorizations> ReestrProjectAuthorizations { get; set; } 
        public DbSet<ProjectAuthorizations> ProjectAuthorizations { get; set; }
        public DbSet<ReestrProjectAutomatedServices> ReestrProjectAutomatedServices { get; set; }
        public DbSet<AutomatedFunctions> AutomatedFunctions { get; set; }
        public DbSet<AutomatedServices> AutomatedServices { get; set; }
        public DbSet<MygovReports> MygovReports { get; set; }
        public DbSet<MygovReportsDetail> MygovReportsDetail { get; set; }
        public DbSet<ReestrProjectEfficiency> ReestrProjectEfficiency { get; set; }
        public DbSet<ProjectEfficiency> ProjectEfficiency { get; set; }
        public DbSet<OrganizationFinance> OrganizationFinance { get; set; }
        public DbSet<OrganizationFinanceReport> OrganizationFinanceReports { get; set; }
        public DbSet<OrganizationBudget> OrganizationBudget { get; set; }
        public DbSet<OrganizationIndicators> OrganizationIndictors { get; set; }
        public DbSet<IndicatorRating> IndicatorRatings { get; set; }
    }
}
