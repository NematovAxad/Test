using Domain.Models;
using Domain.Models.SecondSection;
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
    }
}
