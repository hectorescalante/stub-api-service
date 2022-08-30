namespace Stub.Api.Stub
{
    public static class RequestHeaderActions
    {
        public static string ActionHeader(this HttpStubOptions stubOptions) => $"{stubOptions.RequestHeaderPrefix}-action";
        public static string CreateKey = "createkey";
        public static string CreateStub = "createstub";
        public static string DeleteStub = "deletestub";
    }
}
