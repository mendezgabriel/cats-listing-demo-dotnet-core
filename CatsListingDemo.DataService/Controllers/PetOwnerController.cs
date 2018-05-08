using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace CatsListingDemo.DataService.Controllers
{
    [Route("api/[controller]")]
    public class PetOwnerController : Controller, IPetOwnerRepository
    {

        // GET api/values
        [HttpGet]
        public IEnumerable<PetOwner> GetPetOwners()
        {
            return new PetOwner[] {
                new PetOwner
                {
                    Gender = Gender.Male,
                    Name = "Owner1",
                    Pets = new Pet[]
                    {
                        new Pet{ Name = "Garfield", Type = PetType.Cat },
                        new Pet{ Name = "Moly", Type = PetType.Cat },
                        new Pet{ Name = "Tigger", Type = PetType.Dog }
                    }
                },
                new PetOwner
                {
                    Gender = Gender.Male,
                    Name = "Owner2",
                    Pets = new Pet[]
                    {
                        new Pet{ Name = "Fluffy", Type = PetType.Cat },
                        new Pet{ Name = "Nemo", Type = PetType.Fish }
                    }
                },
                new PetOwner
                {
                    Gender = Gender.Female,
                    Name = "Owner3",
                    Pets = new Pet[]
                    {
                        new Pet{ Name = "Lindsey", Type = PetType.Dog },
                        new Pet{ Name = "Puggles", Type = PetType.Cat },
                    }
                },
                new PetOwner
                {
                    Gender = Gender.Female,
                    Name = "Owner4",
                    Pets = new Pet[]
                    {
                        new Pet{ Name = "Ligthning", Type = PetType.Cat },
                        new Pet{ Name = "Ray", Type = PetType.Fish }
                    }
                }
            };
        }

    }
}
