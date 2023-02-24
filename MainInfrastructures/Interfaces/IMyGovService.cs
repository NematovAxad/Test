using Domain.CyberSecurityModels;
using Domain.Models.Organization;
using Domain.MyGovModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MainInfrastructures.Services.MyGovService;

namespace MainInfrastructures.Interfaces
{
    public interface IMyGovService
    {
        Task<OrgServiceReportResult> MygovServiceReport(int orgId);
        Task<bool> UpdateMyGovReport(int deadlineId);

        Task<OrgServiceReportDetailResult> MygovReportsDetails(int serviceId, int orgId);
        Task<bool> UpdateMyGovReportDetails(int deadlineId);
    }
}
