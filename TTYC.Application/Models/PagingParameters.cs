namespace TTYC.Application.Models
{
    public class PagingParameters
    {
        private int pageSize = 10;
        public int PageNumber { get; set; } = 1;
        const int maxPageSize = 50;

        public int PageSize
        {
            get => pageSize;
            set => pageSize = (value > maxPageSize) ? maxPageSize : value;
        }
    }
}
