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
using Domain;
using Domain.Models.FirstSection;
using Domain.Models.FifthSection.ReestrModels;

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
            int id = 0;
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: id = Add(request); break;
                case Domain.Enums.EventType.Update: id = Update(request); break;
                case Domain.Enums.EventType.Delete: id = Delete(request); break;
            }
            return new AutomaticFunctionsCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(AutomaticFunctionsCommand model)
        {
            int id = 0;

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            



            var projectFunctions = _projectServices.Find(p => p.Id == model.ParentId && p.ProjectFunctionsExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectFunctions == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            var functions = _functions.Find(p => p.ParentId == model.ParentId && p.FunctionName == model.FunctionName).FirstOrDefault();
            if (functions != null)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);


            


            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                AutomatedFunctions addModel = new AutomatedFunctions();
                addModel.ParentId = model.ParentId;
                addModel.FunctionName = model.FunctionName;

                _functions.Add(addModel);
                id = addModel.Id;
            }
            



            

            return id;
        }

        public int Update(AutomaticFunctionsCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            

            var function = _functions.Find(p => p.Id == model.Id).FirstOrDefault();
            if (function == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            var projectFunctions = _projectServices.Find(p => p.Id == function.ParentId && p.ProjectFunctionsExist == true).Include(mbox => mbox.Organizations).FirstOrDefault();
            if (projectFunctions == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);

            


            if ((model.UserOrgId == projectFunctions.Organizations.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
            {
                if (deadline.FifthSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                if (!String.IsNullOrEmpty(model.FilePath))
                    function.FilePath = model.FilePath;
            }
            if(model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.Error(UIErrors.DeadlineExpired);

                function.FunctionName = model.FunctionName;
            }

            _functions.Update(function);

            return function.Id;
        }
        public int Delete(AutomaticFunctionsCommand model)
        {
            var function = _functions.Find(p => p.Id == model.Id).FirstOrDefault();
            if (function == null)
                throw ErrorStates.Error(UIErrors.DataToChangeNotFound);
            _functions.Remove(function);

            return function.Id;
        }
    }
}
