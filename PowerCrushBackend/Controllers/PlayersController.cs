using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewSystem;
using NewSystem.App.Player;

namespace PowerCrushBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PlayersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Gets all.</summary>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the list of tool loans, otherwise it returns an error.</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetPlayersList>>> GetAll(CancellationToken cancellation)
        {
            var result = await _mediator.Send(new GetPlayersQuery(), cancellation);

            return result switch
            {
                Ok<List<GetPlayersList>> ok => Ok(ok.Value),
                Error<List<GetPlayersList>> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Ups the sert.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns a boolean if everything is done correctly, if not, it returns an error.</returns>
        [HttpPost]
        public async Task<ActionResult<bool>> UpSert([FromBody] UpSertPlayerCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);

            return result switch
            {
                Ok<bool> ok => Ok(ok.Value),
                Error<bool> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Deletes the specified command.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns a boolean if everything is done correctly, if not, it returns an error.</returns>
        [HttpDelete]
        public async Task<ActionResult<bool>> Delete([FromQuery] RemovePlayerCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);

            return result switch
            {
                Ok<bool> ok => Ok(ok.Value),
                Error<bool> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }
    }
}
