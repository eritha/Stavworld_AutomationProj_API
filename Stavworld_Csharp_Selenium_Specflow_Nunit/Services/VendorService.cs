using System;
using System.Net.Http;
using System.Threading.Tasks;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Core;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Services
{
    /// <summary>
    /// Vendor API Service for testing
    /// </summary>
    public class VendorService
    {
        private readonly AsyncApiClient _apiClient;

        public VendorService()
        {
            _apiClient = new AsyncApiClient();
        }

        /// Set authentication token
        public void SetAuthentication(string token)
        {
            _apiClient.SetBearerToken(token);
        }

        /// Get vendor summary data with custom request
        public async Task<ApiResponse<VendorSummaryResponse>> GetVendorSummaryDataAsync(VendorSummaryRequest request)
        {
            return await _apiClient.PostAsync<VendorSummaryResponse>("/dotnetcore/api/v1/vendorsummarydata", request);
        }

        /// Get vendor summary data with default parameters
        public async Task<ApiResponse<VendorSummaryResponse>> GetVendorSummaryDataAsync(bool activeFlag = true, string tileFlag = "v_f_1")
        {
            var request = new VendorSummaryRequest
            {
                ActiveFlag = activeFlag,
                TileFlag = tileFlag,
                Filters = new System.Collections.Generic.List<object>()
            };

            return await GetVendorSummaryDataAsync(request);
        }

        /// Get vendor detail by vendor ID
        public async Task<ApiResponse<VendorDetailResponse>> GetVendorDetailAsync(int vendorId)
        {
            return await _apiClient.GetAsync<VendorDetailResponse>($"/dotnetcore/api/v1/vendordetail/{vendorId}/");
        }

        /// Dispose resources
        public void Dispose()
        {
            _apiClient?.Dispose();
        }
    }
}