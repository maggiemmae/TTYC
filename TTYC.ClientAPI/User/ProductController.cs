using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Products.GetProduct;
using TTYC.Application.Products.GetProductsList;

namespace TTYC.ClientAPI.User
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ISender mediatr;

        public ProductController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Gets paged list of products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProductsList([FromQuery] GetProductsListQuery query)
        {
            var products = await mediatr.Send(query);
            return Ok(products);
        }

        /// <summary>
        /// Gets product by id.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct([FromRoute] Guid id)
        {
            var query = new GetProductQuery(id);
            var product = await mediatr.Send(query);
            return Ok(product);
        }
    }
}
