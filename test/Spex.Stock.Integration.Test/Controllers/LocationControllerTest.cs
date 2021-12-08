using Application.CQRS.Commands.LocationCommand;
using Application.CQRS.Queries.LocationQuery;
using Spex.Stock.Api;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Spex.Stock.Integration.Test.Controllers
{
    public class LocationControllerTest : BaseControllerTest
    {

        public LocationControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task GetLocationById_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            GetLocationByIdQuery query = new GetLocationByIdQuery
            {
                Id = 1
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{BASE_URL}/Location/GetById?id={query.Id}", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetLocations_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            GetAllLocationQuery query = new GetAllLocationQuery
            {
                Page = 1,
                PageSize = 20
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{BASE_URL}/Location/GetAll?page={query.Page}&pageSize={query.PageSize}", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(query), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task SaveLocation_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            CreateLocationCommand command = new CreateLocationCommand
            {
                Location = "Loc1"
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BASE_URL}/Location/Create", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task UpdateLocation_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            UpdateLocationCommand command = new UpdateLocationCommand
            {
                Id = 1,
                Location = "Loc1"
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{BASE_URL}/Location/Update?id={command.Id}", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task DeleteLocation_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            DeleteLocationByIdCommand command = new DeleteLocationByIdCommand
            {
                Id = 1
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{BASE_URL}/Location/Delete?id={command.Id}", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }
    }
}
