using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("employee_statistics", Schema = "organizations")]
    public class EmployeeStatistics : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organizations { get; set; }
        [Column("central_management_positions")]
        public int CentralManagementPositions { get; set; }
        [Column("central_management_employees")]
        public int CentralManagementEmployees { get; set; }
        [Column("territorial_management_positions")]
        public int TerritorialManagementPositions { get; set; }
        [Column("territorial_management_employees")]
        public int TerritorialManagementEmployees { get; set; }
        [Column("subordination_positions")]
        public int SubordinationPositions { get; set; }
        [Column("subordination_employees")]
        public int SubordinationEmployees { get; set; }
        [Column("other_positions")]
        public int OtherPositions { get; set; }
        [Column("other_employees")]
        public int OtherEmployees { get; set; }
        [Column("head_positions")]
        public int HeadPositions { get; set; }
        [Column("head_employees")]
        public int HeadEmployees { get; set; }
        [Column("department_head_positions")]
        public int DepartmentHeadPositions { get; set; }
        [Column("department_head_employees")]
        public int DepartmentHeadEmployees { get; set; }
        [Column("specialists_position")]
        public int SpecialistsPosition { get; set; }
        [Column("specialists_employee")]
        public int SpecialistsEmployee { get; set; }
        [Column("production_personnels_position")]
        public int ProductionPersonnelsPosition { get; set; }
        [Column("production_personnels_employee")]
        public int ProductionPersonnelsEmployee { get; set; }
        [Column("technical_stuff_positions")]
        public int TechnicalStuffPositions { get; set; }
        [Column("technical_stuff_employee")]
        public int TechnicalStuffEmployee { get; set; }
        [Column("service_stuff_positions")]
        public int ServiceStuffPositions { get; set; }
        [Column("service_stuff_employee")]
        public int ServiceStuffEmployee { get; set; }

    }
}
