using Spex.Stock.Api;
using System.Net.Http;
using System.Text.Json;
using Xunit;

namespace Spex.Stock.Integration.Test.Controllers
{
    public abstract class BaseControllerTest : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        protected const string BASE_URL = "https://localhost:5001/api/v1";

        protected readonly HttpClient _client;
        protected readonly JsonSerializerOptions _options;
        protected readonly CustomWebApplicationFactory<Startup> _factory;

        public BaseControllerTest(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();

            if (_options == null)
            {
                _options = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };
            }
        }
    }
}
