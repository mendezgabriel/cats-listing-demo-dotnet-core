using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Polly;

namespace CatsListingDemo.PetOwner.Service
{
    /// <summary>
    /// Defines the policies for retrying HTTP operations.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HttpRetryMessageHandler : DelegatingHandler
    {
        private readonly Random _jitterer;
        private readonly int _maxNumberOfAttempts;
        private readonly int _intervalInSeconds;

        /// <summary>
        /// Default constructor. Used to inject any dependencies.
        /// </summary>
        /// <param name="handler">Default message handler used by the <see cref="HttpClient"/>.</param>
        /// <param name="appConfiguration">The application configuration settings.</param>
        public HttpRetryMessageHandler(HttpClientHandler handler,
            IConfiguration appConfiguration
        ) : base(handler)
        {
            _maxNumberOfAttempts = int.Parse(appConfiguration["RetryOptions:MaxNumberOfAttempts"]);
            _intervalInSeconds = int.Parse(appConfiguration["RetryOptions:IntervalInSeconds"]);
            _jitterer = new Random();
        }

        /// <inheritdoc />
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken) =>
            Policy
                .Handle<HttpRequestException>()
                .Or<TaskCanceledException>()
                .OrResult<HttpResponseMessage>(ShouldRetryRequest)
                .WaitAndRetryAsync(_maxNumberOfAttempts, SleepDurationProvider)
                .ExecuteAsync(SendRequestAsync(request, cancellationToken));

        private static bool ShouldRetryRequest(HttpResponseMessage httpResponseMessage) =>
            HttpStatusCodesWorthRetrying.Contains(httpResponseMessage.StatusCode);

        private TimeSpan SleepDurationProvider(int retryAttempt)
        {
            var retryInterval = TimeSpan.FromSeconds(_intervalInSeconds) +
                                TimeSpan.FromMilliseconds(_jitterer.Next(0, 100));
            
            return retryInterval;
        }

        private Func<Task<HttpResponseMessage>> SendRequestAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            return async () =>
            {
                var httpResponseMessage = await base.SendAsync(request, cancellationToken);
                return httpResponseMessage;
            };
        }

        private static IEnumerable<HttpStatusCode> HttpStatusCodesWorthRetrying => new[]
        {
            HttpStatusCode.RequestTimeout, // 408
            HttpStatusCode.InternalServerError, // 500
            HttpStatusCode.BadGateway, // 502
            HttpStatusCode.ServiceUnavailable, // 503
            HttpStatusCode.GatewayTimeout // 504
        };
    }
}
