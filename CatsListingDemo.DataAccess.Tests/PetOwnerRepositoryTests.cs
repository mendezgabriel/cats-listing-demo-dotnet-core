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

namespace CatsListingDemo.DataAccess.Tests
{
    [TestClass]
    public class PetOwnerRepositoryTests
    {
        private static Fixture _fixture;
        private PetOwnerRepository _systemUnderTest;
        private List<PetOwner> _petOwnersList;
        private Fake<IConfiguration> _configuration;
        private Fake<IDataService> _dataService;

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
            _configuration = _fixture.Freeze<Fake<IConfiguration>>();
            _dataService = _fixture.Freeze<Fake<IDataService>>();
            _systemUnderTest = _fixture.Create<PetOwnerRepository>();

            _petOwnersList = _fixture.Freeze<List<PetOwner>>();
            A.CallTo(() => _configuration.FakedObject["PetOwnersService:Url"]).Returns(string.Empty);
            A.CallTo(() => _dataService.FakedObject.GetResourceContent(string.Empty)).Returns(JsonConvert.SerializeObject(_petOwnersList));

        }

        [TestMethod]
        public void AListOfPetOwnersShouldBeRetrievedWhenDataServiceIsAvailable()
        {
            // Act
            var actual = _systemUnderTest.GetAll();

            // Assert
            actual.Should().NotBeNullOrEmpty();
        }

        [TestMethod]
        public void AnEmptyListShouldBeRetrievedWhenDataServiceIsNotAvailable()
        {
            // Arrange
            A.CallTo(() => _dataService.FakedObject.GetResourceContent(A<string>.Ignored)).Returns(string.Empty);

            // Act
            var actual = _systemUnderTest.GetAll();

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void NullShouldNotBeRetrievedWhenDataServiceIsNotAvailable()
        {
            // Arrange
            A.CallTo(() => _dataService.FakedObject.GetResourceContent(A<string>.Ignored)).Returns(string.Empty);

            // Act
            var actual = _systemUnderTest.GetAll();

            // Assert
            actual.Should().NotBeNull();
        }
    }
}
