using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using CatsListingDemo.RepositoryInterfaces;
using CatsListingDemo.Domain;
using System.Linq;
using System;
using System.Threading.Tasks;

namespace CatsListingDemo.Business.Tests
{
    [TestClass]
    public class PetOwnerServiceTests
    {
        private static Fixture _fixture;
        private PetOwnerService _systemUnderTest;
        private List<PetOwner> _petOwnersList;
        private Fake<IPetOwnerRepository> _petOwnerRepository;
        private PetType _petType;

        [ClassInitialize]
        public static void SetUpTestFixture(TestContext context)
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoFakeItEasyCustomization());
        }

        [TestInitialize]
        public void SetUpSystemUnderTestAndDependencies()
        {
            // Arrange
            _petOwnerRepository = _fixture.Freeze<Fake<IPetOwnerRepository>>();
            _systemUnderTest = _fixture.Create<PetOwnerService>();
            _petType = PetType.Unknown;

            _petOwnersList = _fixture.Freeze<List<PetOwner>>();
            A.CallTo(() => _petOwnerRepository.FakedObject.GetAllAsync()).Returns(_petOwnersList);

        }

        [TestMethod]
        public async Task GetOwnersByPetTypeShouldNotBeNull()
        {
            // Act
            var result = await _systemUnderTest.GetAllByAsync(_petType);

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public async Task GetOwnersByPetTypeShouldReturnAListOfPetOwners()
        {
            // Act
            var result = await _systemUnderTest.GetAllByAsync(_petType);

            // Assert
            result.Should().BeOfType<List<PetOwner>>();
        }

        [TestMethod]
        public async Task GetOwnersByPetTypeShouldReturnOwnersWithTheSpecifiedPetTypeOnly()
        {
            // Arrange
            _petType = PetType.Dog;
            _petOwnersList[0].Pets = new List<Pet>() {
                    new Pet{ Name = "Ray", Type = PetType.Fish },
                    new Pet { Name = "Rufus", Type = PetType.Dog }
                };
            _petOwnersList[1].Pets = new List<Pet>() {
                    new Pet{ Name = "Fluffy", Type = PetType.Cat }
                };
            _petOwnersList[2].Pets = new List<Pet>() {
                    new Pet{ Name = "Guardian", Type = PetType.Dog }
                };

            // Act
            var result = await _systemUnderTest.GetAllByAsync(_petType);

            // Assert
            result.TrueForAll(owner =>
            {
                var ownsPetType = owner.Pets.Any(p => p.Type == _petType);
                return ownsPetType;

            }).Should()
            .BeTrue($"because there should be only owners with pets of type '{Enum.GetName(typeof(PetType), _petType)}' included in the results");

            result.Count().Should().Be(2);
        }
    }
}
