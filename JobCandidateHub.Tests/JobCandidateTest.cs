using JobCandidateHubAPI;
using JobCandidateHubAPI.DataAccess;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net.Http.Json;

namespace JobCandidateHub.Tests
{
    public class JobCandidateTest: IClassFixture<WebApplicationFactory<Program>>

    {
        private readonly HttpClient _client;
        private readonly WebApplicationFactory<Program> _factory;

        public JobCandidateTest(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient(); // Use the client to make requests to the API

        }

        [Fact]
        public async Task AddCandidate_ValidCandidate_ReturnsCreated()
        {
            var candidate = new Candidate
            {
                FirstName = "Sahil",
                LastName = "Singh",
                Email = "Sahilm.Singh@gmail.com",
                PhoneNumber = "123456789",
                TimeInterval = "1 hour 30 minutes",
                LinkedInProfileURL = "http://linkedin.com/sahil",
                GitHubProfileURL = "http://github.com/sahil",
                Comment = "This is a test candidate."
            };

            // Act
            var response = await _client.PostAsJsonAsync("/api/candidates", candidate);
            var createdCandidate = await response.Content.ReadFromJsonAsync<Candidate>();

            // Assert
            Assert.Equal(System.Net.HttpStatusCode.Created, response.StatusCode);
            Assert.NotNull(createdCandidate);
            Assert.Equal(candidate.Email, createdCandidate.Email);
        }
    }
}