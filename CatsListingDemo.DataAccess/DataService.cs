using CatsListingDemo.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace CatsListingDemo.DataAccess
{
    /// <summary>
    /// Provides implementation details for the <see cref="IDataService"/>.
    /// </summary>
    public class DataService : IDataService
    {
        /// <summary>
        /// Gets the content of a specified resource by its Url.
        /// </summary>
        /// <param name="resourceUrl">The resource address.</param>
        /// <returns>The resource text content.</returns>
        public string GetResourceContent(string resourceUrl)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(resourceUrl);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "text/json";

            HttpWebResponse serviceResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responseContent = string.Empty;
            using (serviceResponse)
            {
                if (serviceResponse.StatusCode == HttpStatusCode.OK)
                {
                    using (StreamReader reader = new StreamReader(serviceResponse.GetResponseStream()))
                    {
                        responseContent = reader.ReadToEnd();
                    }
                }
            }

            return responseContent;
        }
    }
}
