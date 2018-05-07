using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.Domain
{
    public class PetOwner
    {
        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public IEnumerable<Pet> Pets { get; set; } = new List<Pet>();
    }
}
