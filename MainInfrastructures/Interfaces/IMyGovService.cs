using Domain.CyberSecurityModels;
using Domain.Models.Organization;
using Domain.MyGovModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IMyGovService
    {
        Task<List<MygovReports>> OrgServiceReport(int orgId, int deadlineId);
        Task<bool> UpdateMyGovReport(int deadlineId);

        Task<List<MygovReportsDetail>> MygovReportsDetails(int serviceId);
        Task<bool> UpdateMyGovReportDetails(int deadlineId);
    }
}
