using Domain.Models.SixthSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SixthSectionResults
{
    public class OrgIndicatorQueryResult
    {
        public int Count { get; set; }
        public List<OrganizationIndicators> Data { get; set; }
    }
}
