using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
using Domain.Models;
using Domain.Models.FirstSection;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using EntityRepository;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace AdminHandler.Handlers.SecondOptionHandlers
{
    public class WebSiteRequirementsCommandHandler : IRequestHandler<WebSiteRequirementsCommand, WebSiteRequirementsCommandResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<WebSiteRequirements, int> _websiteRequirements;
        private readonly IRepository<Deadline, int> _deadline;
        private IDataContext _db;

        public WebSiteRequirementsCommandHandler(IRepository<Organizations, int> organizations, IRepository<WebSiteRequirements, int> websiteRequirements, IDataContext db, IRepository<Deadline, int> deadline)
        {
            _organizations = organizations;
            _websiteRequirements = websiteRequirements;
            _db = db;
            _deadline = deadline;
        }

        public async Task<WebSiteRequirementsCommandResult> Handle(WebSiteRequirementsCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new WebSiteRequirementsCommandResult() { IsSuccess = true };
        }
        public void Add(WebSiteRequirementsCommand model)
        {
            var orgRequirements = _websiteRequirements.Find(r => r.OrganizationId == model.Requirements[0].OrganizationId).ToList();
            if (orgRequirements.Count > 0)
                throw ErrorStates.Error(UIErrors.DataWithThisParametersIsExist);
            
            var org = _organizations.Find(o => o.Id == model.Requirements[0].OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.Requirements[0].Id.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if(model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) ||model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());


            if (model.Requirements.Any())
            {
                List<WebSiteRequirements> addList = new List<WebSiteRequirements>();

                
                try
                {
                    foreach (var r in model.Requirements)
                    {
                        WebSiteRequirements requirement = new WebSiteRequirements();
                        requirement.OrganizationId = r.OrganizationId;
                        requirement.Name = r.Name;
                        requirement.Number = r.Number;

                        if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || (model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                        {
                            requirement.SiteLink1 = r.SiteLink1;
                            requirement.SiteLink2 = r.SiteLink2;
                            requirement.SiteLink3 = r.SiteLink3;

                            requirement.ScreenLink1 = r.ScreenLink1;
                            requirement.ScreenLink2 = r.ScreenLink2;
                            requirement.ScreenLink3 = r.ScreenLink3;

                        }
                        if (model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                        {
                            requirement.Comment = r.Comment;
                            requirement.RequirementStatus = r.RequirementStatus;
                        };
                        
                        if(!String.IsNullOrEmpty(model.UserPinfl))
                            requirement.UserPinfl = model.UserPinfl;
                        
                        if(r.LastUpdate!=null)
                            requirement.LastUpdate = r.LastUpdate;

                        addList.Add(requirement);
                    }
                }
                catch(Exception ex)
                {
                    throw ErrorStates.NotFound("cannot parse list");
                }
                
                
                

                _websiteRequirements.AddRange(addList);
            }
            else
            {
                throw ErrorStates.NotAllowed("empty list");
            }
            
        }
        public void Update(WebSiteRequirementsCommand model)
        {
            var org = _organizations.Find(o => o.Id == model.Requirements[0].OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.Requirements[0].Id.ToString());

            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");

            if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                if (deadline.OperatorDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.OperatorDeadlineDate.ToString());

            if ((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                if (deadline.SecondSectionDeadlineDate < DateTime.Now)
                    throw ErrorStates.NotAllowed(deadline.SecondSectionDeadlineDate.ToString());

            var orgRequirements = _websiteRequirements.Find(r => r.OrganizationId == model.Requirements[0].OrganizationId).ToList();

            if (model.Requirements.Any())
            {
                
                foreach (var r in model.Requirements)
                {


                    var requirement = orgRequirements.Find(t => t.Id == r.Id);

                    if (model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) || (model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE)))
                    {
                        requirement.SiteLink1 = r.SiteLink1;
                        requirement.SiteLink2 = r.SiteLink2;
                        requirement.SiteLink3 = r.SiteLink3;

                        requirement.ScreenLink1 = r.ScreenLink1;
                        requirement.ScreenLink2 = r.ScreenLink2;
                        requirement.ScreenLink3 = r.ScreenLink3;

                    }
                    if (model.UserPermissions.Any(p => p == Permissions.OPERATOR_RIGHTS))
                    {
                        requirement.Comment = r.Comment;
                        requirement.RequirementStatus = r.RequirementStatus;
                    };
                    if (!String.IsNullOrEmpty(model.UserPinfl))
                        requirement.UserPinfl = model.UserPinfl;
                    
                    if(r.LastUpdate!=null)
                        requirement.LastUpdate = r.LastUpdate;
                }   

                _db.Context.Set<WebSiteRequirements>().UpdateRange(orgRequirements);
                _db.Context.SaveChanges();
            }
            else
            {
                throw ErrorStates.NotAllowed("empty list");
            }
        }
        public void Delete(WebSiteRequirementsCommand model)
        {
            
        }
    }
}
