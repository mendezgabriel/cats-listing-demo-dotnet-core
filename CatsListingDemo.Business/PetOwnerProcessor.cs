using System.Collections.Generic;
using CatsListingDemo.BusinessInterfaces;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;

namespace CatsListingDemo.Business
{
    public class PetOwnerProcessor : IPetOwnerProcessor
    {
        private IPetOwnerRepository _petOwnerRepository;

        public PetOwnerProcessor(IPetOwnerRepository petOwnerRepository)
        {
            _petOwnerRepository = petOwnerRepository;
        }

        public IEnumerable<PetOwner> GetAll()
        {
            return _petOwnerRepository.GetAll();
        }
    }
}
