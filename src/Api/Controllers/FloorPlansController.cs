using ElderlyCare.Api.Features;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ElderlyCare.Api.Controllers
{
    [ApiController]
    [Route("floor-plans")]
    public class FloorPlansController : ControllerBase
    {
        private readonly IMediator _mediator;
        public FloorPlansController(IMediator mediator) =>
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Returns the default floor plan
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetFloorPlans))]
        [ProducesResponseType(Status200OK)]
        [Produces(typeof(GetFloorPlansResponse))]
        public async Task<IActionResult> GetFloorPlans()
        {
            var result = await _mediator.Send(new GetFloorPlansRequest());
            return Ok(result);
        }
    }
}
