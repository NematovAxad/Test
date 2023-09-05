using AutoMapper;
using Domain.CyberSecurityModels;
using Domain.Models.Ranking;
using MainInfrastructures.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Domain.Enums;

namespace MainInfrastructures.Interfaces
{
    public interface IOrganizationService
    {
        Task<RankingStruct> GetStruct(int orgId);
        Task<bool> UpdateOrgsName();
        Task<bool> UploadOrgServices(IFormFile file);

        Task<decimal> SubFieldMaxRate(int orgId, string fieldSection, string subFieldSection);
        Task<decimal> FieldMaxRate(int orgId, string fieldSection);

        Task<OrgExceptionPercentResultModel> GetOrganizationExceptionPercent(int orgId);

        Task<MemoryStream> DownloadOrgData(int orgId);
        Task<MemoryStream> DownloadOrganizationsRateReport(OrgCategory category, int deadlineId);
    }
}
