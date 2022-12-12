using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.CyberSecurityModels
{
    public class GetOrgRanksQuery
    {
        public int OrgId { get; set; }
        public int DeadlineId { get; set; }
    }
}
