using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.SeventhSection
{
    [Table("organization_computers", Schema = "organizations")]
    public class OrganizationComputers:IDomain<int>
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        public Organizations Organization { get; set; }
        [Column("all_cmputers")]
        public int AllComputers { get; set; }
        [Column("central_all_cmputers")]
        public int CentralAllComputers { get; set; }
        [Column("territorial_all_cmputers")]
        public int TerritorialAllComputers { get; set; }
        [Column("subordinate_all_cmputers")]
        public int SubordinateAllComputers { get; set; }
        [Column("working_cmputers")]
        public int WorkingComputers { get; set; }
        [Column("central_working_cmputers")]
        public int CentralWorkingComputers { get; set; }
        [Column("territorial_working_cmputers")]
        public int TerritorialWorkingComputers { get; set; }
        [Column("subordinate_working_cmputers")]
        public int SubordinateWorkingComputers { get; set; }
        [Column("connected_local_set")]
        public int ConnectedLocalSet { get; set; }
        [Column("central_connected_local_set")]
        public int CentralConnectedLocalSet { get; set; }
        [Column("territorial_connected_local_set")]
        public int TerritorialConnectedLocalSet { get; set; }
        [Column("subordinate_connected_local_set")]
        public int SubordinateConnectedLocalSet { get; set; }
        [Column("connected_network")]
        public int ConnectedNetwork { get; set; }
        [Column("central_connected_network")]
        public int CentralConnectedNetwork { get; set; }
        [Column("territorial_connected_network")]
        public int TerritorialConnectedNetwork { get; set; }
        [Column("subordinate_connected_network")]
        public int SubordinateConnectedNetwork { get; set; }
        [Column("connected_corporate_network")]
        public int ConnectedCorporateNetwork { get; set; }
        [Column("central_connected_corporate_network")]
        public int CentralConnectedCorporateNetwork { get; set; }
        [Column("territorial_connected_corporate_network")]
        public int TerritorialConnectedCorporateNetwork { get; set; }
        [Column("subordinate_connected_corporate_network")]
        public int SubordinateConnectedCorporateNetwork { get; set; }
        [Column("connected_exat")]
        public int ConnectedExat { get; set; }
        [Column("central_connected_exat")]
        public int CentralConnectedExat { get; set; }
        [Column("territorial_connected_exat")]
        public int TerritorialConnectedExat { get; set; }
        [Column("subordinate_connected_exat")]
        public int SubordinateConnectedExat { get; set; }
        [Column("connected_eijro")]
        public int ConnectedEijro { get; set; }
        [Column("central_connected_eijro")]
        public int CentralConnectedEijro { get; set; }
        [Column("territorial_connected_eijro")]
        public int TerritorialConnectedEijro { get; set; }
        [Column("subordinate_connected_eijro")]
        public int SubordinateConnectedEijro { get; set; }
        [Column("connected_project_gov")]
        public int ConnectedProjectGov { get; set; }
        [Column("central_connected_project_gov")]
        public int CentralConnectedProjectGov { get; set; }
        [Column("territorial_connected_project_gov")]
        public int TerritorialConnectedProjectGov { get; set; }
        [Column("subordinateconnected_project_gov")]
        public int SubordinateConnectedProjectGov { get; set; }
        [Column("connected_project_my_work")]
        public int ConnectedProjectMyWork { get; set; }
        [Column("central_connected_project_my_work")]
        public int CentralConnectedProjectMyWork { get; set; }
        [Column("territorial_connected_project_my_work")]
        public int TerritorialConnectedProjectMyWork { get; set; }
        [Column("subordinate_connected_project_my_work")]
        public int SubordinateConnectedProjectMyWork { get; set; }
    }
}
