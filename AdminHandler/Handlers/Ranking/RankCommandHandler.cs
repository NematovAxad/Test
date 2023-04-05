using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.Ranking;
using Domain.Models.Ranking.Administrations;
using Domain.Permission;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Ranking
{
    public class RankCommandHandler : IRequestHandler<RankCommand, RankCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<GRankTable, int> _gRankTable;
        private readonly IRepository<XRankTable, int> _xRankTable;
        private readonly IRepository<ARankTable, int> _aRankTable;
        private readonly IRepository<GSphere, int> _gSphere;
        private readonly IRepository<GField, int> _gField;
        private readonly IRepository<GSubField, int> _gSubField;
        private readonly IRepository<XSphere, int> _xSphere;
        private readonly IRepository<XField, int> _xField;
        private readonly IRepository<XSubField, int> _xSubField;
        private readonly IRepository<ASphere, int> _aSphere;
        private readonly IRepository<AField, int> _aField;
        private readonly IRepository<ASubField, int> _aSubField;
        private readonly IDataContext _db;

        public RankCommandHandler(  IRepository<Organizations, int> organization, 
                                    IRepository<Deadline, int> deadline, 
                                    IRepository<GRankTable, int> gRankTable,
                                    IRepository<XRankTable, int> xRankTable,
                                    IRepository<ARankTable, int> aRankTable,
                                    IRepository<GSphere, int> gSphere,
                                    IRepository<GField, int> gField,
                                    IRepository<GSubField, int> gSubField,
                                    IRepository<XSphere, int> xSphere,
                                    IRepository<XField, int> xField,
                                    IRepository<XSubField, int> xSubField,
                                    IRepository<ASphere, int> aSphere,
                                    IRepository<AField, int> aField,
                                    IRepository<ASubField, int> aSubField,
                                    IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _gRankTable = gRankTable;
            _xRankTable = xRankTable;
            _aRankTable = aRankTable;
            _gSphere = gSphere;
            _gField = gField;
            _gSubField = gSubField;
            _xSphere = xSphere;
            _xField = xField;
            _xSubField = xSubField;
            _aSphere = aSphere;
            _aField = aField;
            _aSubField = aSubField;
            _db = db;
        }
        public async Task<RankCommandResult> Handle(RankCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new RankCommandResult() { IsSuccess = true };
        }

        public void Add(RankCommand model)
        {
            if(model.Switch==true)
            {
                ExceptionCase(model);
            }
            else
            {
                if (model.FieldId == 0)
                    throw ErrorStates.Error(UIErrors.FieldIdNotProvided);

                var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.OrganizationId.ToString());

                var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
                if (deadline == null || deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(model.Quarter.ToString());
                if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                    throw ErrorStates.NotAllowed("permission");


                if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    var field = _gField.Find(r => r.Id == model.FieldId).Include(mbox=>mbox.GSubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());
                    GRankTable addModel = new GRankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        Rank = model.Rank,
                        IsException = model.IsException,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        CreatedDAte = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        SubFieldId = 0,
                        ElementId = 0
                    };

                    if (field.GSubFields.Count() > 0)
                    {
                        if (model.SubFieldId == 0)
                            throw ErrorStates.Error(UIErrors.SubFieldIdNotProvided);

                        var subField = _gSubField.Find(r => r.Id == model.SubFieldId && r.FieldId == model.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");
                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = subField.MaxRate;
                        }
                        addModel.SubFieldId = subField.Id;
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _gRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _gRankTable.Add(addModel);
                        }

                    }
                    else
                    {
                        if (model.Rank > field.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = field.MaxRate;
                        }
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _gRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _gRankTable.Add(addModel);
                        }
                    }

                }
                if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    var field = _xField.Find(r => r.Id == model.FieldId).Include(mbox=>mbox.XSubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());
                    XRankTable addModel = new XRankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        Rank = model.Rank,
                        IsException = model.IsException,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        CreatedDAte = DateTime.Now,
                        ModifiedDate = DateTime.Now,
                        SubFieldId = 0,
                        ElementId = 0
                    };
                    if (field.XSubFields.Count() > 0)
                    {
                        if (model.SubFieldId == 0)
                            throw ErrorStates.Error(UIErrors.SubFieldIdNotProvided);

                        var subField = _xSubField.Find(r => r.Id == model.SubFieldId && r.FieldId == model.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");
                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = subField.MaxRate;
                        }
                        addModel.SubFieldId = subField.Id;
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _xRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _xRankTable.Add(addModel);
                        }

                    }
                    else
                    {
                        if (model.Rank > field.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = field.MaxRate;
                        }
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _xRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _xRankTable.Add(addModel);
                        }
                    }

                }
                if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                {
                    var field = _aField.Find(r => r.Id == model.FieldId).Include(mbox=>mbox.ASubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());
                    ARankTable addModel = new ARankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        Rank = model.Rank,
                        IsException = model.IsException,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        CreatedDAte = DateTime.Now,
                        ModifiedDate = DateTime.Now,    
                        SubFieldId = 0,
                        ElementId = 0
                    };

                    if (field.ASubFields.Count() > 0)
                    {
                        if (model.SubFieldId == 0)
                            throw ErrorStates.Error(UIErrors.SubFieldIdNotProvided);

                        var subField = _aSubField.Find(r => r.Id == model.SubFieldId && r.FieldId == model.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");
                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = subField.MaxRate;
                        }
                        addModel.SubFieldId = subField.Id;
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _aRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _aRankTable.Add(addModel);
                        }

                    }
                    else
                    {
                        if (model.Rank > field.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");
                        if (model.IsException == true)
                        {
                            addModel.Rank = field.MaxRate;
                        }
                        if (model.ElementId != 0)
                        {
                            var rankWithoutElement = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == 0).FirstOrDefault();
                            if (rankWithoutElement != null)
                                throw ErrorStates.NotAllowed("ranking should be with element id");

                            var rank = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.ElementId == model.ElementId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            addModel.ElementId = model.ElementId;
                            _aRankTable.Add(addModel);
                        }
                        else
                        {
                            var rank = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
                            if (rank != null)
                                throw ErrorStates.NotAllowed("ranking ");
                            _aRankTable.Add(addModel);
                        }
                    }

                }
            }
        }
        public void Update(RankCommand model)
        {
            if(model.Switch == true)
            {
                ExceptionCase(model);
            }
            else
            {
                var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
                if (deadline == null || deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(model.Quarter.ToString());

                var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.OrganizationId.ToString());

                if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
                {
                    var rank = _gRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                    if (rank == null)
                        throw ErrorStates.NotFound("rank for " + model.OrganizationId.ToString());


                    var field = _gField.Find(r => r.Id == rank.FieldId).Include(mbox=>mbox.GSubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());


                    if (model.Rank > field.MaxRate)
                        throw ErrorStates.NotAllowed("incorrect mark");


                    if (field.GSubFields.Count() > 0)
                    {
                        var subField = _gSubField.Find(r => r.Id == rank.SubFieldId && r.FieldId == rank.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");

                        if (model.IsException == true)
                        {
                            rank.Rank = subField.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }

                    }
                    else
                    {
                        if (model.IsException == true)
                        {
                            rank.Rank = field.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }
                    }
                    rank.IsException = false;
                    rank.Comment = model.Comment;
                    rank.ExpertId = model.UserId;
                    rank.ExpertPinfl = model.UserPinfl;
                    rank.ModifiedDate = DateTime.Now;
                    _gRankTable.Update(rank);
                }
                if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
                {
                    var rank = _xRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                    if (rank == null)
                        throw ErrorStates.NotFound("rank for " + model.OrganizationId.ToString());

                    var field = _xField.Find(r => r.Id == rank.FieldId).Include(mbox=>mbox.XSubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                    if (model.Rank > field.MaxRate)
                        throw ErrorStates.NotAllowed("incorrect mark");

                    if (field.XSubFields.Count() > 0)
                    {
                        var subField = _xSubField.Find(r => r.Id == rank.SubFieldId && r.FieldId == rank.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");

                        if (model.IsException == true)
                        {
                            rank.Rank = subField.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }
                    }
                    else
                    {
                        if (model.IsException == true)
                        {
                            rank.Rank = field.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }
                    }
                    rank.IsException = false;
                    rank.Comment = model.Comment;
                    rank.ExpertId = model.UserId;
                    rank.ExpertPinfl = model.UserPinfl;
                    rank.ModifiedDate = DateTime.Now;
                    _xRankTable.Update(rank);
                }
                if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
                {
                    var rank = _aRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                    if (rank == null)
                        throw ErrorStates.NotFound("rank for " + model.OrganizationId.ToString());

                    var field = _aField.Find(r => r.Id == rank.FieldId).Include(mbox=>mbox.ASubFields).FirstOrDefault();
                    if (field == null)
                        throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                    if (model.Rank > field.MaxRate)
                        throw ErrorStates.NotAllowed("incorrect mark");

                    if (field.ASubFields.Count() > 0)
                    {
                        var subField = _aSubField.Find(r => r.Id == rank.SubFieldId && r.FieldId == rank.FieldId).FirstOrDefault();
                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        if (model.Rank > subField.MaxRate)
                            throw ErrorStates.NotAllowed("incorrect mark");

                        if (model.IsException == true)
                        {
                            rank.Rank = subField.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }

                    }
                    else
                    {
                        if (model.IsException == true)
                        {
                            rank.Rank = field.MaxRate;
                        }
                        else
                        {
                            rank.Rank = model.Rank;
                        }
                    }
                    rank.IsException = false;
                    rank.Comment = model.Comment;
                    rank.ExpertId = model.UserId;
                    rank.ExpertPinfl = model.UserPinfl;
                    rank.ModifiedDate = DateTime.Now;
                    _aRankTable.Update(rank);
                }
            }
        }
        public void Delete(RankCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            if (org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var rank = _gRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                if (rank == null)
                    throw ErrorStates.NotFound(model.Id.ToString());
                _gRankTable.Remove(rank);
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var rank = _xRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                if (rank == null)
                    throw ErrorStates.NotFound(model.Id.ToString());
                _xRankTable.Remove(rank);
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var rank = _aRankTable.Find(r => r.Id == model.Id).FirstOrDefault();
                if (rank == null)
                    throw ErrorStates.NotFound(model.Id.ToString());
                _aRankTable.Remove(rank);
            }
        }
        public void ExceptionCase(RankCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            if(org.OrgCategory == Domain.Enums.OrgCategory.GovernmentOrganizations)
            {
                var field = _gField.Find(r => r.Id == model.FieldId).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());
                
                if(model.SubFieldId!=0)
                {
                    var subField = _gSubField.Find(r => r.Id == model.SubFieldId).FirstOrDefault();
                    if (subField == null)
                        throw ErrorStates.NotFound("sub field ");

                    var rank = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).ToList();
                        
                    if (rank.Count()>0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<GRankTable>().RemoveRange(rank);
                        _db.Context.SaveChanges();
                    }
                    if (model.SwitchValue == 0)
                    {
                        if(rank.Count()>0)
                        {
                            _db.Context.Set<GRankTable>().RemoveRange(rank);
                            _db.Context.SaveChanges();
                        }
                        GRankTable addModel = new GRankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            Rank = model.Rank,
                            IsException = true,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            SubFieldId = 0,
                            ElementId = 0
                        };
                        addModel.Rank = subField.MaxRate;
                        addModel.IsException = true;
                        addModel.SubFieldId = subField.Id;
                        _gRankTable.Update(addModel);
                    }
                    
                }
                else
                 {
                    var ranks = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).ToList();
                    if(ranks.Count()>0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<GRankTable>().RemoveRange(ranks);
                        _db.Context.SaveChanges();
                    }
                    if(model.SwitchValue == 0)
                    {
                        if(ranks.Count()>0)
                        {
                            _db.Context.Set<GRankTable>().RemoveRange(ranks);
                            _db.Context.SaveChanges();
                        }
                        var sFields = _gSubField.Find(f => f.FieldId == field.Id).ToList();
                        if(sFields.Count()>0)
                        {
                            List<GRankTable> addList = new List<GRankTable>();
                            foreach(var s in sFields)
                            {
                                GRankTable addModel = new GRankTable()
                                {
                                    OrganizationId = model.OrganizationId,
                                    Year = model.Year,
                                    Quarter = model.Quarter,
                                    Rank = model.Rank,
                                    IsException = true,
                                    SphereId = field.SphereId,
                                    FieldId = field.Id,
                                    Comment = model.Comment,
                                    SubFieldId = 0,
                                    ElementId = 0
                                };
                                addModel.Rank = s.MaxRate;
                                addModel.IsException = true;
                                addModel.SubFieldId = s.Id;
                                addList.Add(addModel);
                            }
                            _gRankTable.AddRange(addList);
                        }
                        else
                        {
                            GRankTable addModel = new GRankTable()
                            {
                                OrganizationId = model.OrganizationId,
                                Year = model.Year,
                                Quarter = model.Quarter,
                                Rank = model.Rank,
                                IsException = model.IsException,
                                SphereId = field.SphereId,
                                FieldId = field.Id,
                                Comment = model.Comment,
                                SubFieldId = 0,
                                ElementId = 0
                            };
                            addModel.Rank = field.MaxRate;
                            addModel.IsException = true;
                            _gRankTable.Add(addModel);
                        }
                        
                    }
                }
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                var field = _xField.Find(r => r.Id == model.FieldId).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                if (model.SubFieldId != 0)
                {
                    var subField = _xSubField.Find(r => r.Id == model.SubFieldId).FirstOrDefault();
                    if (subField == null)
                        throw ErrorStates.NotFound("sub field ");

                    var rank = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).ToList();

                    if (rank.Count() > 0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<XRankTable>().RemoveRange(rank);
                        _db.Context.SaveChanges();
                    }
                    if (model.SwitchValue == 0)
                    {
                        if (rank.Count() > 0)
                        {
                            _db.Context.Set<XRankTable>().RemoveRange(rank);
                            _db.Context.SaveChanges();
                        }
                        XRankTable addModel = new XRankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            Rank = model.Rank,
                            IsException = model.IsException,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            SubFieldId = 0,
                            ElementId = 0
                        };
                        addModel.Rank = subField.MaxRate;
                        addModel.IsException = true;
                        addModel.SubFieldId = subField.Id;
                        _xRankTable.Update(addModel);
                    }

                }
                else
                {
                    var ranks = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).ToList();
                    if (ranks.Count() > 0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<XRankTable>().RemoveRange(ranks);
                        _db.Context.SaveChanges();
                    }
                    if (model.SwitchValue == 0)
                    {
                        if (ranks.Count() > 0)
                        {
                            _db.Context.Set<XRankTable>().RemoveRange(ranks);
                            _db.Context.SaveChanges();
                        }
                        var sFields = _xSubField.Find(f => f.FieldId == field.Id).ToList();
                        if (sFields.Count() > 0)
                        {
                            List<XRankTable> addList = new List<XRankTable>();
                            foreach (var s in sFields)
                            {
                                XRankTable addModel = new XRankTable()
                                {
                                    OrganizationId = model.OrganizationId,
                                    Year = model.Year,
                                    Quarter = model.Quarter,
                                    Rank = model.Rank,
                                    IsException = model.IsException,
                                    SphereId = field.SphereId,
                                    FieldId = field.Id,
                                    Comment = model.Comment,
                                    SubFieldId = 0,
                                    ElementId = 0
                                };
                                addModel.Rank = s.MaxRate;
                                addModel.IsException = true;
                                addModel.SubFieldId = s.Id;
                                addList.Add(addModel);
                            }
                            _xRankTable.AddRange(addList);
                        }
                        else
                        {
                            XRankTable addModel = new XRankTable()
                            {
                                OrganizationId = model.OrganizationId,
                                Year = model.Year,
                                Quarter = model.Quarter,
                                Rank = model.Rank,
                                IsException = model.IsException,
                                SphereId = field.SphereId,
                                FieldId = field.Id,
                                Comment = model.Comment,
                                SubFieldId = 0,
                                ElementId = 0
                            };
                            addModel.Rank = field.MaxRate;
                            addModel.IsException = true;
                            _xRankTable.Add(addModel);
                        }

                    }
                }
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                var field = _aField.Find(r => r.Id == model.FieldId).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                if (model.SubFieldId != 0)
                {
                    var subField = _aSubField.Find(r => r.Id == model.SubFieldId).FirstOrDefault();
                    if (subField == null)
                        throw ErrorStates.NotFound("sub field ");

                    var rank = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId && r.SubFieldId == model.SubFieldId).ToList();

                    if (rank.Count() > 0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<ARankTable>().RemoveRange(rank);
                        _db.Context.SaveChanges();
                    }
                    if (model.SwitchValue == 0)
                    {
                        if (rank.Count() > 0)
                        {
                            _db.Context.Set<ARankTable>().RemoveRange(rank);
                            _db.Context.SaveChanges();
                        }
                        ARankTable addModel = new ARankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            Rank = model.Rank,
                            IsException = true,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            SubFieldId = 0,
                            ElementId = 0
                        };
                        addModel.Rank = subField.MaxRate;
                        addModel.IsException = true;
                        addModel.SubFieldId = subField.Id;
                        _aRankTable.Update(addModel);
                    }

                }
                else
                {
                    var ranks = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).ToList();
                    if (ranks.Count() > 0 && model.SwitchValue == 1)
                    {
                        _db.Context.Set<ARankTable>().RemoveRange(ranks);
                        _db.Context.SaveChanges();
                    }
                    if (model.SwitchValue == 0)
                    {
                        if (ranks.Count() > 0)
                        {
                            _db.Context.Set<ARankTable>().RemoveRange(ranks);
                            _db.Context.SaveChanges();
                        }
                        var sFields = _aSubField.Find(f => f.FieldId == field.Id).ToList();
                        if (sFields.Count() > 0)
                        {
                            List<ARankTable> addList = new List<ARankTable>();
                            foreach (var s in sFields)
                            {
                                ARankTable addModel = new ARankTable()
                                {
                                    OrganizationId = model.OrganizationId,
                                    Year = model.Year,
                                    Quarter = model.Quarter,
                                    Rank = model.Rank,
                                    IsException = true,
                                    SphereId = field.SphereId,
                                    FieldId = field.Id,
                                    Comment = model.Comment,
                                    SubFieldId = 0,
                                    ElementId = 0
                                };
                                addModel.Rank = s.MaxRate;
                                addModel.IsException = true;
                                addModel.SubFieldId = s.Id;
                                addList.Add(addModel);
                            }
                            _aRankTable.AddRange(addList);
                        }
                        else
                        {
                            ARankTable addModel = new ARankTable()
                            {
                                OrganizationId = model.OrganizationId,
                                Year = model.Year,
                                Quarter = model.Quarter,
                                Rank = model.Rank,
                                IsException = model.IsException,
                                SphereId = field.SphereId,
                                FieldId = field.Id,
                                Comment = model.Comment,
                                SubFieldId = 0,
                                ElementId = 0
                            };
                            addModel.Rank = field.MaxRate;
                            addModel.IsException = true;
                            _aRankTable.Add(addModel);
                        }

                    }
                }
            }
        }
    }
}
