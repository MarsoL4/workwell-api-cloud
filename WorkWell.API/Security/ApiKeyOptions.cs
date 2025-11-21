namespace WorkWell.API.Security
{
    public class ApiKeyOptions
    {
        public required Dictionary<string, string> Keys { get; set; }
        public required string SuperKey { get; set; }
    }
}