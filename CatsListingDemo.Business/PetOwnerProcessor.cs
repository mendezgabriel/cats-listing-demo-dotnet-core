using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CatsListingDemo.BusinessInterfaces;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using System.Linq;

namespace CatsListingDemo.Business
{
    public class PetOwnerProcessor : IPetOwnerProcessor
    {
        private IPetOwnerRepository _petOwnerRepository;

        public PetOwnerProcessor(IPetOwnerRepository petOwnerRepository)
        {
            _petOwnerRepository = petOwnerRepository;
        }

        public List<PetOwner> GetPetsByGender()
        {
            var petsByGender = _petOwnerRepository.GetAll()
               // .GroupBy(petOwner => petOwner.Gender)
               // .OrderBy(petOwner => petOwner.Key);
               .OrderBy(petOwner => petOwner.Gender);

            return petsByGender.ToList();

        }
    }
}
