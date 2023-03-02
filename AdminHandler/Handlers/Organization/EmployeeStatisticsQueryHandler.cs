using AdminHandler.Querys.Organization;
using AdminHandler.Results.Organization;
using Domain.Models.FirstSection;
using Domain.States;
using JohaRepository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AdminHandler.Handlers.Organization
{
    public class EmployeeStatisticsQueryHandler : IRequestHandler<EmployeeStatisticsQuery, EmployeeStatisticsQueryResult>
    {
        private readonly IRepository<Organizations, int> _organizations;
        private readonly IRepository<EmployeeStatistics, int> _employeeStatistics;

        public EmployeeStatisticsQueryHandler(IRepository<Organizations, int> organizations, IRepository<EmployeeStatistics, int> employeeStatistics)
        {
            _organizations = organizations;
            _employeeStatistics = employeeStatistics;
        }

        public async Task<EmployeeStatisticsQueryResult> Handle(EmployeeStatisticsQuery request, CancellationToken cancellationToken)
        {
            var org = _organizations.Find(o => o.Id == request.OrganizationId).FirstOrDefault();
            if (org == null)
                throw ErrorStates.NotFound(request.OrganizationId.ToString());
            var stat = _employeeStatistics.GetAll();

            if (request.OrganizationId != 0)
            {
                stat = stat.Where(o => o.OrganizationId == request.OrganizationId);
            }

            EmployeeStatisticsQueryResult result = new EmployeeStatisticsQueryResult();
            result.Count = stat.Count();
            result.Data = stat.OrderBy(u => u.Id).ToList<object>();
            return result;
        }
    }
}
