using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewSystem;
using NewSystem.App.Product;

namespace PowerCrushBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>Gets all.</summary>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns the list of tool loans, otherwise it returns an error.</returns>
        [HttpGet]
        public async Task<ActionResult<List<GetProductsList>>> GetAll(CancellationToken cancellation)
        {
            var result = await _mediator.Send(new GetProductsQuery(), cancellation);

            return result switch
            {
                Ok<List<GetProductsList>> ok => Ok(ok.Value),
                Error<List<GetProductsList>> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

        /// <summary>Ups the sert.</summary>
        /// <param name="command">The command.</param>
        /// <param name="cancellation">The cancellation.</param>
        /// <returns>Returns a boolean if everything is done correctly, if not, it returns an error.</returns>
        [HttpPost]
        public async Task<ActionResult<bool>> UpSert([FromBody] UpSertProductCommand command, CancellationToken cancellation)
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
        public async Task<ActionResult<bool>> Delete([FromQuery] RemoveProductCommand command, CancellationToken cancellation)
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
