using Domain.Models.FirstSection;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("reestr_project_automated_services", Schema = "reestrprojects")]
    public class ReestrProjectAutomatedServices : IDomain<int>
    {

        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }

        [Column("reestr_project_id")]
        public int ReestrProjectId { get; set; }

        [Column("project_service_exist")]
        public bool ProjectServiceExist { get; set; }

        [Column("project_functions_exist")]
        public bool ProjectFunctionsExist { get; set; }

        public ICollection<AutomatedFunctions> AutomatedFunctions { get; set; }

        public ICollection<AutomatedServices> AutomatedServices { get; set; }

        [Column("all_items")]
        public int AllItems { get; set; }

        [Column("excepted_items")]
        public int ExceptedItems { get; set; }

        [Column("expert_comment")]
        public string ExpertComment { get; set; }
    }
}
