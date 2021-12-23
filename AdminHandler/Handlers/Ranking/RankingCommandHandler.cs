using AdminHandler.Commands.Ranking;
using AdminHandler.Results.Ranking;
using Domain.Enums;
using Domain.Models;
using Domain.Permission;
using Domain.States;
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

        public RankingCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<RankTable, int> rankTable, IRepository<Sphere, int> sphere, IRepository<Field, int> field)
        {
            _organization = organization;
            _deadline = deadline;
            _rankTable = rankTable;
            _sphere = sphere;
            _field = field;
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
                FieldId = field.Id
            };
            _rankTable.Add(addModel);
        }
        public void Update(RankingCommand model)
        {
            var deadline = _deadline.Find(d => d.Year == model.Year && d.Quarter == model.Quarter).FirstOrDefault();
            if (deadline == null || deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(model.Quarter.ToString());
            var rank = _rankTable.Find(r => r.Id == model.Id && r.OrganizationId == model.OrganizationId && r.Year == model.Year && r.FieldId == model.FieldId).FirstOrDefault();
            if (rank == null)
                throw ErrorStates.NotFound("rank for " + model.OrganizationId.ToString());
            if(model.IsException==false)
            {
                rank.Rank = model.Rank;
            }
            _rankTable.Update(rank);

        }
        public void Delete(RankingCommand model)
        {
            var rank = _rankTable.Find(r => r.Id == model.Id).FirstOrDefault();
            if (rank == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _rankTable.Remove(rank);
        }
    }
}
