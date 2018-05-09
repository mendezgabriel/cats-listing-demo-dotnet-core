using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsListingDemo.WebMvc.Models
{
    public class PetsByOwnersGenderViewModel
    {
        public Gender OwnerGender { get; set; }

        public IEnumerable<Pet> Pets { get; set; } = new List<Pet>();
    }
}
