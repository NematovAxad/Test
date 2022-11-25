using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("project_classifications", Schema = "reestrprojects")]
    public class ProjectClassifications:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectClassifications ReestrProjectClassifications { get; set; }

        [Column("classification_type")]
        public ReestrProjectClassificationType ClassificationType { get; set; }

        [Column("classification_uri")]
        public string ClassificationUri { get; set; }


        [Column("file_path")]
        public string FilePath { get; set; }
    }
}
