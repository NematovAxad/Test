using Domain.Models.SixthSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SixthSectionResults
{
    public class OrgDataAvailabilityReportQueryResult
    {
        public List<OrganizationDataAvailability> Data { get; set; }

        public double DataAvailabilityRate { get; set; }

        public double DataRelevanceRate { get; set; }

        public double RateSum { get; set; }

        public DateTime LastUpdate { get; set; }

    }
}
