using AutoFixture;
using AutoFixture.AutoFakeItEasy;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace CatsListingDemo.Domain.Tests
{
    [TestClass]
    public class PetOwnerTests
    {
        private static Fixture _fixture;
        PetOwner _systemUnderTest;

        [ClassInitialize]
        public static void SetUpTestFixture(TestContext context)
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoFakeItEasyCustomization());
        }

        [TestInitialize]
        public void SetUpSystemUnderTest()
        {
            // Arrange
            _systemUnderTest = new PetOwner
            {
                Pets = null
            };
        }

        [TestMethod]
        public void SettingANullListOfPetsShouldNotReturnANullList()
        {
            // Act
            var actual = _systemUnderTest.Pets;

            // Assert
            actual.Should().NotBeNull();
        }

        [TestMethod]
        public void SettingANullListOfPetsShouldReturnAnEmptyList()
        {
            // Act
            var actual = _systemUnderTest.Pets;

            // Assert
            actual.Should().BeEmpty();
        }

        [TestMethod]
        public void SettingAListOfPetsShouldReturnTheSameList()
        {
            // Arrange
            var petsList = _fixture.Create<IEnumerable<Pet>>();
            _systemUnderTest.Pets = petsList;

            // Act
            var actual = _systemUnderTest.Pets;

            // Assert
            actual.Should().BeSameAs(petsList);
        }
    }
}
