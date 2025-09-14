using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Core;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Services
{
    /// <summary>
    /// Azure B2C Authentication Service
    /// </summary>
    public class AzureB2CAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly string _tokenEndpoint;
        private readonly string _clientId;
        private readonly string _refreshToken;

        public AzureB2CAuthService()
        {
            _httpClient = new HttpClient();
            _tokenEndpoint = "https://stavworld.b2clogin.com/stavworld.onmicrosoft.com/b2c_1_demo2/oauth2/v2.0/token";
            _clientId = "092490a5-42e8-4046-9979-3ded18dab7f7";
            _refreshToken = "eyJraWQiOiJjcGltY29yZV8wOTI1MjAxNSIsInZlciI6IjEuMCIsInppcCI6IkRlZmxhdGUiLCJzZXIiOiIxLjAifQ..RMwrkclEQ_0_FqfC.hcI733uPnQoqt0Dxzyynim8xc9kPj0WhJZshq66BJinsb127PTxc3qOkuApgHJVAqJp0j25Dx_BZZrSiiAqOxECiWygUDl6-FgIfFs1ZULgplgN28JanXR8RNfxjZwrv6wuG5J4k-O3Kmr96H92DTdRLyc9SEPyQ6uIJcjdNX-7FthFlYt5VweHohXZ20Cgh2ExZpqIval_bTEhqg58utGUnppTFE_EXsV5Rbpa90fOdq8ePtbN3_-qHCt8Dx4JyMbx6sroWg4VWvYUQHkbDKQccyOuXRPhWO1GkDFeyKfd5zGp8NB5GKrIn0eEleSis8qmt3OpVwihySBVnNfxbwjE9N2456cJdMg0c4wS5uyeRx6-83vAKrT-vExU9QbAR3XdqV2BC2yT1r9YMHVGZXuhUj19RswSxiZ_xckbTwPsopLDF8ChwnXyBF6ZG-Py0-n4rDH1U8V7br6aQuKij4DrMk3mCbcTDCZkE8VcYS48m2OnDC2F7EkkP9r-P8-0yHnjntCkOsGentHedWMFJcMv3WnL91XVsbg6qGfFQ8VKbkeJBPrpAMidYCzUeplGvJ_EuGk79uuKyi06oxTbvSxrcENYyEXEv2quM7s2PVu1MnpKu_X-_TDnOTajh1IO53W5QqVG8PpaU07-X2KDbzksVzVI.EnUqtbDhEriAnbYb-nWipA";
        }

        /// Get access token using refresh token
        public async Task<ApiResponse<AzureB2CTokenResponse>> GetAccessTokenAsync()
        {
            try
            {
                ApiLogger.LogInfo("🔐 Starting Azure B2C token refresh...");
                
                // Check if refresh token is expired before making the request
                if (IsRefreshTokenExpired())
                {
                    var errorMessage = "❌ REFRESH TOKEN EXPIRED: The refresh token has expired and needs to be renewed. Please login again to get a new refresh token.";
                    ApiLogger.LogError(new Exception(errorMessage), errorMessage);
                    return new ApiResponse<AzureB2CTokenResponse>
                    {
                        Success = false,
                        ErrorMessage = errorMessage
                    };
                }

                // Prepare form data exactly like the curl command
                var formData = new List<KeyValuePair<string, string>>
                {
                    new KeyValuePair<string, string>("client_id", _clientId),
                    new KeyValuePair<string, string>("scope", "offline_access openid profile"),
                    new KeyValuePair<string, string>("grant_type", "refresh_token"),
                    new KeyValuePair<string, string>("client_info", "1"),
                    new KeyValuePair<string, string>("x-client-SKU", "msal.js.browser"),
                    new KeyValuePair<string, string>("x-client-VER", "2.28.2"),
                    new KeyValuePair<string, string>("x-ms-lib-capability", "retry-after, h429"),
                    new KeyValuePair<string, string>("x-client-current-telemetry", "5|61,0,,,|,"),
                    new KeyValuePair<string, string>("x-client-last-telemetry", "5|0|||0,0"),
                    new KeyValuePair<string, string>("client-request-id", Guid.NewGuid().ToString()),
                    new KeyValuePair<string, string>("refresh_token", _refreshToken),
                    new KeyValuePair<string, string>("X-AnchorMailbox", "Oid%3Abbb02c25-6736-4788-a357-d666d217b7be-b2c_1_demo2%40e07c656d-a11a-45e5-a5d3-53ff3f25ddb8")
                };

                var content = new FormUrlEncodedContent(formData);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Add("Accept", "*/*");
                _httpClient.DefaultRequestHeaders.Add("Accept-Language", "en,id;q=0.9,en-US;q=0.8,vi-VN;q=0.7,vi;q=0.6");
                _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
                _httpClient.DefaultRequestHeaders.Add("Origin", "https://stavpaydemo2.stavtar.com");
                _httpClient.DefaultRequestHeaders.Add("Referer", "https://stavpaydemo2.stavtar.com/");
                _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", "empty");
                _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", "cors");
                _httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", "cross-site");
                _httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_7) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/140.0.0.0 Safari/537.36");
                _httpClient.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"140\", \"Not=A?Brand\";v=\"24\", \"Google Chrome\";v=\"140\"");
                _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                _httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"macOS\"");
                // content-type should be set on the content, not the request headers

                ApiLogger.LogInfo($"🚀 Sending token request to: {_tokenEndpoint}");

                var startTime = DateTime.UtcNow;
                var response = await _httpClient.PostAsync(_tokenEndpoint, content);
                var duration = DateTime.UtcNow - startTime;

                var responseContent = await response.Content.ReadAsStringAsync();

                ApiLogger.LogInfo($"📥 Token response received in {duration.TotalMilliseconds:F2}ms");
                ApiLogger.LogInfo($"📊 Status: {response.StatusCode}");

                var apiResponse = new ApiResponse<AzureB2CTokenResponse>
                {
                    StatusCode = (int)response.StatusCode,
                    Success = response.IsSuccessStatusCode
                };

                if (response.IsSuccessStatusCode)
                {
                    try
                    {
                        var tokenResponse = JsonConvert.DeserializeObject<AzureB2CTokenResponse>(responseContent);
                        apiResponse.Data = tokenResponse;
                        ApiLogger.LogInfo("✅ Token refresh successful");
                        ApiLogger.LogInfo($"🔑 Access Token: {tokenResponse.AccessToken?.Substring(0, Math.Min(20, tokenResponse.AccessToken?.Length ?? 0))}...");
                        ApiLogger.LogInfo($"🆔 ID Token: {tokenResponse.IdToken?.Substring(0, Math.Min(20, tokenResponse.IdToken?.Length ?? 0))}...");
                        ApiLogger.LogInfo($"⏰ Expires In: {tokenResponse.ExpiresIn} seconds");
                    }
                    catch (Exception ex)
                    {
                        ApiLogger.LogError(ex, "Failed to deserialize token response");
                        apiResponse.Success = false;
                        apiResponse.ErrorMessage = $"Deserialization error: {ex.Message}";
                    }
                }
                else
                {
                    // Parse error response to provide more specific error messages
                    string detailedErrorMessage = ParseTokenErrorResponse(responseContent, response.StatusCode);
                    ApiLogger.LogError(new Exception($"Token refresh failed: {response.StatusCode} - {responseContent}"), "Token refresh failed");
                    apiResponse.ErrorMessage = detailedErrorMessage;
                }

                return apiResponse;
            }
            catch (Exception ex)
            {
                ApiLogger.LogError(ex, "Token refresh exception");
                return new ApiResponse<AzureB2CTokenResponse>
                {
                    Success = false,
                    ErrorMessage = ex.Message
                };
            }
        }

        /// Parse token error response to provide more specific error messages
        private string ParseTokenErrorResponse(string responseContent, System.Net.HttpStatusCode statusCode)
        {
            try
            {
                // Try to parse as JSON error response
                var errorResponse = JsonConvert.DeserializeObject<dynamic>(responseContent);
                
                if (errorResponse?.error != null)
                {
                    string errorCode = errorResponse.error?.ToString();
                    string errorDescription = errorResponse.error_description?.ToString();
                    
                    switch (errorCode?.ToLower())
                    {
                        case "invalid_grant":
                            return $"❌ REFRESH TOKEN EXPIRED: {errorDescription ?? "The refresh token has expired and needs to be renewed. Please login again to get a new refresh token."}";
                        case "invalid_client":
                            return $"❌ INVALID CLIENT: {errorDescription ?? "Client authentication failed. Please check client ID and configuration."}";
                        case "invalid_scope":
                            return $"❌ INVALID SCOPE: {errorDescription ?? "The requested scope is invalid or not supported."}";
                        case "unauthorized_client":
                            return $"❌ UNAUTHORIZED CLIENT: {errorDescription ?? "Client is not authorized to use this grant type."}";
                        case "unsupported_grant_type":
                            return $"❌ UNSUPPORTED GRANT TYPE: {errorDescription ?? "The grant type is not supported."}";
                        default:
                            return $"❌ AUTHENTICATION ERROR ({errorCode}): {errorDescription ?? "Unknown authentication error occurred."}";
                    }
                }
            }
            catch (Exception ex)
            {
                ApiLogger.LogError(ex, "Failed to parse error response");
            }
            
            // Fallback to generic error message
            return $"❌ AUTHENTICATION FAILED (HTTP {statusCode}): {responseContent}";
        }

        /// Check if refresh token is expired based on JWT payload
        private bool IsRefreshTokenExpired()
        {
            try
            {
                // Parse JWT payload to check expiry
                var parts = _refreshToken.Split('.');
                if (parts.Length >= 2)
                {
                    var payload = parts[1];
                    // Add padding if needed
                    while (payload.Length % 4 != 0)
                        payload += "=";
                    
                    var payloadBytes = Convert.FromBase64String(payload);
                    var payloadJson = Encoding.UTF8.GetString(payloadBytes);
                    var payloadObj = JsonConvert.DeserializeObject<dynamic>(payloadJson);
                    
                    if (payloadObj?.exp != null)
                    {
                        var expiryTime = DateTimeOffset.FromUnixTimeSeconds((long)payloadObj.exp);
                        var currentTime = DateTimeOffset.UtcNow;
                        var timeUntilExpiry = expiryTime - currentTime;
                        
                        ApiLogger.LogInfo($"🕐 Refresh Token Expiry Check:");
                        ApiLogger.LogInfo($"   Current Time: {currentTime:yyyy-MM-dd HH:mm:ss} UTC");
                        ApiLogger.LogInfo($"   Token Expires: {expiryTime:yyyy-MM-dd HH:mm:ss} UTC");
                        ApiLogger.LogInfo($"   Time Until Expiry: {timeUntilExpiry.TotalMinutes:F1} minutes");
                        
                        return timeUntilExpiry <= TimeSpan.Zero;
                    }
                }
            }
            catch (Exception ex)
            {
                ApiLogger.LogError(ex, "Failed to check refresh token expiry");
            }
            
            return false; // Assume not expired if we can't parse
        }

        /// Dispose resources
        public void Dispose()
        {
            _httpClient?.Dispose();
        }
    }
}