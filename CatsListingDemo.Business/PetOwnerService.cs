using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsListingDemo.BusinessInterfaces;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;

namespace CatsListingDemo.Business
{
    /// <summary>
    /// Process a <see cref="Domain.PetOwner"/> according to the business rules.
    /// </summary>
    public class PetOwnerService : IPetOwnerService
    {
        private readonly IPetOwnerRepository _petOwnerRepository;

        /// <summary>
		/// Creates a new instance of this.
		/// </summary>
		/// <param name="petOwnerRepository">How to persist/retrieve pet owner related data from the data repository.</param>
        public PetOwnerService(IPetOwnerRepository petOwnerRepository)
        {
            _petOwnerRepository = petOwnerRepository;
        }

        /// <summary>
        /// Gets a collection of <see cref="PetOwner"/> where each person owns at least one pet of the type
        /// specified by <paramref name="petType"/>.
        /// </summary>
        /// <param name="petType">The type of pet to be used as a filter.</param>
        /// <returns>A filtered collection of <see cref="PetOwner"/>.</returns>
        public async Task<List<PetOwner>> GetAllByAsync(PetType petType)
        {
            var ownersByPetType = (await _petOwnerRepository.GetAllAsync())
                .Where(owner => owner.Pets.Any(pet => pet.Type == petType));

            return ownersByPetType.ToList();
        }
    }
}
