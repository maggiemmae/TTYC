using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Products.AddProduct;
using TTYC.Application.Products.DeleteProduct;
using TTYC.Application.Products.EditProduct;
using TTYC.Application.Products.GetProduct;
using TTYC.Application.Products.GetProductsList;
using TTYC.Constants;

namespace TTYC.ClientAPI.Controllers
{
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
        /// Adds product.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
        {
            var productId = await mediatr.Send(command);
            return Ok(productId);
        }

        /// <summary>
        /// Gets paged list of products.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetProductsList([FromQuery] GeStoresListQuery query)
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
            var query = new GetProductQuery()
            {
                Id = id
            };
            var product = await mediatr.Send(query);
            return Ok(product);
        }

        /// <summary>
        /// Edits product by id.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] EditProductCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Deletes product by id.
        /// </summary>
        [Authorize(Roles = Roles.Admin)]
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
