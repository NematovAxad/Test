using System;
using Domain.Enums;

namespace Domain.ReesterModels
{
    public class FirstRequestResultModelNew
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public ReesterProjectStatus PassportStatus { get; set; }
        public bool HasCyberSecurityExpertise { get; set; }
        public bool HasDigitalTechnologyMinistryExpertise { get; set; }
        public string LinkForSystem { get; set; }
        
        public DateTime LastUpdate { get; set; }
    }
}