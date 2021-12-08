using Application.CQRS.Commands.StockCommand;
using Application.CQRS.Queries.StockQuery;
using DomainModel;
using Spex.Stock.Api;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace Spex.Stock.Integration.Test.Controllers
{
    public class StockControllerTest : BaseControllerTest
    {
        public StockControllerTest(CustomWebApplicationFactory<Startup> factory) : base(factory)
        {

        }

        [Fact]
        public async Task CreateStockNotExist_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            CreateStockCommand command = new CreateStockCommand
            {
                Stocks = new List<StockRequest>
                {
                    new StockRequest
                    {
                        Sku = "A10",
                        LocationId = 1,
                        Quantity = 20
                    },
                    new StockRequest
                    {
                        Sku = "A10",
                        LocationId = 2,
                        Quantity = 20
                    },
                    new StockRequest
                    {
                        Sku = "A20",
                        LocationId = 1,
                        Quantity = 30
                    },
                    new StockRequest
                    {
                        Sku = "A20",
                        LocationId = 2,
                        Quantity = 30
                    },
                    new StockRequest
                    {
                        Sku = "A30",
                        LocationId = 3,
                        Quantity = 10
                    }
                }
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BASE_URL}/Stock/Create", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command.Stocks), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task UpdateExistingStock_EndpointReturnSuccessAndCorrectContent()
        {
            // Arrange
            CreateStockCommand command = new CreateStockCommand
            {
                Stocks = new List<StockRequest>
                {
                    new StockRequest
                    {
                        Sku = "A10",
                        LocationId = 1,
                        Quantity = 20
                    },
                    new StockRequest
                    {
                        Sku = "A10",
                        LocationId = 2,
                        Quantity = 20
                    },
                    new StockRequest
                    {
                        Sku = "A20",
                        LocationId = 1,
                        Quantity = 30
                    },
                    new StockRequest
                    {
                        Sku = "A20",
                        LocationId = 2,
                        Quantity = 30
                    },
                    new StockRequest
                    {
                        Sku = "A30",
                        LocationId = 3,
                        Quantity = 10
                    }
                }
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BASE_URL}/Stock/Create", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command.Stocks), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task EmptyRequestStock_EndpointReturnBadRequest()
        {
            // Arrange
            CreateStockCommand command = new CreateStockCommand
            {
                Stocks = new List<StockRequest>()
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{BASE_URL}/Stock/Create", UriKind.RelativeOrAbsolute)
            };

            requestMessage.Content = new StringContent(JsonSerializer.Serialize(command), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }

        [Fact]
        public async Task GetStockBySkus_EndPointReturnSuccessAndCorrectContent()
        {
            // Arrange
            GetStockBySkusQuery query = new GetStockBySkusQuery
            {
                Skus = new List<string>
                {
                    "A10",
                    "A20",
                    "A30"
                }
            };

            HttpRequestMessage requestMessage = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{BASE_URL}/Stock/GetStock", UriKind.RelativeOrAbsolute)
            };
                        
            requestMessage.Headers.Add("skus", query.Skus);
            requestMessage.Content = new StringContent(JsonSerializer.Serialize(query.Skus), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.SendAsync(requestMessage);

            // Assert
            response.EnsureSuccessStatusCode();

            Assert.NotNull(response);
        }
    }
}
