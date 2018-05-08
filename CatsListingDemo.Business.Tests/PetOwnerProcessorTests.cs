using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FluentAssertions;
using AutoFixture.AutoFakeItEasy;
using AutoFixture;
using FakeItEasy;
using CatsListingDemo.RepositoryInterfaces;
using CatsListingDemo.Domain;

namespace CatsListingDemo.Business.Tests
{
    [TestClass]
    public class PetOwnerProcessorTests
    {
        private static Fixture _fixture;
        private PetOwnerProcessor _systemUnderTest;
        private List<PetOwner> _petOwnersList;
        private Fake<IPetOwnerRepository> _petOwnerRepository;

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
            _systemUnderTest = _fixture.Create<PetOwnerProcessor>();

            _petOwnersList = _fixture.Freeze<List<PetOwner>>();
            A.CallTo(() => _petOwnerRepository.FakedObject.GetAll()).Returns(_petOwnersList);

        }

        [TestMethod]
        public void GetPetsByGenderShouldNotBeNull()
        {
            // Act
            var result = _systemUnderTest.GetPetsByGender();

            // Assert
            result.Should().NotBeNull();
        }

        [TestMethod]
        public void GetPetsByGenderShouldReturnAListOfPetOwners()
        {
            // Act
            var result = _systemUnderTest.GetPetsByGender();

            // Assert
            result.Should().BeOfType<List<PetOwner>>();
        }
    }
}
