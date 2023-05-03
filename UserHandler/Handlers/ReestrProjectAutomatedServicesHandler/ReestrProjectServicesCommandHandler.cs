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
using UserHandler.Results.ReestrProjectIdentityResult;
using Domain.States;
using System.Linq;
using Domain.Permission;
using Microsoft.EntityFrameworkCore;
using Domain;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class ReestrProjectServicesCommandHandler : IRequestHandler<ReestrProjectServicesCommand, ReestrProjectServicesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _projectServices;
        private readonly IRepository<AutomatedFunctions, int> _functions;
        private readonly IRepository<AutomatedServices, int> _services;

        public ReestrProjectServicesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectAutomatedServices, int> projectServices, IRepository<AutomatedFunctions, int> functions, IRepository<AutomatedServices, int> services)
        {
            _organization = organization;
            _deadline = deadline;
            _projectServices = projectServices;
            _functions = functions;
            _services = services;
        }

        public async Task<ReestrProjectServicesCommandResult> Handle(ReestrProjectServicesCommand request, CancellationToken cancellationToken)
        {
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new ReestrProjectServicesCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(ReestrProjectServicesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var projectServices = _projectServices.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).FirstOrDefault();
            if (projectServices != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);
            ReestrProjectAutomatedServices addModel = new ReestrProjectAutomatedServices();

            addModel.OrganizationId = model.OrganizationId;
            addModel.ReestrProjectId = model.ReestrProjectId;
            addModel.ProjectServiceExist = model.ProjectServiceExist;
            addModel.ProjectFunctionsExist = model.ProjectFunctionsExist;



            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;
            }


            _projectServices.Add(addModel);

            return addModel.Id;
        }

        public int Update(ReestrProjectServicesCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);


            if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                throw ErrorStates.Error(UIErrors.DeadlineExpired);

            var projectServices = _projectServices.Find(p => p.Id == model.Id).Include(mbox => mbox.AutomatedServices).Include(mbox=>mbox.AutomatedFunctions).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            projectServices.ProjectFunctionsExist = model.ProjectFunctionsExist;
            projectServices.ProjectServiceExist = model.ProjectServiceExist;

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (!String.IsNullOrEmpty(model.ExpertComment))
                    projectServices.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    projectServices.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    if(model.ExceptedItems <= model.AllItems)
                    {
                        projectServices.ExceptedItems = model.ExceptedItems;
                    }
                    else { throw ErrorStates.Error(UIErrors.EnoughDataNotProvided); }  
            }

            if (model.ProjectServiceExist == false)
            {
                _services.RemoveRange(projectServices.AutomatedServices);

                projectServices.ExpertComment = String.Empty;
                projectServices.AllItems = 0;
                projectServices.ExceptedItems = 0;
            }
            if (model.ProjectFunctionsExist == false)
            {
                _functions.RemoveRange(projectServices.AutomatedFunctions);

                projectServices.ExpertComment = String.Empty;
                projectServices.AllItems = 0;
                projectServices.ExceptedItems = 0;
            }

            _projectServices.Update(projectServices);

            return projectServices.Id;
        }

        public int Delete(ReestrProjectServicesCommand model)
        {
            var projectServices = _projectServices.Find(p => p.Id == model.Id).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);
            _projectServices.Remove(projectServices);

            return projectServices.Id;
        }
    }
}
