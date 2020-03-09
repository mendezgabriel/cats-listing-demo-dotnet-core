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
        private readonly IPetOwnerServiceClient _petOwnerServiceClient;

        /// <summary>
		/// Creates a new instance of this.
		/// </summary>
		/// <param name="petOwnerServiceClient">How to persist/retrieve pet owner data.</param>
        public PetOwnerRepository(IPetOwnerServiceClient petOwnerServiceClient)
        {
            _petOwnerServiceClient = petOwnerServiceClient;
        }

        /// <summary>
        /// Gets a collection of all pet owners from the data store.
        /// </summary>
        /// <returns>A collection of <see cref="PetOwner"/>.</returns>
        public Task<List<PetOwner>> GetAllAsync()
        {
            return _petOwnerServiceClient.GetAsync();
        }
    }
}
