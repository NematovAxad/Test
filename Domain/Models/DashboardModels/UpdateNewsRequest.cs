using System.Collections.Generic;
using System.Text.Json.Serialization;
using Domain.Enums;

namespace Domain.Models.DashboardModels
{
    public class UpdateNewsRequest
    {
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public string UserPinfl { get; set; }
        [JsonIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public List<string> UserPermissions { get; set; }
        
        public int Id { get; set; }
        
        public string Title { get; set; }
        
        public string Body { get; set; }
        
        public string FileLink { get; set; }
        
        public bool First { get; set; }
    }
}