namespace Stub.Api.Stub
{
    public static class RequestHeaderValues
    {
        public static string RequestKeyHeader(this HttpStubOptions stubOptions) => $"{stubOptions.RequestHeaderPrefix}-requestkey";
        public static string HostHeader(this HttpStubOptions stubOptions) => $"{stubOptions.RequestHeaderPrefix}-host";
        public static string ContentTypeHeader() => "Content-Type";

    }
}
