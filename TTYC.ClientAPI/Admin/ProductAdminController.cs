using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stripe;
using TTYC.Application.Products.AddProduct;
using TTYC.Application.Products.DeleteProduct;
using TTYC.Application.Products.EditProduct;
using TTYC.Constants;

namespace TTYC.ClientAPI.Admin
{
    [Authorize(Roles = Roles.Admin)]
    [Route("[controller]")]
    [ApiController]
    public class ProductAdminController : ControllerBase
    {
        private readonly ISender mediatr;

        public ProductAdminController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds product.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductCommand command)
        {
            var productId = await mediatr.Send(command);
            return Ok(productId);
        }

        /// <summary>
        /// Edits product by id.
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> EditProduct([FromBody] EditProductCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Deletes product by id.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteProduct([FromQuery] DeleteProductCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
