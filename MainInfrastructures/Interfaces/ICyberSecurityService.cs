﻿using Domain.CyberSecurityModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface ICyberSecurityService
    {
        Task<bool> GetOrgRank(GetOrgRanksQuery model);
    }
}
