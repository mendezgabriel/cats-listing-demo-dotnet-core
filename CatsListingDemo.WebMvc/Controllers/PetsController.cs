using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatsListingDemo.BusinessInterfaces;
using CatsListingDemo.Domain;
using CatsListingDemo.WebMvc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatsListingDemo.WebMvc.Controllers
{
    public class PetsController : Controller
    {
        private IPetOwnerProcessor _petOwnerProcessor;

        public PetsController(IPetOwnerProcessor petOwnerProcessor)
        {
            _petOwnerProcessor = petOwnerProcessor;
        }

        // GET: Pets
        public ActionResult Index()
        {

            var viewModel = new List<PetsByOwnersGenderViewModel>();

            var ownersByGenderGroup = _petOwnerProcessor.GetAll()
                .GroupBy(owner => owner.Gender);

            ownersByGenderGroup.ToList().ForEach(groupedItem =>
            {
                var viewModelItem = new PetsByOwnersGenderViewModel
                {
                    OwnerGender = groupedItem.Key,
                    Pets = groupedItem.SelectMany(owner => {

                        return owner.Pets
                        .Where(pet => pet.Type == PetType.Cat)
                        .OrderBy(pet => pet.Name);

                    })
                };

                viewModel.Add(viewModelItem);

            });

            return View(viewModel);
        }
    }
}