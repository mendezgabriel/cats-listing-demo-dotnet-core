using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.PetOwner.Service
{
    /// <summary>
    /// Defines the contracts for generic HTTP GET and POST requests.
    /// </summary>
    public interface IHttpHandler
    {
        /// <summary>
        /// Generic HTTP Client GetAsync request wrapper.
        /// </summary>
        /// <param name="request">The request message to send.</param>
        /// <returns>The resulting <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> GetAsync(HttpRequestMessage request);

        /// <summary>
        /// Generic HTTP Client PostAsync request wrapper.
        /// </summary>
        /// <param name="request">The request message to post.</param>
        /// <returns>The resulting <see cref="HttpResponseMessage"/>.</returns>
        Task<HttpResponseMessage> PostAsync(HttpRequestMessage request);
    }
}
