using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Enums;
using Domain.Models;
using Domain.Permission;
using Domain.States;
using EntityRepository;
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
    public class RankingCommandHandler : IRequestHandler<RankingCommand, RankingCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<RankTable, int> _rankTable;
        private readonly IRepository<Sphere, int> _sphere;
        private readonly IRepository<Field, int> _field;
        private readonly IDataContext _db;

        public RankingCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<RankTable, int> rankTable, IRepository<Sphere, int> sphere, IRepository<Field, int> field, IDataContext db)
        {
            _organization = organization;
            _deadline = deadline;
            _rankTable = rankTable;
            _sphere = sphere;
            _field = field;
            _db = db;
        }

        public async Task<RankingCommandResult> Handle(RankingCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new RankingCommandResult() { IsSuccess = true };
        }
        public void Add(RankingCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());

            var field = _field.Find(r => r.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());

            var rank = _rankTable.Find(r => r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.Quarter == model.Quarter && r.FieldId == model.FieldId).FirstOrDefault();
            if (rank != null)
                throw ErrorStates.NotAllowed("ranking " + model.OrganizationId.ToString() + " for " + model.Quarter + " quartetr!");

            if (model.Rank > field.MaxRate)
                throw ErrorStates.NotAllowed("incorrect mark");
            if (!model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            

            RankTable addModel = new RankTable()
            {
                OrganizationId = model.OrganizationId,
                Year = model.Year,
                Quarter = model.Quarter,
                Rank = model.Rank,
                IsException = model.IsException,
                SphereId = field.SphereId,
                FieldId = field.Id,
                Comment = model.Comment
            };
            _rankTable.Add(addModel);

            ExceptionCases(model.OrganizationId, model.FieldId, deadline.Id);
        }
        public void Update(RankingCommand model)
        {
            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());

            var rank = _rankTable.Find(r => r.Id == model.Id && r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.FieldId == model.FieldId).FirstOrDefault();
            if (rank == null)
                throw ErrorStates.NotFound("rank for " + model.OrganizationId.ToString());
            
            var field = _field.Find(r => r.Id == model.FieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound("rank field " + model.FieldId.ToString());
            if (model.Rank > field.MaxRate)
                throw ErrorStates.NotAllowed("incorrect mark");
            rank.IsException = model.IsException;
            rank.Rank = model.Rank;
            rank.Comment = model.Comment;
            _rankTable.Update(rank);
            ExceptionCases(model.OrganizationId, model.FieldId, deadline.Id);
        }
        public void Delete(RankingCommand model)
        {
            var rank = _rankTable.Find(r => r.Id == model.Id).FirstOrDefault();
            if (rank == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _rankTable.Remove(rank);
        }
        public void ExceptionCases(int orgId, int fieldId, int deadlineId)
        {
            var org = _organization.Find(o => o.Id == orgId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(orgId.ToString());
            var field = _field.Find(f => f.Id == fieldId).FirstOrDefault();
            if (field == null)
                throw ErrorStates.NotFound(fieldId.ToString());
            var deadline = _deadline.Find(d => d.Id == deadlineId).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound(deadlineId.ToString());
            var ranks = _rankTable.Find(r => r.OrganizationId == org.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.IsException == false && r.SphereId == field.SphereId).ToList();
            double rankSum = 0;
            if(ranks.Count>0)
                rankSum = ranks.Select(r => r.Rank).Sum();

            double maxRankSum = _field.Find(f => f.SphereId == field.SphereId).Select(f => f.MaxRate).Sum();

            double percent = Math.Round(rankSum / maxRankSum, 2);

            if (percent == 0)
                percent = 1;

            var exceptionRanks = _rankTable.Find(r => r.OrganizationId == org.Id && r.Year == deadline.Year && r.Quarter == deadline.Quarter && r.IsException == true && r.SphereId == field.SphereId).ToList();
            foreach(var eRank in exceptionRanks)
            {
                eRank.Rank = Math.Round(_field.Find(f => f.Id == eRank.FieldId).Select(f => f.MaxRate).Sum() * percent, 2);
                _rankTable.Update(eRank);
            }
        }
    }
}
