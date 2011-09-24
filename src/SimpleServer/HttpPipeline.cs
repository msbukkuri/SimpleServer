using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace SimpleServer
{
    public class HttpPipeline : IHttpPipeline
    {
        private readonly HttpRequestNotFoundPolicy _defaultPolicy;
        private readonly IEnumerable<IHttpRequestPolicy> _policies;

        public HttpPipeline(IEnumerable<IHttpRequestPolicy> policies, HttpRequestNotFoundPolicy defaultPolicy)
        {
            _defaultPolicy = defaultPolicy;
            var configuredPolicies = new List<IHttpRequestPolicy>();
            configuredPolicies.AddRange(policies);
            configuredPolicies.Add(defaultPolicy);

            _policies = configuredPolicies;
        }

        public void Execute(IHttpRequest request)
        {
            var policy =_policies.FirstOrDefault(p => p.Matches(request)) ?? _defaultPolicy;
            policy.Execute(request);
        }
    }

    public class HttpRequestNotFoundPolicy : IHttpRequestPolicy
    {
        public bool Matches(IHttpRequest request)
        {
            return true;
        }

        public void Execute(IHttpRequest request)
        {
            throw new HttpException(404, "Page not found!");
        }
    }
}