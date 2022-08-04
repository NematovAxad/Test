using Domain.Models;
using Domain.Models.Organization;
using Domain.Tasks;
using EntityRepository;
using JohaRepository;
using MainInfrastructures.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MainInfrastructures.Tasks
{
    public class PingTask: ScheduledProcessor
    {
        private readonly IPingService _pingService;

        public PingTask(IServiceScopeFactory serviceScopeFactory, IPingService pingService) : base(serviceScopeFactory)
        {
            _pingService = pingService;
        }

        protected override string Schedule => "* */1 * * * *";

        public override async Task ProcessInScope(IServiceProvider serviceProvider)
        {
            Console.WriteLine($"Task started! Execution time: {DateTime.Now.ToString()}");
            _pingService.CheckPing();
            Console.WriteLine($"Task ended ! Execution time: {DateTime.Now.ToString()}");
        }
    }
}
