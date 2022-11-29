using ElderlyCare.Api.Models;
using ElderyCare.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElderlyCare.Api.Features
{
    public record GetFloorPlansRequest : IRequest<GetFloorPlansResponse>;
    public record GetFloorPlansResponse(List<FloorPlan> FloorPlans);

    public class GetFloorPlansHandler : IRequestHandler<GetFloorPlansRequest, GetFloorPlansResponse>
    {
        private readonly ElderlyCareContext _dbContext;

        public GetFloorPlansHandler(ElderlyCareContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetFloorPlansResponse> Handle(GetFloorPlansRequest request, CancellationToken cancellationToken)
        {
            var floorPlans = await _dbContext.FloorPlans
                .Select(x => new FloorPlan { Id = x.Id, Name = x.Name, SvgUrl = x.SvgUrl })
                .ToListAsync();

            return new GetFloorPlansResponse(floorPlans);
        }
    }
}
