using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Models.FirstSection;
using Domain.Models.Ranking.Administrations;
using Domain.Models.Ranking;
using Domain.Models;
using EntityRepository;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.States;
using Domain;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Permission;
using MainInfrastructures.Services;

namespace AdminHandler.Handlers.Ranking
{
    public class FieldExceptionCommandHandler : IRequestHandler<FieldExceptionCommand, FieldExceptionCommandResult>
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

        public FieldExceptionCommandHandler(IRepository<Organizations, int> organization,
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
        public async Task<FieldExceptionCommandResult> Handle(FieldExceptionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new FieldExceptionCommandResult() { IsSuccess = true};
        }

        public void Add(FieldExceptionCommand model)
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
                #region calculate rate

                double rate = 0;
                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _gSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _gField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }

                rate = gotRate / maxRate;

                if (rate == 0)
                    rate = 1;

                #endregion

                #region clear old ranks
                var field = _gField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.GSubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<GRankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion

                #region add new ranks

                List<GRankTable> addList = new List<GRankTable>();
                
            

                if (field.GSubFields.Count() > 0)
                {
                   
                    foreach(var subField in field.GSubFields)
                    {
                        GRankTable addModel = new GRankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            IsException = true,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            ExpertId = model.UserId,
                            ExpertPinfl = model.UserPinfl,
                            SubFieldId = subField.Id,
                            Rank = subField.MaxRate * rate,
                            ModifiedDate = DateTime.Now,
                            CreatedDAte = DateTime.Now,
                        };

                        addList.Add(addModel);
                    }                    
                }
                else
                {
                    GRankTable addModel = new GRankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        IsException = true,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        Rank = field.MaxRate * rate,
                        ModifiedDate = DateTime.Now,
                        CreatedDAte = DateTime.Now,
                    };

                    addList.Add(addModel);
                }

                _db.Context.Set<GRankTable>().AddRange(addList);
                _db.Context.SaveChanges();

                #endregion 
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {
                #region calculate rate

                double rate = 0;
                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _xSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _xField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }

                rate = gotRate / maxRate;

                if (rate == 0)
                    rate = 1;

                #endregion

                #region clear old ranks
                var field = _xField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.XSubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<XRankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion

                #region add new ranks

                List<XRankTable> addList = new List<XRankTable>();



                if (field.XSubFields.Count() > 0)
                {

                    foreach (var subField in field.XSubFields)
                    {
                        XRankTable addModel = new XRankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            IsException = true,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            ExpertId = model.UserId,
                            ExpertPinfl = model.UserPinfl,
                            SubFieldId = subField.Id,
                            Rank = subField.MaxRate * rate,
                            ModifiedDate = DateTime.Now,
                            CreatedDAte = DateTime.Now,
                        };

                        addList.Add(addModel);
                    }
                }
                else
                {
                    XRankTable addModel = new XRankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        IsException = true,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        Rank = field.MaxRate * rate,
                        ModifiedDate = DateTime.Now,
                        CreatedDAte = DateTime.Now,
                    };

                    addList.Add(addModel);
                }

                _db.Context.Set<XRankTable>().AddRange(addList);
                _db.Context.SaveChanges();

                #endregion 
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {
                #region calculate rate

                double rate = 0;
                double maxRate = 0;
                double gotRate = 0;

                var allRanks = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter).ToList();

                foreach (var rank in allRanks)
                {
                    gotRate += rank.Rank;

                    if (rank.SubFieldId != 0)
                    {
                        var subField = _aSubField.Find(s => s.Id == rank.SubFieldId).FirstOrDefault();

                        if (subField == null)
                            throw ErrorStates.NotFound("sub field ");

                        maxRate += subField.MaxRate;
                    }
                    else
                    {
                        var f = _aField.Find(f => f.Id == rank.FieldId).FirstOrDefault();

                        maxRate += f.MaxRate;
                    }
                }

                rate = gotRate / maxRate;

                if (rate == 0)
                    rate = 1;

                #endregion

                #region clear old ranks
                var field = _aField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.ASubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<ARankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion

                #region add new ranks

                List<ARankTable> addList = new List<ARankTable>();



                if (field.ASubFields.Count() > 0)
                {

                    foreach (var subField in field.ASubFields)
                    {
                        ARankTable addModel = new ARankTable()
                        {
                            OrganizationId = model.OrganizationId,
                            Year = model.Year,
                            Quarter = model.Quarter,
                            IsException = true,
                            SphereId = field.SphereId,
                            FieldId = field.Id,
                            Comment = model.Comment,
                            ExpertId = model.UserId,
                            ExpertPinfl = model.UserPinfl,
                            SubFieldId = subField.Id,
                            Rank = subField.MaxRate * rate,
                            ModifiedDate = DateTime.Now,
                            CreatedDAte = DateTime.Now,
                        };

                        addList.Add(addModel);
                    }
                }
                else
                {
                    ARankTable addModel = new ARankTable()
                    {
                        OrganizationId = model.OrganizationId,
                        Year = model.Year,
                        Quarter = model.Quarter,
                        IsException = true,
                        SphereId = field.SphereId,
                        FieldId = field.Id,
                        Comment = model.Comment,
                        ExpertId = model.UserId,
                        ExpertPinfl = model.UserPinfl,
                        Rank = field.MaxRate * rate,
                        ModifiedDate = DateTime.Now,
                        CreatedDAte = DateTime.Now,
                    };

                    addList.Add(addModel);
                }

                _db.Context.Set<ARankTable>().AddRange(addList);
                _db.Context.SaveChanges();

                #endregion 
            }
        }

        public void Delete(FieldExceptionCommand model)
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
                #region clear old ranks
                var field = _gField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.GSubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _gRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<GRankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.FarmOrganizations)
            {

                #region clear old ranks
                var field = _xField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.XSubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _xRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<XRankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion
            }
            if (org.OrgCategory == Domain.Enums.OrgCategory.Adminstrations)
            {

                #region clear old ranks

                var field = _aField.Find(r => r.Id == model.FieldId).Include(mbox => mbox.ASubFields).FirstOrDefault();
                if (field == null)
                    throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

                var ranksToDelete = _aRankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId);
                _db.Context.Set<ARankTable>().RemoveRange(ranksToDelete);
                _db.Context.SaveChanges();

                #endregion
            }
        }
    
    }
}
