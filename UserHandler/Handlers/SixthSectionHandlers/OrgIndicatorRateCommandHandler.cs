using Domain.Models.FirstSection;
using Domain.Models.SixthSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SixthSectionCommands;
using UserHandler.Results.SixthSectionResults;
using Domain.States;
using Domain;
using MainInfrastructures.Migrations;
using System.Linq;
using Domain.Permission;

namespace UserHandler.Handlers.SixthSectionHandlers
{
    public class OrgIndicatorRateCommandHandler : IRequestHandler<OrgIndicatorRateCommand, OrgIndicatorRateCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<IndicatorRating, int> _indicatorRating;

        public OrgIndicatorRateCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<IndicatorRating, int> indicatorRating)
        {
            _organization = organization;
            _deadline = deadline;
            _indicatorRating = indicatorRating;
        }

        public async Task<OrgIndicatorRateCommandResult> Handle(OrgIndicatorRateCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Update:
                    id = Update(request);
                    break;
                case Domain.Enums.EventType.Delete:
                    id = Delete(request);
                    break;
            }
            return new OrgIndicatorRateCommandResult() { Id = id, IsSuccess = false };
        }

        public int Add(OrgIndicatorRateCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            var indicatorRate = _indicatorRating.Find(p => p.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (indicatorRate != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            IndicatorRating addModel = new IndicatorRating();
            addModel.OrganizationId = model.OrganizationId;
            addModel.AllIndicators = model.AllIndicators;
            addModel.CompleteIndicators = model.CompleteIndicators;
            addModel.ExpertComment = model.ExpertComment;
            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _indicatorRating.Add(addModel);

            return addModel.Id;
        }
        public int Update(OrgIndicatorRateCommand model)
        {

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.OperatorDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var indicatorRate = _indicatorRating.Find(p => p.Id == model.Id).FirstOrDefault();
            if (indicatorRate == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            var org = _organization.Find(o => o.Id == indicatorRate.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);



            indicatorRate.AllIndicators = model.AllIndicators;
            indicatorRate.CompleteIndicators = model.CompleteIndicators;
            indicatorRate.ExpertComment = model.ExpertComment;
            indicatorRate.UserPinfl = model.UserPinfl;
            indicatorRate.LastUpdate = DateTime.Now;


            _indicatorRating.Update(indicatorRate);

            return indicatorRate.Id;
        }
        public int Delete(OrgIndicatorRateCommand model)
        {
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !(model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS)))
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);


            var indicatorRate = _indicatorRating.Find(p => p.Id == model.Id).FirstOrDefault();
            if (indicatorRate == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _indicatorRating.Remove(indicatorRate);

            return indicatorRate.Id;
        }
    }
}
