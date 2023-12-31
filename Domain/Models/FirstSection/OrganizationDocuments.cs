﻿using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("organization_documents", Schema = "organizations")]
    public class OrganizationDocuments : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("document_no")]
        public string DocumentNo { get; set; }
        [Column("document_date")]
        public DateTime DocumentDate { get; set; }
        [Column("document_type")]
        public DocumentType DocumentType { get; set; }
        [Column("document_name")]
        public string DocumentName { get; set; }
        [Column("main_purpose")]
        public string MainPurpose { get; set; }
        [Column("path")]
        public string Path { get; set; }
        [Column("user_pinfl")]
        public string UserPinfl { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
