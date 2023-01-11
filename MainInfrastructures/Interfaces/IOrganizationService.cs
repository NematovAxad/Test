using Domain.CyberSecurityModels;
using MainInfrastructures.Services;
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
    }
}
