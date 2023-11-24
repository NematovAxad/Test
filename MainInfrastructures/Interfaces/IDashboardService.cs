using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Domain.Models.DashboardModels;
using Domain.Models.FirstSection;
using Domain.MyGovModels;

namespace MainInfrastructures.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResultModel> GetDashboardData(int? deadlineId);
        
        Task<bool> TransferRanks(int deadlineFromId, int deadlineToId, string userPinfl);

        Task<bool> SetDashboardPeriod(List<string> userRight, int deadlineId);

        Task<bool> AddNews(AddNewsRequest request);

        Task<NewsOnDashboard> UpdateNews(UpdateNewsRequest request);

        Task<bool> DeleteNews(List<string> userRights, int id);

        Task<List<NewsOnDashboard>> GetNews(int id);

    }
}