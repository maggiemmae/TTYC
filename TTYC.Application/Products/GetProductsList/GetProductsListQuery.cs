using MediatR;
using TTYC.Application.Models;
using TTYC.Domain;

namespace TTYC.Application.Products.GetProductsList
{
    public class GetProductsListQuery : IRequest<PagedList<Product>>
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;

        private int pageSize = 10;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
