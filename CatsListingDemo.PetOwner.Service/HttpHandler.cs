using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Diagnostics.CodeAnalysis;

namespace CatsListingDemo.PetOwner.Service
{
    /// <summary>
    /// Defines the contracts for generic HTTP GET and POST requests.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class HttpHandler : IHttpHandler
    {
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Default constructor. Used to inject any dependencies.
        /// <param name="appConfiguration">The application configuration settings.</param>
        /// </summary>
        public HttpHandler(IConfiguration appConfiguration)
        {
            _httpClient = new HttpClient(new HttpRetryMessageHandler(new HttpClientHandler(), appConfiguration));
        }

        /// <inheritdoc />
        public async Task<HttpResponseMessage> GetAsync(HttpRequestMessage request)
        {
            request.Method = HttpMethod.Get;
            return await _httpClient.SendAsync(request);
        }

        /// <inheritdoc />
        public Task<HttpResponseMessage> PostAsync(HttpRequestMessage request)
        {
            throw new NotImplementedException();
        }
    }
}
