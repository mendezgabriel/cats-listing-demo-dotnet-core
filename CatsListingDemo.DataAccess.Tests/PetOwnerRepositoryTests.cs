using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using CatsListingDemo.Domain;
using CatsListingDemo.RepositoryInterfaces;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CatsListingDemo.DataAccess.Tests
{
    [TestClass]
    public class PetOwnerRepositoryTests
    {
        private static Fixture _fixture;
        private PetOwnerRepository _systemUnderTest;
        private List<PetOwner> _petOwnersList;
        private Fake<IPetOwnerServiceClient> _petOwnerServiceClient;

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
            _petOwnerServiceClient = _fixture.Freeze<Fake<IPetOwnerServiceClient>>();
            _systemUnderTest = _fixture.Create<PetOwnerRepository>();

            _petOwnersList = _fixture.Freeze<List<PetOwner>>();
            A.CallTo(() => _petOwnerServiceClient.FakedObject.GetAsync()).Returns(_petOwnersList);

        }

        [TestMethod]
        public async Task AListOfPetOwnersShouldBeRetrievedWhenDataServiceIsAvailable()
        {
            // Act
            var actual = await _systemUnderTest.GetAllAsync();

            // Assert
            actual.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public async Task AnEmptyListShouldBeRetrievedWhenDataServiceIsNotAvailable()
        {
            // Arrange
            A.CallTo(() => _petOwnerServiceClient.FakedObject.GetAsync()).Returns(new List<PetOwner>());

            // Act
            var actual = await _systemUnderTest.GetAllAsync();

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public async Task NullShouldNotBeRetrievedWhenDataServiceIsNotAvailable()
        {
            // Arrange
            A.CallTo(() => _petOwnerServiceClient.FakedObject.GetAsync()).Returns(new List<PetOwner>());

            // Act
            var actual = await _systemUnderTest.GetAllAsync();

            // Assert
            actual.Should().NotBeNull();
        }
    }
}
