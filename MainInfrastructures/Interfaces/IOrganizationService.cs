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

        Task<MemoryStream> DownloadOrgData(int orgId); // 1.1 report
        
        Task<MemoryStream> DownloadOrganizationsRateReport(OrgCategory category, int deadlineId); // detalni baholar
        
        Task<MemoryStream> DownloadOrgPingReport(List<string> userRights);  // 2.3 report
        
        Task<MemoryStream> DownloadOrganizationsReestrReport(List<string> userRights, int userOrgId);   // 5.1 report
        
        Task<MemoryStream> DownloadOrgSocialSitesReport(List<string> userRights);
        
        Task<MemoryStream> DownloadOrgOpenDataReport(List<string> userRights);

        Task<MemoryStream> DownloadOrgClassifications();
    }
}
