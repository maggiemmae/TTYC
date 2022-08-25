using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TTYC.Application.Carts.AddProductToCart;
using TTYC.Application.Carts.DeleteProductFromCart;
using TTYC.Application.Carts.GetCart;

namespace TTYC.ClientAPI.User
{
    [Authorize]
    [Route("[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        readonly ISender mediatr;

        public CartController(ISender mediatr)
        {
            this.mediatr = mediatr;
        }

        /// <summary>
        /// Adds product to cart.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> AddProductToCart(AddProductToCartCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }

        /// <summary>
        /// Views user cart.
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var query = new GetCartQuery();
            return Ok(await mediatr.Send(query));
        }

        /// <summary>
        /// Deletes product from user cart.
        /// </summary>
        [HttpDelete]
        public async Task<IActionResult> DeleteProductFromCart(DeleteProductFromCartCommand command)
        {
            await mediatr.Send(command);
            return Ok();
        }
    }
}
