namespace Stub.Core.Domain
{
    public class HttpStubResponse
    {
        public HttpStubResponse() { }

        public HttpStubResponse(string contentType, string content)
        {
            ContentType = contentType;
            BodyContent = content;
        }

        public string? ContentType { get; set; }
        public string? BodyContent { get; set; }

    }
}
