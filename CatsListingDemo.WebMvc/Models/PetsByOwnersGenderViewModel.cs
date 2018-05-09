using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatsListingDemo.WebMvc.Models
{
    /// <summary>
    /// Defines the model used by to list the pets grouped by their owner's gender.
    /// </summary>
    public class PetsByOwnersGenderViewModel
    {
        /// <summary>
        /// The pet owner's gender.
        /// </summary>
        public Gender OwnerGender { get; set; }

        /// <summary>
        /// The collection of pets matched by the owner's gender group.
        /// </summary>
        public IEnumerable<Pet> Pets { get; set; } = new List<Pet>();
    }
}
