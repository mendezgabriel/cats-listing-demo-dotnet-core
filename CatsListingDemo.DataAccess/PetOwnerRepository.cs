using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CatsListingDemo.DataAccess
{
    /// <summary>
    /// Provides implementation details for the <see cref="IPetOwnerRepository"/>.
    /// </summary>
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private readonly IConfiguration _configuration;
        private string _petOwnersServiceUrl;

        /// <summary>
		/// Creates a new instance of this.
		/// </summary>
		/// <param name="configuration">How to persist/retrieve configuration data.</param>
        public PetOwnerRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _petOwnersServiceUrl = _configuration["PetOwnersService:Url"];
        }

        /// <summary>
        /// Gets a collection of all pet owners from the data store.
        /// </summary>
        /// <returns>A collection of <see cref="PetOwner"/>.</returns>
        public IEnumerable<PetOwner> GetAll()
        {
            var petOwners = new PetOwner[] { };

            string serviceData = GetServiceData(_petOwnersServiceUrl);

            if (!string.IsNullOrWhiteSpace(serviceData))
            {
                petOwners = JsonConvert.DeserializeObject<PetOwner[]>(serviceData);
            }

            return petOwners;
        }

        /// <summary>
        /// Gets the response content text of a specified service by its Url.
        /// </summary>
        /// <param name="serviceUrl">The service address.</param>
        /// <returns>The response context text.</returns>
        private string GetServiceData(string serviceUrl)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "text/json";

            HttpWebResponse serviceResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responseContent = string.Empty;
            using (serviceResponse)
            {
                if (serviceResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(serviceResponse.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }

            return responseContent;
        }
    }
}
