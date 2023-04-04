using Domain.Models.SixthSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SixthSectionResults
{
    public class OrgDataAvailabilityReportQueryResult
    {
        public List<OrganizationDataAvailability> Data { get; set; }

        public int OrganizationId { get; set; }
        public int AllCheckedSection { get; set; }
        public int FullyProvidedSections { get; set; }
        public int PartiallyProvidedSections { get; set; }
        public int NotProvidedSections { get; set; }
        public int RelevantDataSections { get; set; }
    }
}
