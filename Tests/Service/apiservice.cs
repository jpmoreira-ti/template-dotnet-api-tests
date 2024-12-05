using RestSharp;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using TestsAPI.Response;
using System.Net;
using System.Collections.Generic;
using TestsAPI.FakeStoreResponse;

namespace TestsAPI.Services
{
    public class ApiService
    {
        private readonly RestClient _client;

        public ApiService(string baseUrl)
        {
            _client = new RestClient(baseUrl);
        }

        public async Task<ApiResponse<List<ProductResponse>>> ExecuteGetAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _client.ExecuteGetAsync(request, cancellationToken);

                var apiResponse = new ApiResponse<List<ProductResponse>>
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                apiResponse.Data = JsonSerializer.Deserialize<List<ProductResponse>>(response.Content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return apiResponse;
        }

        public async Task<ApiResponse<ProductResponse>> ExecuteGetByIdAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest(endpoint, Method.Get);
            var response = await _client.ExecuteGetAsync(request, cancellationToken);

            var apiResponse = new ApiResponse<ProductResponse>
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                apiResponse.Data = JsonSerializer.Deserialize<ProductResponse>(response.Content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return apiResponse;
        }

        public async Task<ApiResponse<ProductResponse>> ExecutePostAsync(string endpoint, object body, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest(endpoint, Method.Post);
            request.AddJsonBody(body);
            var response = await _client.ExecutePostAsync(request, cancellationToken);

            var apiResponse = new ApiResponse<ProductResponse>
            {
                StatusCode = response.StatusCode
            };

            if (response.IsSuccessful && !string.IsNullOrEmpty(response.Content))
            {
                apiResponse.Data = JsonSerializer.Deserialize<ProductResponse>(response.Content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            return apiResponse;
        }

        public Task<RestResponse> ExecutePostAsync(RestRequest request, CancellationToken cancellationToken = default)
        {
            return _client.ExecutePostAsync(request, cancellationToken);
        }

        public Task<RestResponse> ExecutePutAsync(RestRequest request, CancellationToken cancellationToken = default)
        {
            return _client.ExecutePutAsync(request, cancellationToken);
        }

        public Task<RestResponse> ExecuteDeleteAsync(string endpoint, CancellationToken cancellationToken = default)
        {
            var request = new RestRequest(endpoint, Method.Delete);
            return _client.ExecuteAsync(request, cancellationToken);
        }
    }
}