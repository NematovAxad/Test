using Domain.Models.FirstSection;
using Domain.States;
using Domain;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ThirdSection;
using UserHandler.Results.ReestrPassportResult;
using UserHandler.Results.ThirdSection;
using JohaRepository;
using Domain.Models.ThirdSection;
using System.Linq;
using Domain.Models;
using Domain.Permission;
using MainInfrastructures.Migrations;

namespace UserHandler.Handlers.ThirdSection
{
    public class OrganizationServicesCommandHandler : IRequestHandler<OrganizationServicesCommand, OrganizationServicesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<OrganizationServices, int> _orgServices;
        private readonly IRepository<Deadline, int> _deadline;

        public OrganizationServicesCommandHandler(IRepository<Organizations, int> organization, IRepository<OrganizationServices, int> orgServices, IRepository<Deadline, int> deadline)
        {
            _organization = organization;
            _orgServices = orgServices;
            _deadline = deadline;
        }

        public async Task<OrganizationServicesCommandResult> Handle(OrganizationServicesCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add:
                    id = Add(request);
                    break;
                case Domain.Enums.EventType.Delete:
                    id = Delete(request);
                    break;
            }
            return new OrganizationServicesCommandResult() { Id = id};
        }

        public int Add(OrganizationServicesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");


            var service = _orgServices.Find(s => s.OrganizationId == model.OrganizationId && s.ServiceNameRu == model.ServiceNameRu).FirstOrDefault();
            if (service != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");
            
            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            OrganizationServices addModel = new OrganizationServices();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ServiceNameRu = model.ServiceNameRu;
            addModel.ServiceNameUz = model.ServiceNameUz;

            _orgServices.Add(addModel);

            return addModel.Id;
        }

        public int Delete(OrganizationServicesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("deadline");

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                throw ErrorStates.NotAllowed("permission");

            var service = _orgServices.Find(s => s.OrganizationId == model.OrganizationId && s.ServiceNameRu == model.ServiceNameRu).FirstOrDefault();
            if (service != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);

            if (deadline.ThirdSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            _orgServices.Remove(service);

            return service.Id;
        }
    }
}
