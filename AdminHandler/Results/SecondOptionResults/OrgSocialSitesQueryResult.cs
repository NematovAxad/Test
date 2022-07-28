using Domain.Models.SecondSection;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Results.SecondOptionResults
{
    public class OrgSocialSitesQueryResult
    {
        public int Count { get; set; }
        public List<OrganizationSocials> Socials { get; set; }
        public OrganizationSocialParameters SocialParameters { get; set; }
    }
}
