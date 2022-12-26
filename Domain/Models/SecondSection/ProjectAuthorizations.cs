﻿using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SecondSection
{
    [Table("project_authorizations", Schema = "reestrprojects")]
    public class ProjectAuthorizations:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("parent_id")]
        public int ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public ReestrProjectAuthorizations ReestrProjectAuthorizations { get; set; }

        [Column("authorization_type")]
        public ReestrProjectAuthorizationType AuthorizationType { get; set; }

        [Column("authorization_uri")]
        public string AuthorizationUri { get; set; }


        [Column("file_path")]
        public string FilePath { get; set; }
    }
}