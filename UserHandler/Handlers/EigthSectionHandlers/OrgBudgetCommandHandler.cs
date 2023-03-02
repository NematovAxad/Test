using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SeventhSection;
using Domain.States;
using Domain;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.EighthSection;
using UserHandler.Results.EigthSectionResult;
using UserHandler.Commands.EigthSectionCommand;

namespace UserHandler.Handlers.EigthSectionHandlers
{
    public class OrgBudgetCommandHandler : IRequestHandler<OrgBudgetCommand, OrgBudgetCommandResult>
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationBudget, int> _orgBudget;

        public OrgBudgetCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationBudget, int> orgBudget)
        {
            _organization = organization;
            _deadline = deadline;
            _orgBudget = orgBudget;
        }

        public async Task<OrgBudgetCommandResult> Handle(OrgBudgetCommand request, CancellationToken cancellationToken)
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
            return new OrgBudgetCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(OrgBudgetCommand model)
        {

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var orgBudget = _orgBudget.Find(p => p.OrganizationId == model.OrganizationId && p.Year == deadline.Year).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgBudget != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());




            OrganizationBudget addModel = new OrganizationBudget();


            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                addModel.OrganizationId = model.OrganizationId;
                addModel.Year = deadline.Year;

                addModel.PersonalBudgetPlan1 = model.PersonalBudgetPlan1;
                addModel.PersonalBudgetFact1 = model.PersonalBudgetFact1;
                addModel.LocalInvestmentBudgetPlan1 = model.LocalInvestmentBudgetPlan1;
                addModel.LocalInvestmentBudgetFact1 = model.LocalInvestmentBudgetFact1;
                addModel.ForeignBudgetPlan1 = model.ForeignBudgetPlan1;
                addModel.ForeignBudgetFact1 = model.ForeignBudgetFact1;
                addModel.OtherBudgetPlan1 = model.OtherBudgetPlan1;
                addModel.OtherBudgetFact1 = model.OtherBudgetFact1;
                addModel.AllPlan1 = model.PersonalBudgetPlan1 + model.LocalInvestmentBudgetPlan1 + model.ForeignBudgetPlan1 + model.OtherBudgetPlan1;
                addModel.AllFact1 = model.PersonalBudgetFact1 + model.LocalInvestmentBudgetFact1 + model.ForeignBudgetFact1 + model.OtherBudgetFact1;

                addModel.PersonalBudgetPlan2 = model.PersonalBudgetPlan2;
                addModel.PersonalBudgetFact2 = model.PersonalBudgetFact2;
                addModel.LocalInvestmentBudgetPlan2 = model.LocalInvestmentBudgetPlan2;
                addModel.LocalInvestmentBudgetFact2 = model.LocalInvestmentBudgetFact2;
                addModel.ForeignBudgetPlan2 = model.ForeignBudgetPlan2;
                addModel.ForeignBudgetFact2 = model.ForeignBudgetFact2;
                addModel.OtherBudgetPlan2 = model.OtherBudgetPlan2;
                addModel.OtherBudgetFact2 = model.OtherBudgetFact2;
                addModel.AllPlan2 = model.PersonalBudgetPlan2 + model.LocalInvestmentBudgetPlan2 + model.ForeignBudgetPlan2 + model.OtherBudgetPlan2;
                addModel.AllFact2 = model.PersonalBudgetFact2 + model.LocalInvestmentBudgetFact2 + model.ForeignBudgetFact2 + model.OtherBudgetFact2;

                addModel.PersonalBudgetPlan3 = model.PersonalBudgetPlan3;
                addModel.PersonalBudgetFact3 = model.PersonalBudgetFact3;
                addModel.LocalInvestmentBudgetPlan3 = model.LocalInvestmentBudgetPlan3;
                addModel.LocalInvestmentBudgetFact3 = model.LocalInvestmentBudgetFact3;
                addModel.ForeignBudgetPlan3 = model.ForeignBudgetPlan3;
                addModel.ForeignBudgetFact3 = model.ForeignBudgetFact3;
                addModel.OtherBudgetPlan3 = model.OtherBudgetPlan3;
                addModel.OtherBudgetFact3 = model.OtherBudgetFact3;
                addModel.AllPlan3 = model.PersonalBudgetPlan3 + model.LocalInvestmentBudgetPlan3 + model.ForeignBudgetPlan3 + model.OtherBudgetPlan3;
                addModel.AllFact3 = model.PersonalBudgetFact3 + model.LocalInvestmentBudgetFact3 + model.ForeignBudgetFact3 + model.OtherBudgetFact3;

                addModel.PersonalBudgetPlan4 = model.PersonalBudgetPlan4;
                addModel.PersonalBudgetFact4 = model.PersonalBudgetFact4;
                addModel.LocalInvestmentBudgetPlan4 = model.LocalInvestmentBudgetPlan4;
                addModel.LocalInvestmentBudgetFact4 = model.LocalInvestmentBudgetFact4;
                addModel.ForeignBudgetPlan4 = model.ForeignBudgetPlan4;
                addModel.ForeignBudgetFact4 = model.ForeignBudgetFact4;
                addModel.OtherBudgetPlan4 = model.OtherBudgetPlan4;
                addModel.OtherBudgetFact4 = model.OtherBudgetFact4;
                addModel.AllPlan4 = model.PersonalBudgetPlan4 + model.LocalInvestmentBudgetPlan4 + model.ForeignBudgetPlan4 + model.OtherBudgetPlan4;
                addModel.AllFact4 = model.PersonalBudgetFact4 + model.LocalInvestmentBudgetFact4 + model.ForeignBudgetFact4 + model.OtherBudgetFact4;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _orgBudget.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrgBudgetCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var orgBudget = _orgBudget.Find(p => p.Id == model.Id).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgBudget == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());




            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                orgBudget.PersonalBudgetPlan1 = model.PersonalBudgetPlan1;
                orgBudget.PersonalBudgetFact1 = model.PersonalBudgetFact1;
                orgBudget.LocalInvestmentBudgetPlan1 = model.LocalInvestmentBudgetPlan1;
                orgBudget.LocalInvestmentBudgetFact1 = model.LocalInvestmentBudgetFact1;
                orgBudget.ForeignBudgetPlan1 = model.ForeignBudgetPlan1;
                orgBudget.ForeignBudgetFact1 = model.ForeignBudgetFact1;
                orgBudget.OtherBudgetPlan1 = model.OtherBudgetPlan1;
                orgBudget.OtherBudgetFact1 = model.OtherBudgetFact1;
                orgBudget.AllPlan1 = model.PersonalBudgetPlan1 + model.LocalInvestmentBudgetPlan1 + model.ForeignBudgetPlan1 + model.OtherBudgetPlan1;
                orgBudget.AllFact1 = model.PersonalBudgetFact1 + model.LocalInvestmentBudgetFact1 + model.ForeignBudgetFact1 + model.OtherBudgetFact1;

                orgBudget.PersonalBudgetPlan2 = model.PersonalBudgetPlan2;
                orgBudget.PersonalBudgetFact2 = model.PersonalBudgetFact2;
                orgBudget.LocalInvestmentBudgetPlan2 = model.LocalInvestmentBudgetPlan2;
                orgBudget.LocalInvestmentBudgetFact2 = model.LocalInvestmentBudgetFact2;
                orgBudget.ForeignBudgetPlan2 = model.ForeignBudgetPlan2;
                orgBudget.ForeignBudgetFact2 = model.ForeignBudgetFact2;
                orgBudget.OtherBudgetPlan2 = model.OtherBudgetPlan2;
                orgBudget.OtherBudgetFact2 = model.OtherBudgetFact2;
                orgBudget.AllPlan2 = model.PersonalBudgetPlan2 + model.LocalInvestmentBudgetPlan2 + model.ForeignBudgetPlan2 + model.OtherBudgetPlan2;
                orgBudget.AllFact2 = model.PersonalBudgetFact2 + model.LocalInvestmentBudgetFact2 + model.ForeignBudgetFact2 + model.OtherBudgetFact2;

                orgBudget.PersonalBudgetPlan3 = model.PersonalBudgetPlan3;
                orgBudget.PersonalBudgetFact3 = model.PersonalBudgetFact3;
                orgBudget.LocalInvestmentBudgetPlan3 = model.LocalInvestmentBudgetPlan3;
                orgBudget.LocalInvestmentBudgetFact3 = model.LocalInvestmentBudgetFact3;
                orgBudget.ForeignBudgetPlan3 = model.ForeignBudgetPlan3;
                orgBudget.ForeignBudgetFact3 = model.ForeignBudgetFact3;
                orgBudget.OtherBudgetPlan3 = model.OtherBudgetPlan3;
                orgBudget.OtherBudgetFact3 = model.OtherBudgetFact3;
                orgBudget.AllPlan3 = model.PersonalBudgetPlan3 + model.LocalInvestmentBudgetPlan3 + model.ForeignBudgetPlan3 + model.OtherBudgetPlan3;
                orgBudget.AllFact3 = model.PersonalBudgetFact3 + model.LocalInvestmentBudgetFact3 + model.ForeignBudgetFact3 + model.OtherBudgetFact3;

                orgBudget.PersonalBudgetPlan4 = model.PersonalBudgetPlan4;
                orgBudget.PersonalBudgetFact4 = model.PersonalBudgetFact4;
                orgBudget.LocalInvestmentBudgetPlan4 = model.LocalInvestmentBudgetPlan4;
                orgBudget.LocalInvestmentBudgetFact4 = model.LocalInvestmentBudgetFact4;
                orgBudget.ForeignBudgetPlan4 = model.ForeignBudgetPlan4;
                orgBudget.ForeignBudgetFact4 = model.ForeignBudgetFact4;
                orgBudget.OtherBudgetPlan4 = model.OtherBudgetPlan4;
                orgBudget.OtherBudgetFact4 = model.OtherBudgetFact4;
                orgBudget.AllPlan4 = model.PersonalBudgetPlan4 + model.LocalInvestmentBudgetPlan4 + model.ForeignBudgetPlan4 + model.OtherBudgetPlan4;
                orgBudget.AllFact4 = model.PersonalBudgetFact4 + model.LocalInvestmentBudgetFact4 + model.ForeignBudgetFact4 + model.OtherBudgetFact4;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }



            _orgBudget.Update(orgBudget);

            return orgBudget.Id;
        }

        public int Delete(OrgBudgetCommand model)
        {
            var orgBudget = _orgBudget.Find(p => p.Id == model.Id).FirstOrDefault();
            if (orgBudget == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _orgBudget.Remove(orgBudget);
            return orgBudget.Id;
        }
    }
}
