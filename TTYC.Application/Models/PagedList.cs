using Microsoft.EntityFrameworkCore;

namespace TTYC.Application.Models
{
    public class PagedList<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }

        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;

        public PagedList(IEnumerable<T> items, int totalCount, int currentPage, int pageSize)
        {
            Items = items;
            CurrentPage = currentPage;
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
            TotalCount = totalCount;
        }

        public static async Task<PagedList<T>> ToPagedListAsync(IQueryable<T> source, int currentPage, int pageSize)
        {
            var totalCount = await source.CountAsync();
            var items = await source
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
            return new PagedList<T>(items, totalCount, currentPage, pageSize);
        }
    }
}
