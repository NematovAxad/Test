﻿using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models
{
    [Table("organization", Schema = "organizations")]
    public class Organizations:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("short_name")]
        public string ShortName { get; set; }
        [Column("director_first_name")]
        public string DirectorFirstName { get; set; }
        [Column("director_last_name")]
        public string DirectorLastName { get; set; }
        [Column("director_mid_name")]
        public string DirectorMidName { get; set; }
        [Column("director_position")]
        public string DirectorPosition { get; set; }
        [Column("phone_number")]
        public string PhoneNumber { get; set; }
        [Column("address_home_no")]
        public string AddressHomeNo { get; set; }
        [Column("address_street")]
        public string AddressStreet { get; set; }
        [Column("address_province")]
        public string AddressProvince { get; set; }
        [Column("address_district")]
        public string AddressDistrict { get; set; }
        [Column("post_index")]
        public string PostIndex { get; set; }
        [Column("department")]
        public string Department { get; set; }
        [Column("director_mail")]
        public string DirectorMail { get; set; }
        [Column("org_mail")]
        public string OrgMail { get; set; }
        [Column("web_site")]
        public string WebSite { get; set; }
        [Column("org_type")]
        public OrgTypes OrgType { get; set; }
        [Column("fax")]
        public string Fax { get; set; }
        [Column("org_category")]
        public OrgCategory OrgCategory { get; set; }
        public ICollection<BasedDocuments> BasedDocuments { get; set; }

    }
}
