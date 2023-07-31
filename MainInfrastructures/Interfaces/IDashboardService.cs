using System.Threading.Tasks;
using Domain.Models.DashboardModels;
using Domain.MyGovModels;

namespace MainInfrastructures.Interfaces
{
    public interface IDashboardService
    {
        Task<DashboardResultModel> GetDashboardData();
    }
}