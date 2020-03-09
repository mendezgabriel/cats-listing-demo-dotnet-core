using System.Collections.Generic;

namespace CatsListingDemo.Domain
{
    /// <summary>
    /// A human being who is the owner of a <see cref="Pet"/>.
    /// </summary>
    public class PetOwner
    {
        private IEnumerable<Pet> _pets;

        /// <summary>
        /// The owner's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The owner's sex.
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// The owner's age.
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// A collection of pets owned.
        /// </summary>
        public IEnumerable<Pet> Pets
        {
            get { return _pets; }
            set { _pets = value ?? new List<Pet>(); }
        }
    }
}
