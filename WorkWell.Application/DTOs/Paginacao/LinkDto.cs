namespace WorkWell.Application.DTOs.Paginacao
{
    public class LinkDto
    {
        public string Href { get; set; } = null!;
        public string Rel { get; set; } = null!;
        public string Method { get; set; } = null!;
        public bool Templated { get; set; }

        public LinkDto(string href, string rel, string method = "GET", bool templated = false)
        {
            Href = href;
            Rel = rel;
            Method = method.ToUpper();
            Templated = templated;
        }
    }
}