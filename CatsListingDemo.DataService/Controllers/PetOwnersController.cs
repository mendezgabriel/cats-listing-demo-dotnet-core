using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CatsListingDemo.DataService.Controllers
{
    [Route("api/[controller]")]
    public class PetOwnersController : Controller, IPetOwnerRepository
    {

        // GET api/values
        [HttpGet]
        public IEnumerable<PetOwner> GetAll()
        {
            var petOwners = new PetOwner[] { };
            string url = "http://agl-developer-test.azurewebsites.net/people.json";

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.Method = WebRequestMethods.Http.Get;
            httpWebRequest.Accept = "text/json";

            HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse();
            string responseContent = string.Empty;
            using (response)
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    responseContent = reader.ReadToEnd();
                }
            }

            if (!string.IsNullOrWhiteSpace(responseContent))
            {
                petOwners = JsonConvert.DeserializeObject<PetOwner[]>(responseContent);
            }
            
            return petOwners;
        }

    }
}
