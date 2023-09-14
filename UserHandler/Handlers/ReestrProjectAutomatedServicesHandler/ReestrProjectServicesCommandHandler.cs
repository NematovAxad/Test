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
using MainInfrastructures.Interfaces;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class ReestrProjectServicesCommandHandler : IRequestHandler<ReestrProjectServicesCommand, ReestrProjectServicesCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _projectServices;
        private readonly IRepository<AutomatedFunctions, int> _functions;
        private readonly IRepository<AutomatedServices, int> _services;
        private readonly IReesterService _reesterService;

        public ReestrProjectServicesCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<ReestrProjectAutomatedServices, int> projectServices, IRepository<AutomatedFunctions, int> functions, IRepository<AutomatedServices, int> services, IReesterService reesterService)
        {
            _organization = organization;
            _deadline = deadline;
            _projectServices = projectServices;
            _functions = functions;
            _services = services;
            _reesterService = reesterService;
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
            if (model.AllItems < model.ExceptedItems)
                throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);
            
            int id = 0;

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();

            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            

            var projectServices = _projectServices.Find(p => p.OrganizationId == model.OrganizationId && p.ReestrProjectId == model.ReestrProjectId).Include(mbox=>mbox.Organizations).FirstOrDefault();
            if (projectServices != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);


            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                ReestrProjectAutomatedServices addModel = new ReestrProjectAutomatedServices();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                addModel.ProjectServiceExist = model.ProjectServiceExist;
               

                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                addModel.LastUpdate = DateTime.Now;
                addModel.UserPinfl = model.UserPinfl;
                
                _projectServices.Add(addModel);
                id = addModel.Id;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                ReestrProjectAutomatedServices addModel = new ReestrProjectAutomatedServices();

                addModel.OrganizationId = model.OrganizationId;
                addModel.ReestrProjectId = model.ReestrProjectId;
                
                addModel.ProjectFunctionsExist = model.ProjectFunctionsExist;

                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.ExpertComment))
                    addModel.ExpertComment = model.ExpertComment;

                if (model.AllItems >= 0)
                    addModel.AllItems = model.AllItems;

                if (model.ExceptedItems >= 0)
                    addModel.ExceptedItems = model.ExceptedItems;

                addModel.LastUpdate = DateTime.Now;
                addModel.UserPinfl = model.UserPinfl;
                
                _projectServices.Add(addModel);
                id = addModel.Id;
            }


            _reesterService.RecordUpdateTime(model.ReestrProjectId);

            return id;
        }

        public int Update(ReestrProjectServicesCommand model)
        {
            if (model.AllItems < model.ExceptedItems)
                throw ErrorStates.Error(UIErrors.EnoughDataNotProvided);
            
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.Error(UIErrors.OrganizationNotFound);
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);


            

            var projectServices = _projectServices.Find(p => p.Id == model.Id).Include(mbox => mbox.Organizations).Include(mbox => mbox.AutomatedServices).Include(mbox=>mbox.AutomatedFunctions).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            

            if ((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                projectServices.ProjectServiceExist = model.ProjectServiceExist;
            }

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                projectServices.ProjectFunctionsExist = model.ProjectFunctionsExist;
       
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

            projectServices.LastUpdate = DateTime.Now;
            projectServices.UserPinfl = model.UserPinfl;

            _projectServices.Update(projectServices);

            _reesterService.RecordUpdateTime(projectServices.ReestrProjectId);
            
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
