namespace CatsListingDemo.Domain
{
    /// <summary>
    /// Defines a domestic animal used by humans keep for companionship.
    /// </summary>
    public class Pet
    {
        /// <summary>
        /// The pet's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The pet's type.
        /// </summary>
        public PetType Type { get; set; }
    }
}
