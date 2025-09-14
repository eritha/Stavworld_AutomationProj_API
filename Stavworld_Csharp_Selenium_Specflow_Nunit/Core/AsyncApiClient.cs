using System;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Core;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Core
{
    /// <summary>
    /// Enhanced HTTP API client with better async handling, timeout, and retry logic
    /// </summary>
    public class AsyncApiClient : IDisposable
    {
        private readonly HttpClient _httpClient;
        private readonly string _baseUrl;
        private readonly int _maxRetries;
        private readonly int _timeoutSeconds;
        private bool _disposed = false;

        public AsyncApiClient(string baseUrl = null, int? timeoutSeconds = null, int? maxRetries = null)
        {
            _baseUrl = baseUrl ?? Configuration.BaseUrl;
            _timeoutSeconds = timeoutSeconds ?? Configuration.TimeoutSeconds;
            _maxRetries = maxRetries ?? Configuration.RetryCount;
            
            _httpClient = new HttpClient();
            _httpClient.Timeout = TimeSpan.FromSeconds(_timeoutSeconds);
            ConfigureDefaultHeaders();
        }

        private void ConfigureDefaultHeaders()
        {
            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
            _httpClient.DefaultRequestHeaders.Add("User-Agent", "StavWorld-API-Test-Client/1.0");
        }

        /// Set Bearer token authentication
        public void SetBearerToken(string token)
        {
            _httpClient.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        }

        /// Clear authentication
        public void ClearAuthentication()
        {
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        /// Execute GET request with retry logic
        public async Task<ApiResponse<T>> GetAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithRetryAsync<T>(() => GetRequestAsync<T>(endpoint, cancellationToken));
        }

        /// Execute POST request with retry logic
        public async Task<ApiResponse<T>> PostAsync<T>(string endpoint, object body = null, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithRetryAsync<T>(() => PostRequestAsync<T>(endpoint, body, cancellationToken));
        }

        /// Execute PUT request with retry logic
        public async Task<ApiResponse<T>> PutAsync<T>(string endpoint, object body = null, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithRetryAsync<T>(() => PutRequestAsync<T>(endpoint, body, cancellationToken));
        }

        /// Execute DELETE request with retry logic
        public async Task<ApiResponse<T>> DeleteAsync<T>(string endpoint, CancellationToken cancellationToken = default)
        {
            return await ExecuteWithRetryAsync<T>(() => DeleteRequestAsync<T>(endpoint, cancellationToken));
        }

        /// Execute request with retry logic and exponential backoff
        private async Task<ApiResponse<T>> ExecuteWithRetryAsync<T>(Func<Task<ApiResponse<T>>> requestFunc)
        {
            Exception lastException = null;
            
            for (int attempt = 0; attempt <= _maxRetries; attempt++)
            {
                try
                {
                    var response = await requestFunc();
                    
                    // If successful, return immediately
                    if (response.Success)
                    {
                        return response;
                    }
                    
                    // If it's a client error (4xx), don't retry
                    if (response.StatusCode >= 400 && response.StatusCode < 500)
                    {
                        return response;
                    }
                    
                    // For server errors (5xx) or network issues, retry
                    lastException = new HttpRequestException($"HTTP {response.StatusCode}: {response.ErrorMessage}");
                }
                catch (TaskCanceledException ex) when (ex.InnerException is TimeoutException)
                {
                    lastException = new TimeoutException($"Request timed out after {_timeoutSeconds} seconds", ex);
                }
                catch (HttpRequestException ex)
                {
                    lastException = ex;
                }
                catch (Exception ex)
                {
                    lastException = ex;
                }

                // If this was the last attempt, break
                if (attempt == _maxRetries)
                {
                    break;
                }

                // Wait before retrying with exponential backoff
                var delay = TimeSpan.FromMilliseconds(1000 * Math.Pow(2, attempt));
                Console.WriteLine($"⏳ Retry attempt {attempt + 1}/{_maxRetries} in {delay.TotalMilliseconds}ms...");
                await Task.Delay(delay);
            }

            // Return error response
            return new ApiResponse<T>
            {
                Success = false,
                ErrorMessage = $"Request failed after {_maxRetries + 1} attempts. Last error: {lastException?.Message}",
                StatusCode = 0
            };
        }

        /// Execute GET request
        private async Task<ApiResponse<T>> GetRequestAsync<T>(string endpoint, CancellationToken cancellationToken)
        {
            var url = $"{_baseUrl}{endpoint}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            
            // Log request
            ApiLogger.LogRequest(HttpMethod.Get, url, request);
            
            var startTime = DateTime.UtcNow;
            var response = await _httpClient.GetAsync(url, cancellationToken);
            var duration = DateTime.UtcNow - startTime;
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Log response
            ApiLogger.LogResponse(response, responseContent, duration);
            
            return await ProcessResponseAsync<T>(response, responseContent);
        }

        /// Execute POST request
        private async Task<ApiResponse<T>> PostRequestAsync<T>(string endpoint, object body, CancellationToken cancellationToken)
        {
            var url = $"{_baseUrl}{endpoint}";
            var content = body != null ? new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json") : null;
            var request = new HttpRequestMessage(HttpMethod.Post, url) { Content = content };
            
            // Log request
            ApiLogger.LogRequest(HttpMethod.Post, url, request, body);
            
            var startTime = DateTime.UtcNow;
            var response = await _httpClient.PostAsync(url, content, cancellationToken);
            var duration = DateTime.UtcNow - startTime;
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Log response
            ApiLogger.LogResponse(response, responseContent, duration);
            
            return await ProcessResponseAsync<T>(response, responseContent);
        }

        /// Execute PUT request
        private async Task<ApiResponse<T>> PutRequestAsync<T>(string endpoint, object body, CancellationToken cancellationToken)
        {
            var url = $"{_baseUrl}{endpoint}";
            var content = body != null ? new StringContent(JsonConvert.SerializeObject(body), System.Text.Encoding.UTF8, "application/json") : null;
            var request = new HttpRequestMessage(HttpMethod.Put, url) { Content = content };
            
            // Log request
            ApiLogger.LogRequest(HttpMethod.Put, url, request, body);
            
            var startTime = DateTime.UtcNow;
            var response = await _httpClient.PutAsync(url, content, cancellationToken);
            var duration = DateTime.UtcNow - startTime;
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Log response
            ApiLogger.LogResponse(response, responseContent, duration);
            
            return await ProcessResponseAsync<T>(response, responseContent);
        }

        /// Execute DELETE request
        private async Task<ApiResponse<T>> DeleteRequestAsync<T>(string endpoint, CancellationToken cancellationToken)
        {
            var url = $"{_baseUrl}{endpoint}";
            var request = new HttpRequestMessage(HttpMethod.Delete, url);
            
            // Log request
            ApiLogger.LogRequest(HttpMethod.Delete, url, request);
            
            var startTime = DateTime.UtcNow;
            var response = await _httpClient.DeleteAsync(url, cancellationToken);
            var duration = DateTime.UtcNow - startTime;
            
            var responseContent = await response.Content.ReadAsStringAsync();
            
            // Log response
            ApiLogger.LogResponse(response, responseContent, duration);
            
            return await ProcessResponseAsync<T>(response, responseContent);
        }

        /// Process the response and create ApiResponse object
        private async Task<ApiResponse<T>> ProcessResponseAsync<T>(HttpResponseMessage response, string responseContent = null)
        {
            var apiResponse = new ApiResponse<T>
            {
                StatusCode = (int)response.StatusCode,
                Success = response.IsSuccessStatusCode
            };

            try
            {
                var content = responseContent ?? await response.Content.ReadAsStringAsync();
                
                if (response.IsSuccessStatusCode)
                {
                    if (typeof(T) == typeof(string))
                    {
                        apiResponse.Data = (T)(object)content;
                    }
                    else if (!string.IsNullOrEmpty(content))
                    {
                        // Use JsonResponseHelper for better error handling
                        apiResponse.Data = JsonResponseHelper.DeserializeObject<T>(content);
                    }
                }
                else
                {
                    apiResponse.ErrorMessage = content;
                }
            }
            catch (Exception ex)
            {
                apiResponse.Success = false;
                apiResponse.ErrorMessage = $"Failed to process response: {ex.Message}";
                ApiLogger.LogError(ex, "ProcessResponseAsync");
            }

            return apiResponse;
        }

        /// Create a cancellation token with custom timeout
        public static CancellationToken CreateCancellationToken(int timeoutSeconds)
        {
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            return cts.Token;
        }

        /// Execute request with custom timeout
        public async Task<ApiResponse<T>> GetWithTimeoutAsync<T>(string endpoint, int timeoutSeconds)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            return await GetAsync<T>(endpoint, cts.Token);
        }

        /// Execute POST request with custom timeout
        public async Task<ApiResponse<T>> PostWithTimeoutAsync<T>(string endpoint, object body, int timeoutSeconds)
        {
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(timeoutSeconds));
            return await PostAsync<T>(endpoint, body, cts.Token);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _httpClient?.Dispose();
                _disposed = true;
            }
        }
    }
}
