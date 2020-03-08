using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.RepositoryInterfaces
{
    /// <summary>
    /// Defines the contracts for repositories handling <see cref="Domain.PetOwner"/> objects.
    /// </summary>
    public interface IPetOwnerRepository
    {
        /// <summary>
        /// Gets a collection of all pet owners from the data store.
        /// </summary>
        /// <returns>A collection of <see cref="PetOwner"/>.</returns>
        Task<IEnumerable<PetOwner>> GetAllAsync();
    }
}
