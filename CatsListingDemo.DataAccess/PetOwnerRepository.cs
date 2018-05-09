using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace CatsListingDemo.DataAccess
{
    public class PetOwnerRepository : IPetOwnerRepository
    {
        private string _petOwnersServiceUrl;

        public PetOwnerRepository(IConfiguration configuration)
        {
            _petOwnersServiceUrl = configuration["PetOwnersService:Url"];
        }

        public IEnumerable<PetOwner> GetAll()
        {
            var petOwners = new PetOwner[] { };

            string serviceData = GetServiceData(_petOwnersServiceUrl);

            if (!string.IsNullOrWhiteSpace(serviceData))
            {
                petOwners = JsonConvert.DeserializeObject<PetOwner[]>(serviceData);
            }

            return petOwners;
        }

        private string GetServiceData(string serviceUrl)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(serviceUrl);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "text/json";

            HttpWebResponse serviceResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            string responseContent = string.Empty;
            using (serviceResponse)
            {
                using (StreamReader reader = new StreamReader(serviceResponse.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }
            }

            return responseContent;
        }
    }
}
