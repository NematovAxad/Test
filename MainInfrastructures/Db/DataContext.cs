﻿using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.Organization;
using Domain.Models.Ranking;
using Domain.Models.SecondSection;
using Domain.Models.SeventhSection;
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
        public DbSet<RankTable> RankTable { get; set; }
        public DbSet<Sphere> Sphere { get; set; }
        public DbSet<Field> Field { get; set; }
        public DbSet<OrganizationSocialParameters> OrganizationSocialParameters { get; set; }
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
        public DbSet<SubField> SubField { get; set; }
        public DbSet<GSphere> GSphere { get; set; }
        public DbSet<GField> GField { get; set; }
        public DbSet<GSubField> GSubField { get; set; }
        public DbSet<XSphere> XSphere { get; set; }
        public DbSet<XField> XField { get; set; }
        public DbSet<XSubField> XSubField { get; set; }
        public DbSet<XRankTable> XRankTable { get; set; }
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
    }
}
