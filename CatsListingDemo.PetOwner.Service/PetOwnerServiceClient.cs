using CatsListingDemo.PetOwner.Service;
using CatsListingDemo.RepositoryInterfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CatsListingDemo.PetOwners.Service
{
    /// <inheritdoc/>
    public class PetOwnerServiceClient : IPetOwnerServiceClient
    {
        private readonly Uri _petOwnersServiceUri;
        private readonly IHttpHandler _httpHandler;

        /// <summary>
        /// Default constructor. Used to inject any dependencies.
        /// </summary>
        /// <param name="appConfiguration">The application configuration settings.</param>
        /// <param name="httpHandler">The http proxy handler.</param>
        public PetOwnerServiceClient(IConfiguration appConfiguration,
            IHttpHandler httpHandler)
        {
            _petOwnersServiceUri = new Uri(appConfiguration["PetOwnersService:Url"]);
           _httpHandler = httpHandler;
        }

        /// <inheritdoc/>
        public async Task<List<Domain.PetOwner>> GetAsync()
        {
            var request = BuildRequest(_petOwnersServiceUri);
            var response = await _httpHandler.GetAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                throw new ApplicationException($"Cannot communicate with the pet owners service. Details: {response.StatusCode} - {response.ReasonPhrase}");
            }

            var deserializedContent = await GetResponseContent<List<Domain.PetOwner>>(response);
            return deserializedContent;
        }

        private HttpRequestMessage BuildRequest(Uri uri)
        {
            var requestMessage = new HttpRequestMessage { RequestUri = uri };

            requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            return requestMessage;
        }

        private static async Task<TResponse> GetResponseContent<TResponse>(HttpResponseMessage response)
        {
            var responseString = response.Content != null ? await response.Content.ReadAsStringAsync() : string.Empty;

            if (string.IsNullOrWhiteSpace(responseString))
                return default;

            return JsonConvert.DeserializeObject<TResponse>(responseString);
        }
    }
}
