﻿using Domain.ReesterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IReesterService
    {
        Task<FirstRequestQueryResult> FirstRequest(FirstRequestQuery model);
        Task<SecondRequestQueryResult> SecondRequest(SecondRequestQuery model);
    }
}