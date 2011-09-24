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
            var failed = false;
            var thread = new Thread(() =>
                                        {
                                            Thread.Sleep(1000);
                                            try
                                            {
                                                var request = (HttpWebRequest)WebRequest.Create("http://localhost:" + Port);
                                                request.KeepAlive = false;
                                                using (var response = request.GetResponse())
                                                {
                                                }
                                            }
                                            catch (WebException exc)
                                            {
                                                // ew...
                                                failed = !exc
                                                    .Message
                                                    .ToLower()
                                                    .Contains("the underlying connection was closed");
                                            }
                                            catch (Exception)
                                            {
                                                failed = true;
                                            }
                                            finally
                                            {
                                                ClassUnderTest.Stop();
                                            }
                                        });

            MockFor<IConnectionHandler>()
                .Expect(handler => handler.Handle(Arg<IConnection>.Matches(con => con.GetType().Equals(typeof(Connection)))));

            ClassUnderTest.Listen(Port, thread.Start);

            if(failed)
            {
                Assert.Fail("Could not connect to HttpListener");
            }

            VerifyCallsFor<IConnectionHandler>();
        }
    }
}
