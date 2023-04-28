using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.ThirdSection;
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
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.ReestrPassportResult;
using UserHandler.Results.ThirdSection;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrganizationServiceRateCommandHandler : IRequestHandler<OrganizationServiceRateCommand, OrganizationServiceRateCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationServices, int> _orgServices;
        private readonly IRepository<OrganizationServicesRate, int> _orgServiceRate;
        private readonly IRepository<Deadline, int> _deadline;


        public OrganizationServiceRateCommandHandler(IRepository<Organizations, int> organization, IRepository<OrganizationServices, int> orgServices, IRepository<OrganizationServicesRate, int> orgServiceRate, IRepository<Deadline, int> deadline)
        {
            _organization = organization;
            _orgServices = orgServices;
            _orgServiceRate = orgServiceRate;
            _deadline = deadline;
        }

        public async Task<OrganizationServiceRateCommandResult> Handle(OrganizationServiceRateCommand request, CancellationToken cancellationToken)
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
            return new OrganizationServiceRateCommandResult() { Id = id};
        }

        public int Add(OrganizationServiceRateCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");


            var service = _orgServices.Find(s => s.OrganizationId == model.OrganizationId && s.Id == model.ServiceId).FirstOrDefault();
            if (service == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            var rate = _orgServiceRate.Find(r => r.OrganizationId == model.OrganizationId && r.ApplicationNumber == model.ApplicationNumber).FirstOrDefault();
            if (rate != null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            OrganizationServicesRate addModel = new OrganizationServicesRate();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ServiceId = model.ServiceId;
            addModel.ApplicationNumber = model.ApplicationNumber;
            addModel.HasApplicationProblem = model.HasApplicationProblem;
            addModel.ApplicationProblemText = model.ApplicationProblemText;
            addModel.ApplicationProblemConfirmde = model.ApplicationProblemConfirmde;
            addModel.RecommendService = model.RecommendService;
            addModel.NotRecommendationComment = model.NotRecommendationComment;
            addModel.ServiceSatisfactive = model.ServiceSatisfactive;
            addModel.ServiceDissatisfactionReason = model.ServiceDissatisfactionReason;
            addModel.ServiceDissatisfactionConfirmed = model.ServiceDissatisfactionConfirmed;
            addModel.ServiceCommentType = model.ServiceCommentType;
            addModel.ServiceComment = model.ServiceComment;
            addModel.ServiceCommentConfirmed = model.ServiceCommentConfirmed;
            addModel.ServiceRate = model.ServiceRate;
            addModel.ExpertComment = model.ExpertComment;

            _orgServiceRate.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrganizationServiceRateCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");


            

            var rate = _orgServiceRate.Find(r => r.Id == model.Id).FirstOrDefault();
            if (rate == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);


            rate.OrganizationId = model.OrganizationId;
            rate.ServiceId = model.ServiceId;
            rate.ApplicationNumber = model.ApplicationNumber;
            rate.HasApplicationProblem = model.HasApplicationProblem;
            rate.ApplicationProblemText = model.ApplicationProblemText;
            rate.ApplicationProblemConfirmde = model.ApplicationProblemConfirmde;
            rate.RecommendService = model.RecommendService;
            rate.NotRecommendationComment = model.NotRecommendationComment;
            rate.ServiceSatisfactive = model.ServiceSatisfactive;
            rate.ServiceDissatisfactionReason = model.ServiceDissatisfactionReason;
            rate.ServiceDissatisfactionConfirmed = model.ServiceDissatisfactionConfirmed;
            rate.ServiceCommentType = model.ServiceCommentType;
            rate.ServiceComment = model.ServiceComment;
            rate.ServiceCommentConfirmed = model.ServiceCommentConfirmed;
            rate.ServiceRate = model.ServiceRate;
            rate.ExpertComment = model.ExpertComment;

            _orgServiceRate.Update(rate);

            return rate.Id;
        }

        public int Delete(OrganizationServiceRateCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            var rate = _orgServiceRate.Find(r => r.Id == model.Id).FirstOrDefault();
            if (rate == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            _orgServiceRate.Remove(rate);

            return rate.Id;
        }
    }
}
