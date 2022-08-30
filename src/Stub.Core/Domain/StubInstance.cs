namespace Stub.Core.Domain
{
    public class StubInstance
    {
        public StubInstance(string requestKey, string contentType, string bodyContent)
        {
            Request = new HttpStubRequest(requestKey);
            Response = new HttpStubResponse(contentType, bodyContent);
        }

        public HttpStubRequest Request { get; set; }
        public HttpStubResponse Response { get; set; }

    }
}
