using Domain.Models.SecondSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.ReestrProjectAutomatedServicesCommand;
using UserHandler.Results.ReestrProjectAutomatedServicesResult;
using Domain.States;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Domain.Permission;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class AutomaticServicesCommandHandler : IRequestHandler<AutomaticServicesCommand, AutomaticServicesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<AutomatedServices, int> _services;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _projectServices;

        public AutomaticServicesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<AutomatedServices, int> services, IRepository<ReestrProjectAutomatedServices, int> projectServices)
        {
            _organization = organization;
            _deadline = deadline;
            _services = services;
            _projectServices = projectServices;
        }

        public async Task<AutomaticServicesCommandResult> Handle(AutomaticServicesCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new AutomaticServicesCommandResult() { IsSuccess = true };
        }
        public void Add(AutomaticServicesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectServices = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var service = _services.Find(p => p.ParentId == model.ParentId && p.ServiceName == model.ServiceName).FirstOrDefault();
            if (service != null)
                throw ErrorStates.NotAllowed(model.ServiceName.ToString());


            AutomatedServices addModel = new AutomatedServices();


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER)))
            {
                addModel.ParentId = model.ParentId;
                addModel.ServiceName = model.ServiceName;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _services.Add(addModel);
        }

        public void Update(AutomaticServicesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectServices = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var service = _services.Find(p => p.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER)))
            {
                service.ServiceName = model.ServiceName;

                if (!String.IsNullOrEmpty(model.FilePath))
                    service.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _services.Update(service);
        }
        public void Delete(AutomaticServicesCommand model)
        {
            var service = _services.Find(p => p.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _services.Remove(service);
        }
    }
}
