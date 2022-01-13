using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
using Domain.Models.Ranking;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Ranking
{
    public class RankQueryHandler : IRequestHandler<RankQuery, RankQueryResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<GRankTable, int> _gRankTable;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;

        public RankQueryHandler(IRepository<Organizations, int> organization, 
                                IRepository<GRankTable, int> gRankTable,
                                IRepository<GField, int> gField,
                                IRepository<GSubField, int> gSubField,
                                IRepository<XRankTable, int> xRankTable,
                                IRepository<XField, int> xField,
                                IRepository<XSubField, int> xSubField
                                )
        {
            _organization = organization;
            _gRankTable = gRankTable;
            _gField = gField;
            _gSubField = gSubField;
            _xRankTable = xRankTable;
            _xField = xField;
            _xSubField = xSubField;
        }
        public async Task<RankQueryResult> Handle(RankQuery request, CancellationToken cancellationToken)
        {
            RankQueryResult result = new RankQueryResult();

            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if(org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var rank = _gRankTable.Find(r=>r.OrganizationId == request.OrganizationId && r.Year == request.Year && r.Quarter == request.Quarter && r.SphereId == request.SphereId && r.FieldId == request.FieldId).ToList();
                var subField = _gSubField.Find(s => s.FieldId == request.FieldId).ToList();
                if(subField.Count()>0)
                {
                    result.OrganizationId = rank.First().OrganizationId;
                    result.Year = rank.First().Year;
                    result.Quarter = rank.First().Quarter;
                    result.Rank = 0;
                    result.SphereId = rank.First().SphereId;
                    result.FieldId = rank.First().FieldId;
                    result.SubFields = new List<Results.Ranking.SubField>();

                    foreach (var sField in subField)
                    {
                        var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                        var subFieldRankWithoutElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId == 0).FirstOrDefault();
                        if (subFieldRankWithoutElements != null)
                        {
                            result.Rank = result.Rank + subFieldRankWithoutElements.Rank;
                            var subFieldToAdd = new Results.Ranking.SubField();
                            subFieldToAdd.RankdId = subFieldRankWithoutElements.Id;
                            subFieldToAdd.SubFieldId = subFieldRankWithoutElements.SubFieldId;
                            subFieldToAdd.SubfieldRank = subFieldRankWithoutElements.Rank;
                            subFieldToAdd.Comment = subFieldRankWithoutElements.Comment;

                            result.SubFields.Add(subFieldToAdd);
                        }
                        if(subFieldRankWithElements.Count()>0)
                        {
                            var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                            result.Rank = result.Rank + subfieldRankMedium;
                            var subFieldToAdd = new Results.Ranking.SubField() { Elements = new List<Elements>()};
                            subFieldToAdd.SubFieldId = subFieldRankWithElements.FirstOrDefault().SubFieldId;
                            subFieldToAdd.SubfieldRank = subfieldRankMedium;
                            foreach(var element in subFieldRankWithElements)
                            {
                                subFieldToAdd.Elements.Add(new Elements() {RankdId = element.Id, ElementId = element.ElementId, ElementRank = element.Rank, Comment = element.Comment });
                            }
                            result.SubFields.Add(subFieldToAdd);
                        }
                    }
                }
                else
                {
                    var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                    var rankWithouthElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId == 0).FirstOrDefault();
                    if (rankWithouthElements!=null)
                    {
                        result.Id = rankWithouthElements.Id;
                        result.OrganizationId = rankWithouthElements.OrganizationId;
                        result.Year = rankWithouthElements.Year;
                        result.Quarter = rankWithouthElements.Quarter;
                        result.Rank = rankWithouthElements.Rank;
                        result.IsException = rankWithouthElements.IsException;
                        result.SphereId = rankWithouthElements.SphereId; 
                        result.FieldId = rankWithouthElements.FieldId;
                        result.Comment = rankWithouthElements.Comment;
                    }
                    if(rankWithElements.Count()>0)
                    {
                        result.Id = rankWithElements.First().Id;
                        result.OrganizationId = rankWithElements.First().OrganizationId;
                        result.Year = rankWithElements.First().Year;
                        result.Quarter = rankWithElements.First().Quarter;
                        result.Rank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                        result.IsException = rankWithElements.First().IsException;
                        result.SphereId = rankWithElements.First().SphereId;
                        result.FieldId = rankWithElements.First().FieldId;
                        result.Comment = rankWithElements.First().Comment;

                        result.Elements = new List<Elements>();
                        
                        foreach(var element in rankWithElements)
                        {
                            result.Elements.Add(new Elements() { ElementId = element.Id, ElementRank = element.Rank, Comment = element.Comment });
                        }
                        
                    }

                }
            }

            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var rank = _xRankTable.Find(r => r.OrganizationId == request.OrganizationId && r.Year == request.Year && r.Quarter == request.Quarter && r.SphereId == request.SphereId && r.FieldId == request.FieldId).ToList();
                var subField = _xSubField.Find(s => s.FieldId == request.FieldId).ToList();
                if (subField.Count() > 0)
                {
                    result.OrganizationId = rank.First().OrganizationId;
                    result.Year = rank.First().Year;
                    result.Quarter = rank.First().Quarter;
                    result.Rank = 0;
                    result.SphereId = rank.First().SphereId;
                    result.FieldId = rank.First().FieldId;
                    result.SubFields = new List<Results.Ranking.SubField>();

                    foreach (var sField in subField)
                    {
                        var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                        var subFieldRankWithoutElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId == 0).FirstOrDefault();
                        if (subFieldRankWithoutElements != null)
                        {
                            result.Rank = result.Rank + subFieldRankWithoutElements.Rank;
                            var subFieldToAdd = new Results.Ranking.SubField();
                            subFieldToAdd.SubFieldId = subFieldRankWithoutElements.SubFieldId;
                            subFieldToAdd.SubfieldRank = subFieldRankWithoutElements.Rank;
                            subFieldToAdd.Comment = subFieldRankWithoutElements.Comment;

                            result.SubFields.Add(subFieldToAdd);
                        }
                        if (subFieldRankWithElements.Count() > 0)
                        {
                            var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                            result.Rank = result.Rank + subfieldRankMedium;
                            var subFieldToAdd = new Results.Ranking.SubField() { Elements = new List<Elements>() };
                            subFieldToAdd.SubFieldId = subFieldRankWithElements.FirstOrDefault().SubFieldId;
                            subFieldToAdd.SubfieldRank = subfieldRankMedium;
                            foreach (var element in subFieldRankWithElements)
                            {
                                subFieldToAdd.Elements.Add(new Elements() { ElementId = element.ElementId, ElementRank = element.Rank, Comment = element.Comment });
                            }
                            result.SubFields.Add(subFieldToAdd);
                        }
                    }
                }
                else
                {
                    var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                    var rankWithouthElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId == 0).FirstOrDefault();
                    if (rankWithouthElements != null)
                    {
                        result.Id = rankWithouthElements.Id;
                        result.OrganizationId = rankWithouthElements.OrganizationId;
                        result.Year = rankWithouthElements.Year;
                        result.Quarter = rankWithouthElements.Quarter;
                        result.Rank = rankWithouthElements.Rank;
                        result.IsException = rankWithouthElements.IsException;
                        result.SphereId = rankWithouthElements.SphereId;
                        result.FieldId = rankWithouthElements.FieldId;
                        result.Comment = rankWithouthElements.Comment;
                    }
                    if (rankWithElements.Count() > 0)
                    {
                        result.Id = rankWithElements.First().Id;
                        result.OrganizationId = rankWithElements.First().OrganizationId;
                        result.Year = rankWithElements.First().Year;
                        result.Quarter = rankWithElements.First().Quarter;
                        result.Rank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                        result.IsException = rankWithElements.First().IsException;
                        result.SphereId = rankWithElements.First().SphereId;
                        result.FieldId = rankWithElements.First().FieldId;
                        result.Comment = rankWithElements.First().Comment;

                        result.Elements = new List<Elements>();

                        foreach (var element in rankWithElements)
                        {
                            result.Elements.Add(new Elements() { ElementId = element.Id, ElementRank = element.Rank, Comment = element.Comment });
                        }

                    }

                }
            }
            return result;
        }
    }
}
