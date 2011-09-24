using FubuTestingSupport;
using NUnit.Framework;
using Rhino.Mocks;

namespace SimpleServer.Tests
{
    [TestFixture]
    public class when_executing_an_http_request :  InteractionContext<HttpPipeline>
    {
        [Test]
        public void should_find_and_execute_policy()
        {
            var request = new HttpRequest();

            MockFor<IHttpRequestPolicy>()
                .Expect(policy => policy.Matches(request))
                .Return(true);

            MockFor<IHttpRequestPolicy>()
                .Expect(policy => policy.Execute(request));

            ClassUnderTest.Execute(request);
            VerifyCallsFor<IHttpRequestPolicy>();
        }
    }
}
