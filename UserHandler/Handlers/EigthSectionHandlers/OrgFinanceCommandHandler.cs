using Domain.Models.SecondSection;
using Domain.Models;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SeventhSection;
using UserHandler.Results.ReestrPassportResult;
using Domain.States;
using Domain;
using MainInfrastructures.Migrations;
using UserHandler.Commands.ReestrPassportCommands;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Domain.Permission;
using Domain.Models.FirstSection;
using Domain.Models.EighthSection;
using UserHandler.Results.EigthSectionResult;
using UserHandler.Commands.EigthSectionCommand;

namespace UserHandler.Handlers.EigthSectionHandlers
{
    public class OrgFinanceCommandHandler : IRequestHandler<OrgFinanceCommand, OrgFinanceCommandResult>
    {

        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<OrganizationFinance, int> _orgFinance;

        public OrgFinanceCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<OrganizationFinance, int> orgFinance)
        {
            _organization = organization;
            _deadline = deadline;
            _orgFinance = orgFinance;
        }

        public async Task<OrgFinanceCommandResult> Handle(OrgFinanceCommand request, CancellationToken cancellationToken)
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
            return new OrgFinanceCommandResult() { Id = id, IsSuccess = true };
        }

        public int Add(OrgFinanceCommand model)
        {

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());


            var orgFinance = _orgFinance.Find(p => p.OrganizationId == model.OrganizationId && p.Year == deadline.Year).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgFinance != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());




            OrganizationFinance addModel = new OrganizationFinance();


            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {
                addModel.OrganizationId = model.OrganizationId;
                addModel.Year = deadline.Year;

                addModel.Plan11 = model.Plan11;
                addModel.Fact11 = model.Fact11;
                addModel.Plan21 = model.Plan21;
                addModel.Fact21 = model.Fact21;
                addModel.Plan31 = model.Plan31;
                addModel.Fact31 = model.Fact31;
                addModel.Plan41 = model.Plan41;
                addModel.Fact41 = model.Fact41;
                addModel.Plan51 = model.Plan51;
                addModel.Fact51 = model.Fact51;
                addModel.Plan61 = model.Plan61;
                addModel.Fact61 = model.Fact61;
                addModel.Plan71 = model.Plan71;
                addModel.Fact71 = model.Fact71;
                addModel.Plan81 = model.Plan81;
                addModel.Fact81 = model.Fact81;
                addModel.AllPlan1 = model.Plan11 + model.Plan21 + model.Plan31 + model.Plan41 + model.Plan51 + model.Plan61 + model.Plan71 + model.Plan81;
                addModel.AllFact1 = model.Fact11 + model.Fact21 + model.Fact31 + model.Fact41 + model.Fact51 + model.Fact61 + model.Fact71 + model.Fact81;

                addModel.Plan12 = model.Plan12;
                addModel.Fact12 = model.Fact12;
                addModel.Plan22 = model.Plan22;
                addModel.Fact22 = model.Fact22;
                addModel.Plan32 = model.Plan32;
                addModel.Fact32 = model.Fact32;
                addModel.Plan42 = model.Plan42;
                addModel.Fact42 = model.Fact42;
                addModel.Plan52 = model.Plan52;
                addModel.Fact52 = model.Fact52;
                addModel.Plan62 = model.Plan62;
                addModel.Fact62 = model.Fact62;
                addModel.Plan72 = model.Plan72;
                addModel.Fact72 = model.Fact72;
                addModel.Plan82 = model.Plan82;
                addModel.Fact82 = model.Fact82;
                addModel.AllPlan2 = model.Plan12 + model.Plan22 + model.Plan32 + model.Plan42 + model.Plan52 + model.Plan62 + model.Plan72 + model.Plan82;
                addModel.AllFact2 = model.Fact12 + model.Fact22 + model.Fact32 + model.Fact42 + model.Fact52 + model.Fact62 + model.Fact72 + model.Fact82;

                addModel.Plan13 = model.Plan13;
                addModel.Fact13 = model.Fact13;
                addModel.Plan23 = model.Plan23;
                addModel.Fact23 = model.Fact23;
                addModel.Plan33 = model.Plan33;
                addModel.Fact33 = model.Fact33;
                addModel.Plan43 = model.Plan43;
                addModel.Fact43 = model.Fact43;
                addModel.Plan53 = model.Plan53;
                addModel.Fact53 = model.Fact53;
                addModel.Plan63 = model.Plan63;
                addModel.Fact63 = model.Fact63;
                addModel.Plan73 = model.Plan73;
                addModel.Fact73 = model.Fact73;
                addModel.Plan83 = model.Plan83;
                addModel.Fact83 = model.Fact83;
                addModel.AllPlan3 = model.Plan13 + model.Plan23 + model.Plan33 + model.Plan43 + model.Plan53 + model.Plan63 + model.Plan73 + model.Plan83;
                addModel.AllFact3 = model.Fact13 + model.Fact23 + model.Fact33 + model.Fact43 + model.Fact53 + model.Fact63 + model.Fact73 + model.Fact83;

                addModel.Plan14 = model.Plan14;
                addModel.Fact14 = model.Fact14;
                addModel.Plan24 = model.Plan24;
                addModel.Fact24 = model.Fact24;
                addModel.Plan34 = model.Plan34;
                addModel.Fact34 = model.Fact34;
                addModel.Plan44 = model.Plan44;
                addModel.Fact44 = model.Fact44;
                addModel.Plan54 = model.Plan54;
                addModel.Fact54 = model.Fact54;
                addModel.Plan64 = model.Plan64;
                addModel.Fact64 = model.Fact64;
                addModel.Plan74 = model.Plan74;
                addModel.Fact74 = model.Fact74;
                addModel.Plan84 = model.Plan84;
                addModel.Fact84 = model.Fact84;
                addModel.AllPlan4 = model.Plan14 + model.Plan24 + model.Plan34 + model.Plan44 + model.Plan54 + model.Plan64 + model.Plan74 + model.Plan84;
                addModel.AllFact4 = model.Fact14 + model.Fact24 + model.Fact34 + model.Fact44 + model.Fact54 + model.Fact64 + model.Fact74 + model.Fact84;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }

            addModel.UserPinfl = model.UserPinfl;
            addModel.LastUpdate = DateTime.Now;

            _orgFinance.Add(addModel);

            return addModel.Id;
        }

        public int Update(OrgFinanceCommand model)
        {
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.Error(UIErrors.DeadlineNotFound);

            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());

            var orgFinance = _orgFinance.Find(p => p.Id == model.Id).Include(mbox => mbox.Organization).FirstOrDefault();
            if (orgFinance == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());




            if (model.UserOrgId == org.UserServiceId && model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE) || model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER || p == Permissions.OPERATOR_RIGHTS))
            {


                orgFinance.Plan11 = model.Plan11;
                orgFinance.Fact11 = model.Fact11;
                orgFinance.Plan21 = model.Plan21;
                orgFinance.Fact21 = model.Fact21;
                orgFinance.Plan31 = model.Plan31;
                orgFinance.Fact31 = model.Fact31;
                orgFinance.Plan41 = model.Plan41;
                orgFinance.Fact41 = model.Fact41;
                orgFinance.Plan51 = model.Plan51;
                orgFinance.Fact51 = model.Fact51;
                orgFinance.Plan61 = model.Plan61;
                orgFinance.Fact61 = model.Fact61;
                orgFinance.Plan71 = model.Plan71;
                orgFinance.Fact71 = model.Fact71;
                orgFinance.Plan81 = model.Plan81;
                orgFinance.Fact81 = model.Fact81;
                orgFinance.AllPlan1 = model.Plan11 + model.Plan21 + model.Plan31 + model.Plan41 + model.Plan51 + model.Plan61 + model.Plan71 + model.Plan81;
                orgFinance.AllFact1 = model.Fact11 + model.Fact21 + model.Fact31 + model.Fact41 + model.Fact51 + model.Fact61 + model.Fact71 + model.Fact81;

                orgFinance.Plan12 = model.Plan12;
                orgFinance.Fact12 = model.Fact12;
                orgFinance.Plan22 = model.Plan22;
                orgFinance.Fact22 = model.Fact22;
                orgFinance.Plan32 = model.Plan32;
                orgFinance.Fact32 = model.Fact32;
                orgFinance.Plan42 = model.Plan42;
                orgFinance.Fact42 = model.Fact42;
                orgFinance.Plan52 = model.Plan52;
                orgFinance.Fact52 = model.Fact52;
                orgFinance.Plan62 = model.Plan62;
                orgFinance.Fact62 = model.Fact62;
                orgFinance.Plan72 = model.Plan72;
                orgFinance.Fact72 = model.Fact72;
                orgFinance.Plan82 = model.Plan82;
                orgFinance.Fact82 = model.Fact82;
                orgFinance.AllPlan2 = model.Plan12 + model.Plan22 + model.Plan32 + model.Plan42 + model.Plan52 + model.Plan62 + model.Plan72 + model.Plan82;
                orgFinance.AllFact2 = model.Fact12 + model.Fact22 + model.Fact32 + model.Fact42 + model.Fact52 + model.Fact62 + model.Fact72 + model.Fact82;

                orgFinance.Plan13 = model.Plan13;
                orgFinance.Fact13 = model.Fact13;
                orgFinance.Plan23 = model.Plan23;
                orgFinance.Fact23 = model.Fact23;
                orgFinance.Plan33 = model.Plan33;
                orgFinance.Fact33 = model.Fact33;
                orgFinance.Plan43 = model.Plan43;
                orgFinance.Fact43 = model.Fact43;
                orgFinance.Plan53 = model.Plan53;
                orgFinance.Fact53 = model.Fact53;
                orgFinance.Plan63 = model.Plan63;
                orgFinance.Fact63 = model.Fact63;
                orgFinance.Plan73 = model.Plan73;
                orgFinance.Fact73 = model.Fact73;
                orgFinance.Plan83 = model.Plan83;
                orgFinance.Fact83 = model.Fact83;
                orgFinance.AllPlan3 = model.Plan13 + model.Plan23 + model.Plan33 + model.Plan43 + model.Plan53 + model.Plan63 + model.Plan73 + model.Plan83;
                orgFinance.AllFact3 = model.Fact13 + model.Fact23 + model.Fact33 + model.Fact43 + model.Fact53 + model.Fact63 + model.Fact73 + model.Fact83;

                orgFinance.Plan14 = model.Plan14;
                orgFinance.Fact14 = model.Fact14;
                orgFinance.Plan24 = model.Plan24;
                orgFinance.Fact24 = model.Fact24;
                orgFinance.Plan34 = model.Plan34;
                orgFinance.Fact34 = model.Fact34;
                orgFinance.Plan44 = model.Plan44;
                orgFinance.Fact44 = model.Fact44;
                orgFinance.Plan54 = model.Plan54;
                orgFinance.Fact54 = model.Fact54;
                orgFinance.Plan64 = model.Plan64;
                orgFinance.Fact64 = model.Fact64;
                orgFinance.Plan74 = model.Plan74;
                orgFinance.Fact74 = model.Fact74;
                orgFinance.Plan84 = model.Plan84;
                orgFinance.Fact84 = model.Fact84;
                orgFinance.AllPlan4 = model.Plan14 + model.Plan24 + model.Plan34 + model.Plan44 + model.Plan54 + model.Plan64 + model.Plan74 + model.Plan84;
                orgFinance.AllFact4 = model.Fact14 + model.Fact24 + model.Fact34 + model.Fact44 + model.Fact54 + model.Fact64 + model.Fact74 + model.Fact84;
            }
            else { throw ErrorStates.NotAllowed(model.UserPermissions.ToString()); }

            orgFinance.UserPinfl = model.UserPinfl;
            orgFinance.LastUpdate = DateTime.Now;

            _orgFinance.Update(orgFinance);

            return orgFinance.Id;
        }

        public int Delete(OrgFinanceCommand model)
        {
            var connection = _orgFinance.Find(p => p.Id == model.Id).FirstOrDefault();
            if (connection == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _orgFinance.Remove(connection);
            return connection.Id;
        }
    }
}
