using System;
using FubuCore;
using SimpleServer.Host.Configuration;
using StructureMap;

namespace SimpleServer.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            bootstrap();
            var server = Service<IHttpServer>();
            var settings = Service<HttpServerSettings>();

            describe(settings);
            server.Start();

            while (Console.ReadLine() != "q")
            {
                // listen
            }

            server.Stop();
        }

        private static TService Service<TService>()
        {
            return ObjectFactory.GetInstance<TService>();
        }

        private static void bootstrap()
        {
            ObjectFactory
                .Initialize(x => x.AddRegistry<CoreRegistry>());
        }

        private static void describe(HttpServerSettings settings)
        {
            Console.WriteLine("SimpleServer online");
            Console.WriteLine("Listening on: http://localhost:{0}/".ToFormat(settings.Port));
            Console.WriteLine("---------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Press 'q' to quit");
        }
    }
}
