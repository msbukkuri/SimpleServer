using StructureMap.Configuration.DSL;

namespace SimpleServer.Host.Configuration
{
    public class HttpRegistry : Registry
    {
        public HttpRegistry()
        {
            For<IStreamHandler>().Use<HttpStreamHandler>();
            Scan(x =>
                     {
                         x.AssemblyContainingType<IHttpServer>();
                         x.ExcludeType<HttpRequestNotFoundPolicy>();
                         x.AddAllTypesOf<IHttpRequestPolicy>();
                     });
        }
    }
}