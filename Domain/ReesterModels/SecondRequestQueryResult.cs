using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.ReesterModels
{
    public class SecondRequestQueryResult
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string ShortName { get; set; }
        public ReesterProjectStatus PassportStatus { get; set; }
        public string BasisName { get; set; }
        public string Tasks { get; set; }
        public bool IsInterdepartmentalInformationSystem { get; set; }
        public List<string> RepresentingGovernmentAgencyList { get; set; }
        public List<IdentityGetModel> IdentityTypes { get; set; }
        public string CybersecurityExpertise { get; set; }
        public DateTime UpdateTime { get; set; }
    }
    public class IdentityGetModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
