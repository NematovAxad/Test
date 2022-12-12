using Domain.CyberSecurityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface ICyberSecurityService
    {
        Task<GetOrgRanksResult> GetOrgRank(GetOrgRanksQuery model);
    }
}
