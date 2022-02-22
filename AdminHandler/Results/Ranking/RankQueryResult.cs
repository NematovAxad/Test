using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdminHandler.Results.Ranking
{
    public class RankQueryResult
    {
        public int Count { get; set; }
        public List<Data> Data { get; set; }
    }
    public class Data
    {
        public int Id { get; set; }
        public int OrganizationId { get; set; }
        public int Year { get; set; }
        public Quarters Quarter { get; set; }
        public double Rank { get; set; }
        public bool IsException { get; set; }
        public int SphereId { get; set; }
        public int FieldId { get; set; }
        public string Comment { get; set; }
        public List<SubField> SubFields { get; set; }
        public List<Elements> Elements { get; set; }
    }
    public class SubField
    {
        public int RankId { get; set; }
        public int SubFieldId { get; set; }
        public double SubfieldRank { get; set; }
        public string Comment { get; set; }
        public List<Elements> Elements { get; set; }
    }
    public class Elements
    {
        public int RankdId { get; set; }
        public int ElementId { get; set; }
        public double ElementRank { get; set; }
        public string Comment { get; set; }
    }
}
