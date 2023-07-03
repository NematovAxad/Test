using Domain.Models.ThirdSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.ThirdSection
{
    public class OrganizationServicesQueryResult
    {
        public List<ServiceReport> Services { get; set; }
        public decimal FinalRank { get; set; }
    }

    public class ServiceReport
    {
        public OrganizationServices Service { get; set; }

        public int RatesCount { get; set; }

        public decimal ApplicationProblems { get; set; }

        public decimal Recommendation { get; set; }

        public decimal ServiceSatisfaction { get; set; }

        public decimal Protest { get; set; }

        public decimal ServiceRate { get; set; }
    }
}
