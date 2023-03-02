using AdminHandler.Querys.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.Ranking.Administrations;
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
        private readonly IRepository<ARankTable, int> _aRankTable;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;

        public RankQueryHandler(IRepository<Organizations, int> organization, 
                                IRepository<GRankTable, int> gRankTable,
                                IRepository<GField, int> gField,
                                IRepository<GSubField, int> gSubField,
                                IRepository<XRankTable, int> xRankTable,
                                IRepository<XField, int> xField,
                                IRepository<XSubField, int> xSubField,
                                IRepository<ARankTable, int> aRankTable,
                                IRepository<AField, int> aField,
                                IRepository<ASubField, int> aSubField
                                )
        {
            _organization = organization;
            _gRankTable = gRankTable;
            _gField = gField;
            _gSubField = gSubField;
            _xRankTable = xRankTable;
            _xField = xField;
            _xSubField = xSubField;
            _aRankTable = aRankTable;
            _aField = aField;
            _aSubField = aSubField;
        }
        public async Task<RankQueryResult> Handle(RankQuery request, CancellationToken cancellationToken)
        {
            RankQueryResult result = new RankQueryResult() { Data = new List<Data>() };

            var org = _organization.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if(org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var rank = _gRankTable.Find(r=>r.OrganizationId == request.OrganizationId && r.Year == request.Year && r.Quarter == request.Quarter && r.SphereId == request.SphereId && r.FieldId == request.FieldId).ToList();
                if (rank.Count > 0)
                {
                    var subField = _gSubField.Find(s => s.FieldId == request.FieldId).ToList();
                    if (subField.Count() > 0)
                    {
                        Data data = new Data();
                        data.OrganizationId = rank.First().OrganizationId;
                        data.Year = rank.First().Year;
                        data.Quarter = rank.First().Quarter;
                        data.Rank = 0;
                        data.SphereId = rank.First().SphereId;
                        data.FieldId = rank.First().FieldId;
                        data.SubFields = new List<Results.Ranking.SubField>();

                        foreach (var sField in subField)
                        {
                            var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                            var subFieldRankWithoutElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId == 0).FirstOrDefault();
                            if (subFieldRankWithoutElements != null)
                            {
                                data.Rank = data.Rank + subFieldRankWithoutElements.Rank;
                                var subFieldToAdd = new Results.Ranking.SubField();
                                subFieldToAdd.RankId = subFieldRankWithoutElements.Id;
                                subFieldToAdd.SubFieldId = subFieldRankWithoutElements.SubFieldId;
                                subFieldToAdd.SubfieldRank = subFieldRankWithoutElements.Rank;
                                subFieldToAdd.IsException = subFieldRankWithoutElements.IsException;
                                subFieldToAdd.Comment = subFieldRankWithoutElements.Comment;

                                data.SubFields.Add(subFieldToAdd);
                            }
                            if (subFieldRankWithElements.Count() > 0)
                            {
                                var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                data.Rank = data.Rank + subfieldRankMedium;
                                var subFieldToAdd = new Results.Ranking.SubField() { Elements = new List<Elements>() };
                                subFieldToAdd.SubFieldId = subFieldRankWithElements.FirstOrDefault().SubFieldId;
                                subFieldToAdd.SubfieldRank = subfieldRankMedium;
                                foreach (var element in subFieldRankWithElements)
                                {
                                    subFieldToAdd.Elements.Add(new Elements() { RankdId = element.Id, ElementId = element.ElementId, ElementRank = element.Rank, Comment = element.Comment });
                                }
                                data.SubFields.Add(subFieldToAdd);
                            }
                        }
                        if(data.SubFields.All(s=>s.IsException == true))
                        {
                            data.IsException = true;
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                    else
                    {
                        Data data = new Data();
                        var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                        var rankWithouthElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId == 0).FirstOrDefault();
                        if (rankWithouthElements != null)
                        {
                            data.Id = rankWithouthElements.Id;
                            data.OrganizationId = rankWithouthElements.OrganizationId;
                            data.Year = rankWithouthElements.Year;
                            data.Quarter = rankWithouthElements.Quarter;
                            data.Rank = rankWithouthElements.Rank;
                            data.IsException = rankWithouthElements.IsException;
                            data.SphereId = rankWithouthElements.SphereId;
                            data.FieldId = rankWithouthElements.FieldId;
                            data.Comment = rankWithouthElements.Comment;
                        }
                        if (rankWithElements.Count() > 0)
                        {
                            data.Id = rankWithElements.First().Id;
                            data.OrganizationId = rankWithElements.First().OrganizationId;
                            data.Year = rankWithElements.First().Year;
                            data.Quarter = rankWithElements.First().Quarter;
                            data.Rank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            data.IsException = rankWithElements.First().IsException;
                            data.SphereId = rankWithElements.First().SphereId;
                            data.FieldId = rankWithElements.First().FieldId;
                            data.Comment = rankWithElements.First().Comment;

                            data.Elements = new List<Elements>();

                            foreach (var element in rankWithElements)
                            {
                                data.Elements.Add(new Elements() { ElementId = element.Id, ElementRank = element.Rank, Comment = element.Comment });
                            }
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                }
            }

            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var rank = _xRankTable.Find(r => r.OrganizationId == request.OrganizationId && r.Year == request.Year && r.Quarter == request.Quarter && r.SphereId == request.SphereId && r.FieldId == request.FieldId).ToList();
                if(rank.Count()>0)
                { 
                    var subField = _xSubField.Find(s => s.FieldId == request.FieldId).ToList();
                    if (subField.Count() > 0)
                    {
                        Data data = new Data();
                        data.OrganizationId = rank.First().OrganizationId;
                        data.Year = rank.First().Year;
                        data.Quarter = rank.First().Quarter;
                        data.Rank = 0;
                        data.SphereId = rank.First().SphereId;
                        data.FieldId = rank.First().FieldId;
                        data.SubFields = new List<Results.Ranking.SubField>();

                        foreach (var sField in subField)
                        {
                            var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                            var subFieldRankWithoutElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId == 0).FirstOrDefault();
                            if (subFieldRankWithoutElements != null)
                            {
                                data.Rank = data.Rank + subFieldRankWithoutElements.Rank;
                                var subFieldToAdd = new Results.Ranking.SubField();
                                subFieldToAdd.RankId = subFieldRankWithoutElements.Id;
                                subFieldToAdd.SubFieldId = subFieldRankWithoutElements.SubFieldId;
                                subFieldToAdd.SubfieldRank = subFieldRankWithoutElements.Rank;
                                subFieldToAdd.IsException = subFieldRankWithoutElements.IsException;
                                subFieldToAdd.Comment = subFieldRankWithoutElements.Comment;

                                data.SubFields.Add(subFieldToAdd);
                            }
                            if (subFieldRankWithElements.Count() > 0)
                            {
                                var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                data.Rank = data.Rank + subfieldRankMedium;
                                var subFieldToAdd = new Results.Ranking.SubField() { Elements = new List<Elements>() };
                                subFieldToAdd.SubFieldId = subFieldRankWithElements.FirstOrDefault().SubFieldId;
                                subFieldToAdd.SubfieldRank = subfieldRankMedium;
                                foreach (var element in subFieldRankWithElements)
                                {
                                    subFieldToAdd.Elements.Add(new Elements() {RankdId = element.Id, ElementId = element.ElementId, ElementRank = element.Rank, Comment = element.Comment });
                                }
                                data.SubFields.Add(subFieldToAdd);
                            }
                        }
                        if (data.SubFields.All(s => s.IsException == true))
                        {
                            data.IsException = true;
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                    else
                    {
                        Data data = new Data();
                        var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                        var rankWithouthElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId == 0).FirstOrDefault();
                        if (rankWithouthElements != null)
                        {
                            data.Id = rankWithouthElements.Id;
                            data.OrganizationId = rankWithouthElements.OrganizationId;
                            data.Year = rankWithouthElements.Year;
                            data.Quarter = rankWithouthElements.Quarter;
                            data.Rank = rankWithouthElements.Rank;
                            data.IsException = rankWithouthElements.IsException;
                            data.SphereId = rankWithouthElements.SphereId;
                            data.FieldId = rankWithouthElements.FieldId;
                            data.Comment = rankWithouthElements.Comment;
                        }
                        if (rankWithElements.Count() > 0)
                        {
                            data.Id = rankWithElements.First().Id;
                            data.OrganizationId = rankWithElements.First().OrganizationId;
                            data.Year = rankWithElements.First().Year;
                            data.Quarter = rankWithElements.First().Quarter;
                            data.Rank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            data.IsException = rankWithElements.First().IsException;
                            data.SphereId = rankWithElements.First().SphereId;
                            data.FieldId = rankWithElements.First().FieldId;
                            data.Comment = rankWithElements.First().Comment;

                            data.Elements = new List<Elements>();

                            foreach (var element in rankWithElements)
                            {
                                data.Elements.Add(new Elements() { ElementId = element.Id, ElementRank = element.Rank, Comment = element.Comment });
                            }
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                }
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var rank = _aRankTable.Find(r => r.OrganizationId == request.OrganizationId && r.Year == request.Year && r.Quarter == request.Quarter && r.SphereId == request.SphereId && r.FieldId == request.FieldId).ToList();
                if (rank.Count > 0)
                {
                    var subField = _aSubField.Find(s => s.FieldId == request.FieldId).ToList();
                    if (subField.Count() > 0)
                    {
                        Data data = new Data();
                        data.OrganizationId = rank.First().OrganizationId;
                        data.Year = rank.First().Year;
                        data.Quarter = rank.First().Quarter;
                        data.Rank = 0;
                        data.SphereId = rank.First().SphereId;
                        data.FieldId = rank.First().FieldId;
                        data.SubFields = new List<Results.Ranking.SubField>();

                        foreach (var sField in subField)
                        {
                            var subFieldRankWithElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId != 0).ToList();
                            var subFieldRankWithoutElements = rank.Where(r => r.SubFieldId == sField.Id && r.ElementId == 0).FirstOrDefault();
                            if (subFieldRankWithoutElements != null)
                            {
                                data.Rank = data.Rank + subFieldRankWithoutElements.Rank;
                                var subFieldToAdd = new Results.Ranking.SubField();
                                subFieldToAdd.RankId = subFieldRankWithoutElements.Id;
                                subFieldToAdd.SubFieldId = subFieldRankWithoutElements.SubFieldId;
                                subFieldToAdd.SubfieldRank = subFieldRankWithoutElements.Rank;
                                subFieldToAdd.IsException = subFieldRankWithoutElements.IsException;
                                subFieldToAdd.Comment = subFieldRankWithoutElements.Comment;

                                data.SubFields.Add(subFieldToAdd);
                            }
                            if (subFieldRankWithElements.Count() > 0)
                            {
                                var subfieldRankMedium = Math.Round(subFieldRankWithElements.Select(r => r.Rank).Sum() / subFieldRankWithElements.Count(), 2);
                                data.Rank = data.Rank + subfieldRankMedium;
                                var subFieldToAdd = new Results.Ranking.SubField() { Elements = new List<Elements>() };
                                subFieldToAdd.SubFieldId = subFieldRankWithElements.FirstOrDefault().SubFieldId;
                                subFieldToAdd.SubfieldRank = subfieldRankMedium;
                                foreach (var element in subFieldRankWithElements)
                                {
                                    subFieldToAdd.Elements.Add(new Elements() { RankdId = element.Id, ElementId = element.ElementId, ElementRank = element.Rank, Comment = element.Comment });
                                }
                                data.SubFields.Add(subFieldToAdd);
                            }
                        }
                        if (data.SubFields.All(s => s.IsException == true))
                        {
                            data.IsException = true;
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                    else
                    {
                        Data data = new Data();
                        var rankWithElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId != 0).ToList();
                        var rankWithouthElements = rank.Where(r => r.SubFieldId == 0 && r.ElementId == 0).FirstOrDefault();
                        if (rankWithouthElements != null)
                        {
                            data.Id = rankWithouthElements.Id;
                            data.OrganizationId = rankWithouthElements.OrganizationId;
                            data.Year = rankWithouthElements.Year;
                            data.Quarter = rankWithouthElements.Quarter;
                            data.Rank = rankWithouthElements.Rank;
                            data.IsException = rankWithouthElements.IsException;
                            data.SphereId = rankWithouthElements.SphereId;
                            data.FieldId = rankWithouthElements.FieldId;
                            data.Comment = rankWithouthElements.Comment;
                        }
                        if (rankWithElements.Count() > 0)
                        {
                            data.Id = rankWithElements.First().Id;
                            data.OrganizationId = rankWithElements.First().OrganizationId;
                            data.Year = rankWithElements.First().Year;
                            data.Quarter = rankWithElements.First().Quarter;
                            data.Rank = Math.Round(rankWithElements.Select(r => r.Rank).Sum() / rankWithElements.Count(), 2);
                            data.IsException = rankWithElements.First().IsException;
                            data.SphereId = rankWithElements.First().SphereId;
                            data.FieldId = rankWithElements.First().FieldId;
                            data.Comment = rankWithElements.First().Comment;

                            data.Elements = new List<Elements>();

                            foreach (var element in rankWithElements)
                            {
                                data.Elements.Add(new Elements() { ElementId = element.Id, ElementRank = element.Rank, Comment = element.Comment });
                            }
                        }
                        result.Count++;
                        result.Data.Add(data);
                    }
                }
            }
            return result;
        }
    }
}
