using CatsListingDemo.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatsListingDemo.BusinessInterfaces
{
    public interface IPetOwnerProcessor
    {
        IEnumerable<PetOwner> GetAll();
    }
}
