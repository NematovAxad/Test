﻿using Domain.OpenDataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IOpenDataService
    {
        Task<OpenDataQueryResult> OpenDataApi(OpenDataQuery model);
    }
}