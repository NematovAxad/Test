using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Models.Models
{
    public class ReportBySpheresModel
    {
        public int OrganizationId { get; set; }
        public string OrgName { get; set; }
        public OrgCategory Category { get; set; }
        public double SphereRate1 { get; set; }
        public double SphereRate2 { get; set; }
        public double SphereRate3 { get; set; }
        public double SphereRate4 { get; set; }
        public double SphereRate5 { get; set; }
        public double RateSum { get; set; }
        public double RatePercent { get; set; }
    }
}
