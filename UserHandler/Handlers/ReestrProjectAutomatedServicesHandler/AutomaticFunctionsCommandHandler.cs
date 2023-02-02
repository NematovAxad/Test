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
using UserHandler.Results.ReestrProjectIdentityResult;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Domain.States;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using UserHandler.Commands.ReestrProjectIdentityCommand;

namespace UserHandler.Handlers.ReestrProjectAutomatedServicesHandler
{
    public class AutomaticFunctionsCommandHandler : IRequestHandler<AutomaticFunctionsCommand, AutomaticFunctionsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<AutomatedFunctions, int> _functions;
        private readonly IRepository<ReestrProjectAutomatedServices, int> _projectServices;

        public AutomaticFunctionsCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<AutomatedFunctions, int> functions, IRepository<ReestrProjectAutomatedServices, int> projectServices)
        {
            _organization = organization;
            _deadline = deadline;
            _functions = functions;
            _projectServices = projectServices;
        }

        public async Task<AutomaticFunctionsCommandResult> Handle(AutomaticFunctionsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new AutomaticFunctionsCommandResult() { IsSuccess = true };
        }

        public void Add(AutomaticFunctionsCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());



            var projectServices = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var functions = _functions.Find(p => p.ParentId == model.ParentId && p.FunctionName == model.FunctionName).FirstOrDefault();
            if (functions != null)
                throw ErrorStates.NotAllowed(model.FunctionName.ToString());


            AutomatedFunctions addModel = new AutomatedFunctions();


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                addModel.ParentId = model.ParentId;
                addModel.FunctionName = model.FunctionName;
                addModel.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _functions.Add(addModel);
        }

        public void Update(AutomaticFunctionsCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());


            var projectServices = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectServiceExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectServices == null)
                throw ErrorStates.NotFound(model.ParentId.ToString());

            var function = _functions.Find(p => p.Id == model.Id).FirstOrDefault();
            if (function == null)
                throw ErrorStates.NotAllowed(model.Id.ToString());


            if (((model.UserOrgId == projectServices.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))) || (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS)))
            {
                function.FunctionName = model.FunctionName;

                if (!String.IsNullOrEmpty(model.FilePath))
                    function.FilePath = model.FilePath;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _functions.Update(function);
        }
        public void Delete(AutomaticFunctionsCommand model)
        {
            var function = _functions.Find(p => p.Id == model.Id).FirstOrDefault();
            if (function == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _functions.Remove(function);
        }
    }
}
