using Domain.Models;
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
    }
}
