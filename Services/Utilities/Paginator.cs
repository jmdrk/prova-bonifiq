using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Services.Utilities
{
    public class Paginator<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }
        public List<T> Items { get; private set; }

        public Paginator(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            TotalCount = count;
            PageSize = pageSize;
            Items = items;
        }

        public bool HasPrevious
        {
            get { return PageIndex > 1; }
        }

        public bool HasNext
        {
            get { return PageIndex < TotalPages; }
        }

        public static async Task<Paginator<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new Paginator<T>(items, count, pageIndex, pageSize);
        }
    }
    public static class PaginationDefaults
    {
        public const int PageSize = 10;
    }
}
