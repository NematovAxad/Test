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
        public int UserServiceId { get; set; }
        public OrgCategory Category { get; set; }
        public List<SphereRateElement> Spheres { get; set; }
        public double RateSum { get; set; }
        public double RatePercent { get; set; }
    }
    public class SphereRateElement
    {
        public int SphereId { get; set; }
        public string SphereName { get; set; }
        public string SphereSection { get; set; }
        public double SphereRate { get; set; }
    }
}
