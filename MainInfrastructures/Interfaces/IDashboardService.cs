using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.DashboardModels;
using Domain.MyGovModels;

namespace MainInfrastructures.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResultModel> GetDashboardData(int? deadlineId);
        
        Task<bool> TransferRanks(int deadlineFromId, int deadlineToId, string userPinfl);

        Task<bool> SetDashboardPeriod(List<string> userRight, int deadlineId);
    }
}