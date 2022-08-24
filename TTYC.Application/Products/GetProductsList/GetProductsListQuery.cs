using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;

namespace TTYC.Application.Products.GetProductsList
{
    public class GeStoresListQuery : PagingParameters, IRequest<PagedList<Product>>
    {
    }
}
