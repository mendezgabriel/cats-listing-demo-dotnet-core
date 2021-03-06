﻿using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatsListingDemo.BusinessInterfaces
{
    /// <summary>
    /// Defines the contract methods for the business layer in order to process a <see cref="Domain.PetOwner"/> object.
    /// </summary>
    public interface IPetOwnerService
    {
        /// <summary>
        /// Gets a collection of <see cref="PetOwner"/> where each person owns at least one pet of the type
        /// specified by <paramref name="petType"/>.
        /// </summary>
        /// <param name="petType">The type of pet to be used as a filter.</param>
        /// <returns>A filtered collection of <see cref="PetOwner"/>.</returns>
        Task <List<PetOwner>> GetAllByAsync(PetType petType);
    }
}
