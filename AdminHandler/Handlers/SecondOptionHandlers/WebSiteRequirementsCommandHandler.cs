using AdminHandler.Commands.SecondOptionCommands;
using AdminHandler.Results.SecondOptionResults;
using Domain;
using Domain.Models;
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
        private IDataContext _db;

        public WebSiteRequirementsCommandHandler(IRepository<Organizations, int> organizations, IRepository<WebSiteRequirements, int> websiteRequirements, IDataContext db)
        {
            _organizations = organizations;
            _websiteRequirements = websiteRequirements;
            _db = db;
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
            if(model.Requirements.Count()>0)
            {
                List<WebSiteRequirements> addList = new List<WebSiteRequirements>();

                var org = _organizations.Find(o => o.Id == model.Requirements[0].OrganizationId).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Requirements[0].Id.ToString());
                try
                {
                    foreach (var r in model.Requirements)
                    {
                        WebSiteRequirements requirement = new WebSiteRequirements
                        {
                            OrganizationId = r.OrganizationId,
                            Name = r.Name,
                            Number = r.Number,
                            SiteLink = r.SiteLink,
                            Comment = r.Comment,
                            RequirementStatus = r.RequirementStatus,
                        };
                        if(!String.IsNullOrEmpty(r.Screenshot))
                        {
                            var filePath = FileState.AddFile("screens", r.Screenshot);
                            requirement.ScreenLink = filePath;
                        }
                        addList.Add(requirement);
                    }
                }
                catch(Exception ex)
                {
                    throw ErrorStates.NotFound("cannot parse list");
                }
                
                
                if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                    throw ErrorStates.NotAllowed("permission");

                _websiteRequirements.AddRange(addList);
            }
            else
            {
                throw ErrorStates.NotAllowed("empty list");
            }
            
        }
        public void Update(WebSiteRequirementsCommand model)
        {
            if (model.Requirements.Count() > 0)
            {
                List<WebSiteRequirements> updateList = new List<WebSiteRequirements>();

                var org = _organizations.Find(o => o.Id == model.Requirements[0].Id).FirstOrDefault();
                if (org == null)
                    throw ErrorStates.NotFound(model.Requirements[0].Id.ToString());
                foreach (var r in model.Requirements)
                {
                    WebSiteRequirements requirement = new WebSiteRequirements
                    {
                        Id = r.Id,
                        OrganizationId = r.OrganizationId,
                        Name = r.Name,
                        Number = r.Number,
                        SiteLink = r.SiteLink,
                        Comment = r.Comment,
                        RequirementStatus = r.RequirementStatus,
                    };
                    if (!String.IsNullOrEmpty(r.Screenshot))
                    {
                        var filePath = FileState.AddFile("screens", r.Screenshot);
                        requirement.ScreenLink = filePath;
                    }
                    updateList.Add(requirement);
                }

                if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                    throw ErrorStates.NotAllowed("permission");

                _db.Context.UpdateRange(updateList);
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
