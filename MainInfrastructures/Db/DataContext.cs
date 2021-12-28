﻿using Domain.Models;
using Domain.Models.FifthSection;
using Domain.Models.SecondSection;
using Domain.Models.SeventhSection;
using Domain.Models.ThirdSection;
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
        public DbSet<OrganizationSocialSites> OrganizationSocialSites { get; set; }
        public DbSet<OrganizationMessengers> OrganizationMessengers { get; set; }
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
    }
}
