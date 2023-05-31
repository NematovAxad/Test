using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IMibService
    {
        Task<bool> MibReport(DateTime startTime, DateTime endTime);
    }
}
