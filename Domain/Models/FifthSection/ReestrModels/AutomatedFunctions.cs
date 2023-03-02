using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FifthSection.ReestrModels
{
    [Table("automated_functions", Schema = "reestrprojects")]
    public class AutomatedFunctions : IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectAutomatedServices ReestrProjectAutomatedServices { get; set; }

        [Column("function_name")]
        public string FunctionName { get; set; }

        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
