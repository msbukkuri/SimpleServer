using StructureMap.Configuration.DSL;

namespace SimpleServer.Host.Configuration
{
    public class CoreRegistry : Registry
    {
        public CoreRegistry()
        {
            Scan(x =>
                     {
                         x.AssemblyContainingType<IHttpServer>();
                         x.TheCallingAssembly();
                         x.LookForRegistries();
                         x.WithDefaultConventions();
                     });
        }
    }
}