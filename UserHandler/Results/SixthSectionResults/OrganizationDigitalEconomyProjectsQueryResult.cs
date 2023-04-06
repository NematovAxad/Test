using Domain.Models.SixthSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SixthSectionResults
{
    public class OrganizationDigitalEconomyProjectsQueryResult
    {
        public int ProjectsCount { get; set; }

        public List<OrganizationDigitalEconomyProjects> Projects { get; set; }

    }
}
