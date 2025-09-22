using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewSystem;
using NewSystem.App.ToolsIoan;
namespace MiProyectoEFEC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ToolsIoanController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ToolsIoanController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>Gets all.</summary>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the list of tool loans, otherwise it returns an error.</returns>
        [HttpGet]
        public async Task<ActionResult<List<ToolsIoanList>>> GetAll(CancellationToken cancellation)
        {
            var result = await _mediator.Send(new GetToolsQuery(), cancellation);

            return result switch
            {
                NewSystem.Ok<List<ToolsIoanList>> ok => Ok(ok.Value),
                Error<List<ToolsIoanList>> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Creates the specified command.</summary>
        /// <param name="cmd">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns a success if the insertion is performed correctly.</returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateToolsIoanCommand cmd, CancellationToken cancellation)
        {
            var res = await _mediator.Send(cmd, cancellation);

            return res switch
            {
                NewSystem.Ok<CreateToolsIoan> ok => Ok(ok.Value),
                Error<CreateToolsIoan> error when error.Code == "InvalidQuantity" =>
                    BadRequest(new { error.Code, error.Message }),
                Error<CreateToolsIoan> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Updates the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns an object if the tool loan is successfully updated.</returns>
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateToolsIoanCommand command, CancellationToken cancellation)
        {
            command = command with { ToolIoanId = id };
            var res = await _mediator.Send(command, cancellation);

            return res switch
            {
                NewSystem.Ok<UpdateToolsIoan> ok => Ok(ok.Value),
                Error<UpdateToolsIoan> error when error.Code == "InvalidQuantity" =>
                    BadRequest(new { error.Code, error.Message }),
                Error<UpdateToolsIoan> error when error.Code == "InactiveIoan" =>
                    BadRequest(new {error.Code, error.Message}),
                Error <UpdateToolsIoan> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="id">The identifier.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the object if it was deleted successfully, otherwise it returns an error.</returns>
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id, CancellationToken cancellation)
        {
            var res = await _mediator.Send(new DeleteToolsIoanCommand(id), cancellation);

            return res switch
            {
                NewSystem.Ok<DeleteToolsIoan> ok => Ok(ok.Value),
                Error<DeleteToolsIoan> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Deletes the specified identifier.</summary>
        /// <param name="command"></param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the object if it was deleted successfully, otherwise it returns an error.</returns>
        [HttpGet("Filter")]
        public async Task<IActionResult> GetDataFilter([FromQuery] FilterToolsIoanCommand command, CancellationToken cancellation)
        {
            var result = await _mediator.Send(command, cancellation);

            return result switch
            {
                NewSystem.Ok<List<FilterToolsIoan>> ok => Ok(ok.Value),
                Error<List<FilterToolsIoan>> er => NotFound(new { er.Code, er.Message }),
                _ => StatusCode(500)
            };
        }
    }
}
