using Domain;
using Domain.Models;
using Domain.Models.Ranking;
using Domain.Models.SecondSection;
using Domain.Permission;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UserHandler.Commands.SecondSectionCommand;
using UserHandler.Results.SecondSectionCommandResult;

namespace UserHandler.Handlers.SecondSectionHandler
{
    public class HelplineInfoCommandHandler : IRequestHandler<HelplineInfoCommand, HelplineInfoCommandResult>
    {
        private readonly IRepository<Organizations, int> _organization;
        private readonly IRepository<Deadline, int> _deadline;
        private readonly IRepository<Field, int> _field;
        private readonly IRepository<HelplineInfo, int> _helplineInfo;

        public HelplineInfoCommandHandler(IRepository<Organizations, int> organization, IRepository<Deadline, int> deadline, IRepository<Field, int> field, IRepository<HelplineInfo, int> helplineInfo)
        {
            _organization = organization;
            _deadline = deadline;
            _field = field;
            _helplineInfo = helplineInfo;
        }

        public async Task<HelplineInfoCommandResult> Handle(HelplineInfoCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new HelplineInfoCommandResult() { IsSuccess = true };
        }
        public void Add(HelplineInfoCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            var helplineInfo = _helplineInfo.Find(h => h.OrganizationId == model.OrganizationId).FirstOrDefault();
            if (helplineInfo != null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());

            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());

            HelplineInfo addModel = new HelplineInfo()
            {
                OrganizationId = model.OrganizationId,
                RegulationShowsPhone = model.RegulationShowsPhone,
                RegulationShowsTimetable = model.RegulationShowsTimetable,
                RegulationShowsServices = model.RegulationShowsServices,
                RegulationShowsRequestProcedure = model.RegulationShowsRequestProcedure,
                RegulationShowsReplayDeadline = model.RegulationShowsReplayDeadline,
                RegulationShowsClientRights = model.RegulationShowsClientRights,
                RegulationVerified = model.RegulationVerified,
                HelplinePhoneWorkStatus = model.HelplinePhoneWorkStatus,
                HelplinePhoneRatingOption = model.HelplinePhoneRatingOption,
                WebsiteHasHelplineStatistics = model.WebsiteHasHelplineStatistics,
                HelplineStatisticsByTime = model.HelplineStatisticsByTime,
                HelplineStatisticsByRank = model.HelplineStatisticsByRank,
                HelplineStatisticsArchiving = model.HelplineStatisticsArchiving,
                HelplineStatisticsIntime = model.HelplineStatisticsIntime,
            };
            addModel.Comment = model.Comment;
            addModel.Comment14 = model.Comment14;
            addModel.Comment2 = model.Comment2;
            addModel.Comment3 = model.Comment3;
            addModel.Comment4 = model.Comment4;
            addModel.Comment5 = model.Comment5;
            addModel.Comment6 = model.Comment6;
            addModel.Comment7 = model.Comment7;
            addModel.Comment8 = model.Comment8;
            addModel.Comment9 = model.Comment9;
            addModel.Comment10 = model.Comment10;
            addModel.Comment11 = model.Comment11;
            addModel.Comment12 = model.Comment12;
            addModel.Comment13 = model.Comment13;

            if (!String.IsNullOrEmpty(model.Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot);
                addModel.ScreenshotLink = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot2))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot2);
                addModel.Screenshot2Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot3))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot3);
                addModel.Screenshot3Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot4))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot4);
                addModel.Screenshot4Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot5))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot5);
                addModel.Screenshot5Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot6))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot6);
                addModel.Screenshot6Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot7))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot7);
                addModel.Screenshot7Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot8))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot8);
                addModel.Screenshot8Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot9))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot9);
                addModel.Screenshot9Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot10))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot10);
                addModel.Screenshot10Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot11))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot11);
                addModel.Screenshot11Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot12))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot12);
                addModel.Screenshot12Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot13))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot13);
                addModel.Screenshot13Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot14))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot14);
                addModel.Screenshot14Link = filePath;
            }

            _helplineInfo.Add(addModel);
        }
        public void Update(HelplineInfoCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var helplineInfo = _helplineInfo.Find(h => h.Id == model.Id).FirstOrDefault();
            if (helplineInfo == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            if(model.RegulationShowsPhone != null)
            {
                helplineInfo.RegulationShowsPhone = model.RegulationShowsPhone;
            }
            if (model.RegulationShowsTimetable != null)
            {
                helplineInfo.RegulationShowsTimetable = model.RegulationShowsTimetable;
            }
            if (model.RegulationShowsServices != null)
            {
                helplineInfo.RegulationShowsServices = model.RegulationShowsServices;
            }
            if (model.RegulationShowsRequestProcedure != null)
            {
                helplineInfo.RegulationShowsRequestProcedure = model.RegulationShowsRequestProcedure;
            }
            if (model.RegulationShowsReplayDeadline != null)
            {
                helplineInfo.RegulationShowsReplayDeadline = model.RegulationShowsReplayDeadline;
            }
            if (model.RegulationShowsClientRights != null)
            {
                helplineInfo.RegulationShowsClientRights = model.RegulationShowsClientRights;
            }
            if (model.RegulationVerified != null)
            {
                helplineInfo.RegulationVerified = model.RegulationVerified;
            }
            if (model.HelplinePhoneWorkStatus != null)
            {
                helplineInfo.HelplinePhoneWorkStatus = model.HelplinePhoneWorkStatus;
            }
            if (model.HelplinePhoneRatingOption != null)
            {
                helplineInfo.HelplinePhoneRatingOption = model.HelplinePhoneRatingOption;
            }
            if (model.WebsiteHasHelplineStatistics != null)
            {
                helplineInfo.WebsiteHasHelplineStatistics = model.WebsiteHasHelplineStatistics;
            }
            if (model.HelplineStatisticsByTime != null)
            {
                helplineInfo.HelplineStatisticsByTime = model.HelplineStatisticsByTime;
            }
            if (model.HelplineStatisticsByRank != null)
            {
                helplineInfo.HelplineStatisticsByRank = model.HelplineStatisticsByRank;
            }
            if (model.HelplineStatisticsArchiving != null)
            {
                helplineInfo.HelplineStatisticsArchiving = model.HelplineStatisticsArchiving;
            }
            if (model.HelplineStatisticsIntime != null)
            {
                helplineInfo.HelplineStatisticsIntime = model.HelplineStatisticsIntime;
            }
            if (!String.IsNullOrEmpty(model.Screenshot))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot);
                helplineInfo.ScreenshotLink = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot2))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot2);
                helplineInfo.Screenshot2Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot3))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot3);
                helplineInfo.Screenshot3Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot4))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot4);
                helplineInfo.Screenshot4Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot5))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot5);
                helplineInfo.Screenshot5Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot6))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot6);
                helplineInfo.Screenshot6Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot7))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot7);
                helplineInfo.Screenshot7Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot8))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot8);
                helplineInfo.Screenshot8Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot9))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot9);
                helplineInfo.Screenshot9Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot10))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot10);
                helplineInfo.Screenshot10Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot11))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot11);
                helplineInfo.Screenshot11Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot12))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot12);
                helplineInfo.Screenshot12Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot13))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot13);
                helplineInfo.Screenshot13Link = filePath;
            }
            if (!String.IsNullOrEmpty(model.Screenshot14))
            {
                var filePath = FileState.AddFile("screens", model.Screenshot14);
                helplineInfo.Screenshot14Link = filePath;
            }
            helplineInfo.Comment = model.Comment;
            helplineInfo.Comment14 = model.Comment14;
            helplineInfo.Comment2 = model.Comment2;
            helplineInfo.Comment3 = model.Comment3;
            helplineInfo.Comment4 = model.Comment4;
            helplineInfo.Comment5 = model.Comment5;
            helplineInfo.Comment6 = model.Comment6;
            helplineInfo.Comment7 = model.Comment7;
            helplineInfo.Comment8 = model.Comment8;
            helplineInfo.Comment9 = model.Comment9;
            helplineInfo.Comment10 = model.Comment10;
            helplineInfo.Comment11 = model.Comment11;
            helplineInfo.Comment12 = model.Comment12;
            helplineInfo.Comment13 = model.Comment13;
            _helplineInfo.Update(helplineInfo);
        }
        public void Delete(HelplineInfoCommand model)
        {
            var org = _organization.Find(o => o.Id == model.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(model.OrganizationId.ToString());
            var helplineInfo = _helplineInfo.Find(h => h.Id == model.Id).FirstOrDefault();
            if (helplineInfo == null)
                throw ErrorStates.NotAllowed(model.OrganizationId.ToString());
            if (!model.UserPermissions.Any(p => p == Permissions.SITE_CONTENT_FILLER) && !((model.UserOrgId == org.UserServiceId) && (model.UserPermissions.Any(p => p == Permissions.ORGANIZATION_EMPLOYEE))))
                throw ErrorStates.NotAllowed("permission");
            var deadline = _deadline.Find(d => d.IsActive == true).FirstOrDefault();
            if (deadline == null)
                throw ErrorStates.NotFound("available deadline");
            if (deadline.DeadlineDate < DateTime.Now)
                throw ErrorStates.NotAllowed(deadline.DeadlineDate.ToString());
            _helplineInfo.Remove(helplineInfo);
        }
    }
}
