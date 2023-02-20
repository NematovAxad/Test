using Domain.CyberSecurityModels;
using Domain.MyGovModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IMyGovService
    {
        Task<List<OrgServiceRecordsResult>> OrgServiceReport(int orgId, int deadlineId);
    }
}
