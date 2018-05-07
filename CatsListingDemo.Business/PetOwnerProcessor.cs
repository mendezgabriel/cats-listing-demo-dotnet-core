using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public List<PetOwner> GetPetsByGender()
        {
            throw new NotImplementedException();
        }
    }
}
