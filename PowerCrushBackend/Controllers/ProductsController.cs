using MediatR;
using Microsoft.AspNetCore.Mvc;
using NewSystem;
using NewSystem.App.Product;
using NewSystem.App.ToolsIoan;

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
        public async Task<ActionResult<List<ToolsIoanList>>> GetAll(CancellationToken cancellation)
        {
            var result = await _mediator.Send(new GetProductsQuery(), cancellation);

            return result switch
            {
                Ok<List<GetProductsList>> ok => Ok(ok.Value),
                Error<List<GetProductsList>> error => NotFound(new { error.Code, error.Message }),
                _ => StatusCode(500)
            };
        }

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
    }
}
