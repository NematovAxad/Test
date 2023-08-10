using System;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Models.FirstSection;
using JohaRepository;

namespace Domain.Models.SecondSection
{
    /// <summary>
    /// 
    /// </summary>
    [Table("open_data_table", Schema="opendata")]
    public class OpenDataTable:IDomain<int>
    {
        /// <summary>
        /// 
        /// </summary>
        [Column("id")]
        public int Id { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("organization_id")]
        [ForeignKey("Organizations")]
        public int OrganizationId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Organizations Organizations { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("table_id")]
        public string TableId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("table_name")]
        public string TableName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("update_date")]
        public DateTime UpdateDate { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("status")]
        public int Status { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("link")]
        public string Link { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        [Column("table_last_update_date")]
        public DateTime TableLastUpdateDate { get; set; }
        
    }
}