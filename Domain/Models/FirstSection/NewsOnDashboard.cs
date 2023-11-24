using JohaRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Models.FirstSection
{
    [Table("news_on_dashboard", Schema = "organizations")]
    public class NewsOnDashboard:IDomain<int>
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("body")]
        public string Body { get; set; }

        [Column("file_link")]
        public string FileLink { get; set; }

        [Column("add_date")]
        public DateTime AddDate { get; set; }

        [Column("update_date")]
        public DateTime UpdateDate { get; set; }

        [Column("author_pinfl")]
        public string AuthorPinfl { get; set; }

        [Column("first")]
        public bool First { get; set; }

    }
}
