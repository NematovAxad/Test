using Domain.CyberSecurityModels;
using Domain.Models.Ranking;
using MainInfrastructures.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IOrganizationService
    {
        Task<RankingStruct> GetStruct(int orgId);
        Task<bool> UpdateOrgsName();
        Task<bool> UploadOrgServices(IFormFile file);

        Task<decimal> SubFieldMaxRate(int orgId, string fieldSection, string subFieldSection);

        Task<OrgExceptionPercentResultModel> GetOrganizationExceptionPercent(int orgId);
    }
}
