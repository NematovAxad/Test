using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Interfaces
{
    public interface IPingService
    {
        public void CheckPing(object obj);
    }
}
