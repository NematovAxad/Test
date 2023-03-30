using Domain.Models.SixthSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserHandler.Results.SixthSectionResults
{
    public class OrganizationDigitalEconomyProjectsQueryResult
    {
        public List<OrganizationDigitalEconomyProjects> Projects { get; set; }

        public int ProjectsCount { get; set; }
        public int CompletedProjects { get; set; }
        public int OngoingProjects { get; set; }
        public int NotFinishedProjects { get; set; }
        public double Rate
        {
            get { return CompletedProjects / ProjectsCount; }
        }
    }
}
