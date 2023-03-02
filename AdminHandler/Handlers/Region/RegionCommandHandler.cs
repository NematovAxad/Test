using AdminHandler.Commands.Region;
using AdminHandler.Results.Region;
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

namespace AdminHandler.Handlers.Region
{
    public class RegionCommandHandler : IRequestHandler<RegionCommand, RegionCommandResult>
    {
        private readonly IRepository<Regions, int> _regions;

        public RegionCommandHandler(IRepository<Regions, int> regions)
        {
            _regions = regions;
        }

        public async Task<RegionCommandResult> Handle(RegionCommand request, CancellationToken cancellationToken)
        {
            switch (request.EventType)
            {
                case Domain.Enums.EventType.Add: Add(request); break;
                case Domain.Enums.EventType.Update: Update(request); break;
                case Domain.Enums.EventType.Delete: Delete(request); break;
            }
            return new RegionCommandResult() { IsSuccess = true };
        }

        public void Add(RegionCommand model)
        {
            var region = _regions.Find(r => r.Name == model.Name).FirstOrDefault();
            if(region!=null)
            {
                if (region.ParentId == 0 && model.ParentId == 0)
                    throw ErrorStates.NotAllowed(model.Name);
            }
            
            if(model.ParentId!=0)
            {
                var reg = _regions.Find(r=>r.Id == model.ParentId).FirstOrDefault();
                if (reg == null)
                    throw ErrorStates.NotFound(model.ParentId.ToString());
            }
           
            Regions addModel = new Regions()
            {
                ParentId = model.ParentId,
                Name = model.Name,
            };
            _regions.Add(addModel);
        }
        public void Update(RegionCommand model)
        {
            var reg = _regions.Find(r => r.Id == model.Id).FirstOrDefault();
            if (reg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            reg.Name = model.Name;
            _regions.Update(reg);
        }
        public void Delete(RegionCommand model)
        {
            var reg = _regions.Find(r => r.Id == model.Id).FirstOrDefault();
            if (reg == null)
                throw ErrorStates.NotFound(model.Id.ToString());
            _regions.Remove(reg);
        }
    }
}
