using System;
using System.IO;
using System.Net;
using System.Threading;
using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_starting_to_listen : InteractionContext<HttpListener>
    {
        const int Port = 8181;
        [Test]
        public void should_listen_and_handle_requests()
        {
            var thread = new Thread(new ThreadStart(PushRequest));
            MockFor<IConnectionHandler>()
                .Expect(handler => handler.Handle(MockFor<IConnection>()));

            thread.Start();
            ClassUnderTest.Listen(Port);
            VerifyCallsFor<IConnectionHandler>();
        }

        private void PushRequest()
        {
            Thread.Sleep(5000);
            var responseStream = ((HttpWebRequest)WebRequest.Create("http://localhost:" + Port)).GetResponse();
        }

    }
}
