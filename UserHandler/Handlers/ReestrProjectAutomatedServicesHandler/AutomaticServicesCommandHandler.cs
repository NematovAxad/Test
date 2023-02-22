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
using Domain;

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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new AutomaticServicesCommandResult() {Id = id, IsSuccess = true };
        }
        public int Add(AutomaticServicesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);



            var projectServices = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            var service = _services.Find(p => p.ParentId == model.ParentId && p.ServiceName == model.ServiceName).FirstOrDefault();
            if (service != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);


            AutomatedServices addModel = new AutomatedServices();


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER)))
            {
                addModel.ParentId = model.ParentId;
                addModel.ServiceName = model.ServiceName;
                addModel.FilePath = model.FilePath;
            }
            else 
            {
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            }



            _services.Add(addModel);

            return addModel.Id;
        }

        public int Update(AutomaticServicesCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var service = _services.Find(p => p.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            var projectServices = _projectServices.Find(p => p.Id == service.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER)))
            {
                service.ServiceName = model.ServiceName;

                if (!String.IsNullOrEmpty(model.FilePath))
                    service.FilePath = model.FilePath;
            }
            else 
            {
                throw ErrorStates.Error(UIErrors.UserPermissionsNotAllowed);
            }



            _services.Update(service);

            return service.Id;
        }
        public int Delete(AutomaticServicesCommand model)
        {
            var service = _services.Find(p => p.Id == model.Id).FirstOrDefault();
            if (service == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);
            _services.Remove(service);

            return service.Id;
        }
    }
}
