using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ReesterModels
{
    public class FirstRequestResultModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public ReesterProjectStatus PassportStatus { get; set; }
        public bool HasTerms { get; set; }
        public bool HasExpertise { get; set; }
        public string LinkForSystem { get; set; }
        
        public DateTime LastUpdate { get; set; }
    }
}
