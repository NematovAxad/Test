using Domain.Enums;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("organization", Schema = "organizations")]
    public class Organizations : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("user_service_id")]
        public int UserServiceId { get; set; }
        [Column("full_name")]
        public string FullName { get; set; }
        [Column("full_name_ru")]
        public string FullNameRu { get; set; }

        [Column("short_name")]
        public string ShortName { get; set; }
        [Column("short_name_ru")]
        public string ShortNameRu { get; set; }
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
        [Column("cyber_security_id")]
        public int CyberSecurityId { get; set; }
        [Column("my_gov_id")]
        public int MyGovId { get; set; }
        [Column("fax")]
        public string Fax { get; set; }
        [Column("org_category")]
        public OrgCategory OrgCategory { get; set; }
        public ICollection<BasedDocuments> BasedDocuments { get; set; }
        public ICollection<SubOrganizations> SubOrganizations { get; set; }
        public ICollection<RankTable> OrgRanks { get; set; }

        [Column("is_active")]
        public bool? IsActive { get; set; }

        [Column("is_ict")]
        public bool? IsIct { get; set; }

        [Column("is_monitoring")]
        public bool? IsMonitoring { get; set; }

        [Column("has_org_documents")]
        public bool HasOrgDocuments { get; set; }

    }
}
