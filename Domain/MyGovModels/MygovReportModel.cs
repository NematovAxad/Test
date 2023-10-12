using Domain.Models.FirstSection;

namespace Domain.MyGovModels
{
    /// <summary>
    /// 
    /// </summary>
    public class MygovReportModel
    {
        /// <summary>
        /// 
        /// </summary>
        public Organizations Organization { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int ServiceId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ServiceName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int AllRequest { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int LateRequest { get; set; }
    }
}