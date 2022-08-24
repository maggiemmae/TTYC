using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;

namespace TTYC.Application.Stores.GetStoresList
{
    public class GetStoresListQuery : PagingParameters, IRequest<PagedList<Store>>
    { 
    }
}
