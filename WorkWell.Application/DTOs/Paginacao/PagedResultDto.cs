using System.Collections.Generic;

namespace WorkWell.Application.DTOs.Paginacao
{
    public class PagedResultDto<T>
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Items { get; set; } = Enumerable.Empty<T>();
        public List<LinkDto> Links { get; set; } = new();
    }
}