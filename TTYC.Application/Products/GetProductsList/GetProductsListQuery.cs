using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;

namespace TTYC.Application.Products.GetProductsList
{
    public class GetProductsListQuery : PagingParameters, IRequest<PagedList<Product>>
    {
    }
}
