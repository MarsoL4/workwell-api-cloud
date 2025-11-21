using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WorkWell.Application.DTOs.Paginacao;

namespace WorkWell.API.Helpers
{
    public static class HateoasLinkBuilder
    {
        public static List<LinkDto> BuildPagedLinks(HttpContext http, int page, int pageSize, int totalCount)
        {
            var links = new List<LinkDto>();
            var url = http.Request;
            var basePath = url.Path.HasValue ? url.Path.Value : "";
            string QueryStr(int p)
                => $"?page={p}&pageSize={pageSize}";

            int totalPages = (totalCount + pageSize - 1) / pageSize;

            // Self
            links.Add(new LinkDto($"{basePath}{QueryStr(page)}", "self", "GET"));

            // Next
            if (page < totalPages)
                links.Add(new LinkDto($"{basePath}{QueryStr(page + 1)}", "next", "GET"));

            // Prev
            if (page > 1)
                links.Add(new LinkDto($"{basePath}{QueryStr(page - 1)}", "prev", "GET"));

            // First
            links.Add(new LinkDto($"{basePath}{QueryStr(1)}", "first", "GET"));

            // Last
            if (totalPages > 0)
                links.Add(new LinkDto($"{basePath}{QueryStr(totalPages)}", "last", "GET"));

            return links;
        }
    }
}