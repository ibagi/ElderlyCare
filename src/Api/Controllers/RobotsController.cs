using MediatR;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace ElderlyCare.Api.Features.Robot
{
    [ApiController]
    [Route("[controller]")]
    public class RobotsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RobotsController(IMediator mediator) =>
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Get all robots
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRobots))]
        [ProducesResponseType(Status200OK)]
        [Produces(typeof(GetRobotsResponse))]
        public async Task<IActionResult> GetRobots()
        {
            var result = await _mediator.Send(new GetRobotsRequest());
            return Ok(result);
        }
    }
}
