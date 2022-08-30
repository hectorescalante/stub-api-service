using Microsoft.Extensions.Primitives;
using Stub.Core.Application;

namespace Stub.Core.Domain
{
    public class HttpStubRequest
    {
        public HttpStubRequest() { }

        public HttpStubRequest(string requestKey)
        {
            var request = requestKey.Split('|');

            IsHttps = request[0].Contains("https");
            Method = request[1];
            Host = request[2];
            if (request.Length > 3)
                Path = request[3];
            if (request.Length > 4)
                QueryString = request[4];
            if (request.Length > 5)
                BodyContent = request[5];
        }

        public ICollection<KeyValuePair<string, StringValues>> Headers { get; set; } = new List<KeyValuePair<string, StringValues>>();
        public bool IsHttps { get; set; }
        public string? Method { get; set; }
        public string? Host { get; set; }
        public string? Path { get; set; }
        public string? QueryString { get; set; }
        public string? ContentType { get; set; }
        public string? BodyContent { get; set; }

        public string Http
        {
            get
            {
                return IsHttps ? "https://" : "http://";
            }
        }
        public string GetRequestKey() => $"{Http}|{Method}|{Host}|{Path}|{QueryString}|{BodyContent}".Minify();
        public string GetUri() => $"{Http}{Host}{Path}{QueryString}";
        public string GetPathWithQueryString() => $"{Path}{QueryString}";

    }
}
