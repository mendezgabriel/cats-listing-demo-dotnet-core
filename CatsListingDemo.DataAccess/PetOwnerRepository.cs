using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace CatsListingDemo.DataAccess
{
    /// <summary>
    /// Provides implementation details for the <see cref="IPetOwnerRepository"/>.
    /// </summary>
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private readonly IDataService _dataService;
        private readonly IConfiguration _configuration;
        private string _petOwnersServiceUrl;

        /// <summary>
		/// Creates a new instance of this.
		/// </summary>
		/// <param name="configuration">How to persist/retrieve configuration data.</param>
        /// <param name="dataService">The external data service.</param>
        public PetOwnerRepository(IConfiguration configuration,
            IDataService dataService)
        {
            _dataService = dataService;
            _configuration = configuration;
            _petOwnersServiceUrl = _configuration["PetOwnersService:Url"];
        }

        /// <summary>
        /// Gets a collection of all pet owners from the data store.
        /// </summary>
        /// <returns>A collection of <see cref="PetOwner"/>.</returns>
        public async Task<IEnumerable<PetOwner>> GetAllAsync()
        {
            var petOwners = new PetOwner[] { };

            string resourceContent = _dataService.GetResourceContent(_petOwnersServiceUrl);

            if (!string.IsNullOrWhiteSpace(resourceContent))
            {
                petOwners = JsonConvert.DeserializeObject<PetOwner[]>(resourceContent);
            }

            return petOwners;
        }
    }
}
