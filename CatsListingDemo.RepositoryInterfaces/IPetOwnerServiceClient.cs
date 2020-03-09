using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.RepositoryInterfaces
{
    /// <summary>
    /// Defines the contracts for accessing external data for pet owners.
    /// </summary>
    public interface IPetOwnerServiceClient
    {
        /// <summary>
        /// Gets a list of <see cref="PetOwner"/>.
        /// </summary>
        /// <returns></returns>
        Task<List<PetOwner>> GetAsync();
    }
}
