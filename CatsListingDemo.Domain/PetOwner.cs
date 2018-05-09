using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.Domain
{
    public class PetOwner
    {
        private IEnumerable<Pet> _pets;

        public string Name { get; set; }

        public Gender Gender { get; set; }

        public int Age { get; set; }

        public IEnumerable<Pet> Pets {

            get { return _pets; }
            set {
                var data = value;
                if(data == null || data.Count() == 0)
                {
                    data = new List<Pet>();
                }
                _pets = data;
            }
        }
    }
}
