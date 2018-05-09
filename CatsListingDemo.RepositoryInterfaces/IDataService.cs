using System;
using System.Collections.Generic;
using System.Text;

namespace CatsListingDemo.RepositoryInterfaces
{
    /// <summary>
    /// Defines the contract for extracting data from an external service.
    /// </summary>
    public interface IDataService
    {
        /// <summary>
        /// Gets the content of a specified resource by its Url.
        /// </summary>
        /// <param name="resourceUrl">The resource address.</param>
        /// <returns>The resource text content.</returns>
        string GetResourceContent(string resourceUrl);
    }
}
