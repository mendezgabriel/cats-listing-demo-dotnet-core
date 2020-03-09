using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Extensions.Configuration;
using CatsListingDemo.PetOwners.Service;
using AutoFixture;
using System.Collections.Generic;
using FakeItEasy;
using AutoFixture.AutoFakeItEasy;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;

namespace CatsListingDemo.PetOwner.Service.Tests
{
    [TestClass]
    public class PetOwnerServiceClientTests
    {
        private static Fixture _fixture;
        private PetOwnerServiceClient _systemUnderTest;
        private List<Domain.PetOwner> _petOwnersList;
        private const string _petOwnersUrl = "http://fake.service.url";
        private HttpResponseMessage _httpResponseMessage;
        private Fake<IHttpHandler> _httpHandler;
        private Fake<IConfiguration> _configuration;

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
            _httpHandler = _fixture.Freeze<Fake<IHttpHandler>>();
            _configuration = _fixture.Freeze<Fake<IConfiguration>>();
            _petOwnersList = _fixture.Freeze<List<Domain.PetOwner>>();

            _httpResponseMessage = _fixture.Build<HttpResponseMessage>()
                .With(x => x.StatusCode, HttpStatusCode.OK)
                .With(x => x.Content, new StringContent(JsonConvert.SerializeObject(_petOwnersList), Encoding.UTF8, "application/json"))
                .Create();

            A.CallTo(() => _configuration.FakedObject["PetOwnersService:Url"]).Returns(_petOwnersUrl);
            A.CallTo(() => _httpHandler.FakedObject.GetAsync(A<HttpRequestMessage>.Ignored)).Returns(_httpResponseMessage);

            _systemUnderTest = _fixture.Create<PetOwnerServiceClient>();
        }

        [TestMethod]
        public async Task GetShouldReturnShouldNotBeNull()
        {
            // Act
            var result = await _systemUnderTest.GetAsync();

            // Assert
            using (new AssertionScope())
            {
                result.Should().NotBeNull();
                result.Count.Should().Be(3);
                result.Should().BeEquivalentTo(_petOwnersList);
            }
        }

        [TestMethod]
        public async Task GetShouldReturnAListOfPetOwners()
        {
            // Act
            var result = await _systemUnderTest.GetAsync();

            // Assert
            result.Should().BeOfType<List<Domain.PetOwner>>();
        }

        [TestMethod]
        public async Task GetWhenNoResponseContentShouldReturnNull()
        {
            // Arrange
            _httpResponseMessage.Content = new StringContent(string.Empty, Encoding.UTF8, "application/json");

            // Act
            var result = await _systemUnderTest.GetAsync();

            // Assert
            result.Should().BeNull();
        }

        [TestMethod]
        public async Task GetWhenResponseIsUnsuccessfulShouldThrowApplicationException()
        {
            // Arrange
            _httpResponseMessage.StatusCode = HttpStatusCode.Forbidden;

            // Act
            Func<Task> act = () => _systemUnderTest.GetAsync();

            // Assert
            await act.Should().ThrowAsync<ApplicationException>();
        }
    }
}
