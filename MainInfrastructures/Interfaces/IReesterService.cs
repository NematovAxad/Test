using Domain.ReesterModels;
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
        Task<bool> UpdateReestrTable();

        Task<FirstRequestQueryResult> FirstRequestNew(FirstRequestQuery model);
        Task<SecondRequestQueryResult> SecondRequestNew(SecondRequestQuery model);
    }
}
