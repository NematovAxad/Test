using Domain.Models;
using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.MonitoringModels.Models
{
    [Table("project", Schema = "module_regions")]
    public class Project : IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }
        [Column("name_uz")]
        public string NameUz { get; set; }
        [Column("name_ru")]
        public string NameRu { get; set; }
        [Column("status")]
        public MonitoringProjectStatus Status { get; set; }
        [Column("project_purpose")]
        public string ProjectPurpose { get; set; }
        public ICollection<Financier> ProjectFinanciers { get; set; }
        [Column("cost_effective")]
        public string CostEffective { get; set; }
        [Column("problem")]
        public string Problem { get; set; }
        [Column("start_date")]
        public DateTime StartDate { get; set; }
        [Column("end_date")]
        public DateTime EndDate { get; set; }
        [Column("volume_forecast_funds")]
        public double VolumeForecastFunds { get; set; }
        [Column("raised_funds")]
        public double RaisedFunds { get; set; }
        [Column("payouts")]
        public double Payouts { get; set; }
        [Column("performencer_id")]
        [ForeignKey("Performencer")]
        public int PerformencerId { get; set; }
        public Performencer Performencer { get; set; }
        public ICollection<Stage> Stages { get; set; }
        [Column("application_id")]
        [ForeignKey("Applications")]
        public int ApplicationId { get; set; }
        public Application Applications { get; set; }
        public ICollection<Cooworkers> Cooworkers { get; set; }
    }
}
