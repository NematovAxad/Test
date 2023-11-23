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

        Task<bool> UploadDigitalEconomyProjects(IFormFile file);
        Task<decimal> SubFieldMaxRate(int orgId, string fieldSection, string subFieldSection);
        Task<decimal> FieldMaxRate(int orgId, string fieldSection);

        Task<OrgExceptionPercentResultModel> GetOrganizationExceptionPercent(int orgId);

        Task<MemoryStream> DownloadOrgData(int orgId); // 1.1 report
        
        Task<MemoryStream> DownloadOrganizationsRateReport(OrgCategory category, int deadlineId); // detalni baholar
        
        Task<MemoryStream> DownloadOrgPingReport(List<string> userRights);  // 2.3 report
        
        Task<MemoryStream> DownloadOrganizationsReestrReport(List<string> userRights, int userOrgId);   // 5.1 report
        
        Task<MemoryStream> DownloadOrgSocialSitesReport(List<string> userRights);  // 2.5 report
        
        Task<MemoryStream> DownloadOrgOpenDataReport(List<string> userRights);  // 2.6 report

        Task<MemoryStream> DownloadOrgHelplineReport(List<string> userRights);  // 2.7 report

        Task<MemoryStream> DownloadOrgServicesReport(List<string> userRights);  //3.1 report
        
        Task<MemoryStream> DownloadOrgPublicServicesReport(List<string> userRights, int userOrgId);  //3.2 report
        
        Task<MemoryStream> DownloadOrgMygovServicesReport(List<string> userRights);  //3.3 report
        
        Task<MemoryStream> DownloadOrgMibServicesReport(List<string> userRights);  //3.4 report

        Task<MemoryStream> DownloadITDepartmentReport(List<string> userRights, int userOrgId); // 6.2 report

        Task<MemoryStream> DownloadOrgClassifications();

        Task<bool> ActivateDeactivateOrganizations(List<string> userRights, int orgId, bool activation);
    }
}
