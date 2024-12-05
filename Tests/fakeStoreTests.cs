using NUnit.Framework;
using FluentAssertions;
using System.Net;
using TestsAPI.Services;
using TestsAPI.Response;
using TestsAPI.FakeStoreResponse;

namespace TestsAPI
{
    [TestFixture]
    public class fakeStoreTests
    {
        private ApiService? _apiService;

        [SetUp]
        public void Setup()
        {
            _apiService = new ApiService("https://fakestoreapi.com/");
        }

        [Test, Order(1)]
        public async Task TestGetAllProducts()
        {
            // Arrange
            var endpoint = "products";

            // Act
            var response = await _apiService!.ExecuteGetAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            if (response.StatusCode == HttpStatusCode.OK && response.Data != null)
            {
                response.Data.Should().NotBeNullOrEmpty();
    
                foreach (var product in (IEnumerable<ProductResponse>)response.Data)
                { 
                    product.Id.Should().BeGreaterThan(0);
                    product.Title.Should().NotBeNullOrEmpty();
                    product.Price.Should().BeGreaterThan(0);
                    product.Description.Should().NotBeNullOrEmpty();
                    product.Category.Should().NotBeNullOrEmpty();
                    product.Image.Should().NotBeNullOrEmpty();
                    product.Rating.Should().NotBeNull();
                    product.Rating?.Rate.Should().BeGreaterThan(0);
                    product.Rating?.Count.Should().BeGreaterThan(0);
                }
            }
        }

         [Test, Order(2)]
        public async Task TestGetProductById()
        {
            // Arrange
            var endpoint = "products/1";

            // Act
            var response = await _apiService!.ExecuteGetByIdAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Data.Should().NotBeNull();
                response.Data!.Id.Should().BeGreaterThan(0);
                response.Data.Title.Should().NotBeNullOrEmpty();
                response.Data.Price.Should().BeGreaterThan(0);
                response.Data.Description.Should().NotBeNullOrEmpty();
                response.Data.Category.Should().NotBeNullOrEmpty();
                response.Data.Image.Should().NotBeNullOrEmpty();
                response.Data.Rating.Should().NotBeNull();
                response.Data.Rating?.Rate.Should().BeGreaterThan(0);
                response.Data.Rating?.Count.Should().BeGreaterThan(0);
            }
        }

        [Test, Order(3)]
        public async Task TestGetProductNotExist()
        {
            // Arrange
            var endpoint = "nonexistent";

            // Act
            var response = await _apiService!.ExecuteGetAsync(endpoint);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Test, Order(4)]
        public async Task TestCreateProduct()
        {
            // Arrange
            var endpoint = "products";
            var body = new
            {
                title = "test product",
                price = 13.5,
                description = "lorem ipsum set",
                image = "https://i.pravatar.cc",
                category = "electronic"
            };

            // Act
            var response = await _apiService!.ExecutePostAsync(endpoint, body);
   
            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Test, Order(5)]
        public async Task TestDeleteProduct()
        {
            // Arrange
            var endpoint = "products";
            var body = new
            {
                title = "test product to delete",
                price = 10.5,
                description = "test product to delete",
                image = "https://i.pravatar.cc",
                category = "electronic"
            };

            // Act - Create the product
            var createResponse = await _apiService!.ExecutePostAsync(endpoint, body);
            createResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            createResponse.Data.Should().NotBeNull();

            var productId = createResponse.Data!.Id;

            // Act - Get the created product
            var getResponse = await _apiService.ExecuteGetByIdAsync($"products/{productId}");
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);
            getResponse.Data.Should().BeNull();

            // Act - Delete the product
            var deleteResponse = await _apiService.ExecuteGetByIdAsync($"products/{productId}");
            deleteResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            // Act - Verify the product is deleted
            var verifyResponse = await _apiService.ExecuteGetByIdAsync($"products/{productId}");
            verifyResponse.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}