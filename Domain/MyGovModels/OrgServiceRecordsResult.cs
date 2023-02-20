using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.MyGovModels
{
    public class OrgServiceRecordsResult
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ServiceId { get; set; }
        public string ServiceName { get; set; }
        public int AllRequests { get; set; }
        public int LateRequests { get; set; }
    }
}
