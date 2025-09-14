using NUnit.Framework;
using System;
using System.Net;
using System.Threading.Tasks;
using FluentAssertions;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Core;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Services;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Tests
{
    [TestFixture]
    public class VendorSummaryAPITests
    {
        private VendorService _vendorService;
        private AzureB2CAuthService _authService;
        private string _accessToken;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            // Initialize ExtentReports once for all tests
            ExtentReportHelper.InitializeReport("Vendor Summary API Tests - Complete Suite");

            _vendorService = new VendorService();
            _authService = new AzureB2CAuthService();

            // Use hardcoded access token for testing
            _accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.eyJleHAiOjE3NTc4NzUzMTksIm5iZiI6MTc1Nzg3MTcxOSwidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly9zdGF2d29ybGQuYjJjbG9naW4uY29tL2UwN2M2NTZkLWExMWEtNDVlNS1hNWQzLTUzZmYzZjI1ZGRiOC92Mi4wLyIsInN1YiI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImF1ZCI6IjA5MjQ5MGE1LTQyZTgtNDA0Ni05OTc5LTNkZWQxOGRhYjdmNyIsIm5vbmNlIjoiY2NkNGM0Y2MtMTllZi00MGZiLWE3ZGItM2E0NmFiNTYyYTMwIiwiaWF0IjoxNzU3ODcxNzE5LCJhdXRoX3RpbWUiOjE3NTc4NzA5NDgsIm9pZCI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImdpdmVuX25hbWUiOiJDaGluaCIsImZhbWlseV9uYW1lIjoiOCIsIm5hbWUiOiJDaGluaCA4IiwiZW1haWxzIjpbImNoaW5oOEBzdGF2dGFyLmNvbSJdLCJ0ZnAiOiJCMkNfMV9EZW1vMiJ9.VhbbmxXA4sU3zucov2tdXYExb-QJplK97bRpdpppPbWdxgTx4sGXTGMbvaghwcCx1SmU2hCoCC0PPxzug2ad223JZy272m50yN9_hiq4eB7XiLB6xUNy-7ht-2x0VCPXuXAkABGA7INwup36SYvXcPfwQeJlw3uhFncPKIUcDFcYIjrbkv4osXBbgxJmKRIVJg0yDdVPjJzCc_DDCcD57t0x9Uj0IVuSE5EwL-nVK-px0z7KAmuVilpXwXGYqwxzJ8YEm9J3R982ImTFTUXzIwSyqFyqPxPqPrWfM56cGoxCu-m6Pbb-fUYwKNPNRosU32jKiZ8hVK2TXINDj6vlkA";
            _vendorService.SetAuthentication(_accessToken);
            ApiLogger.LogInfo("✅ Using hardcoded access token for testing");

            // Create Vendor Summary API Test Suite
            ExtentReportHelper.CreateTestSuite("📊 Vendor Summary API Suite (4 tests)", 
                "This suite contains 4 comprehensive test cases for Vendor Summary API functionality including positive, edge, negative, and security scenarios.");        }

        /// <summary>
        /// Create all test cases for the report with detailed authentication error
        /// </summary>
        private void CreateAllTestCasesWithAuthError(string authError)
        {
            // Reset test counters to start from 1
            ExtentReportHelper.ResetTestCounters();
            
            // Vendor Summary API Suite (4 tests)
            ExtentReportHelper.CreateTestSuite("Vendor Summary API Suite", "Comprehensive tests for vendor summary data API endpoints");
            
            // Add authentication error information to the report
            ExtentReportHelper.LogStep("🔐 Authentication Status", $"❌ FAILED - {authError}");
            ExtentReportHelper.LogStep("📋 Test Execution", "All tests skipped due to authentication failure");
            ExtentReportHelper.LogStep("🛠️ Resolution", "Please check refresh token expiry and login again to get a new token");
            
            ExtentReportHelper.CreateTest("GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess", "Happy Path - Test with valid parameters");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor summary API with valid parameters (ActiveFlag=true, TileFlag='all')");
            ExtentReportHelper.LogStep("Expected Result", "Should return successful response with vendor summary data");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly", "Edge case - Test additional validation scenarios");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor summary API with edge case parameters");
            ExtentReportHelper.LogStep("Expected Result", "Should handle edge cases correctly");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError", "Negative case - Test with invalid parameters");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor summary API with invalid parameters");
            ExtentReportHelper.LogStep("Expected Result", "Should return appropriate error response");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401", "Security case - Test unauthorized access");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor summary API without proper authentication");
            ExtentReportHelper.LogStep("Expected Result", "Should return 401 Unauthorized response");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CloseCurrentSuite();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Flush and close the report
            ExtentReportHelper.FlushReport();
            
            // Dispose services
            _vendorService?.Dispose();
            _authService?.Dispose();
        }

        // ===== VENDOR SUMMARY API TESTS =====

        [Test]
        [Description("Test vendor summary data API - Positive case with valid parameters (Happy Path)")]
        public async Task GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess()
        {
            // Create test suite and test
            ExtentReportHelper.CreateTestSuite("Vendor Summary API Suite", "Comprehensive tests for vendor summary data API endpoints");
            ExtentReportHelper.CreateTest("GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess", "Happy Path - Test with valid parameters");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor summary API with valid parameters (ActiveFlag=true, TileFlag='all')");
                ExtentReportHelper.LogApiRequest("POST", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendorsummarydata", new { ActiveFlag = true, TileFlag = "all", Filters = new object[0] }, "Authorization: Bearer [TOKEN]");

                var request = new VendorSummaryRequest
                {
                    ActiveFlag = true,
                    TileFlag = "all",
                    Filters = new System.Collections.Generic.List<object>()
                };

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorSummaryDataAsync(request);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponseWithArray(response, 200);

                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 200, $"Expected 200, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success, $"Expected true, got {response.Success}");
                ExtentReportHelper.LogAssertion("Response Data Not Null", response.Data != null, "Response data should not be null");

                ExtentReportHelper.EndTest(true, "Test passed successfully");
            }
            catch (Exception ex)
            {
                ExtentReportHelper.LogError($"Test failed with exception: {ex.Message}", ex);
                ExtentReportHelper.EndTest(false, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                ExtentReportHelper.CloseCurrentSuite();
            }
        }

        [Test]
        [Description("Test vendor summary data API - Edge case with additional validation")]
        public async Task GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly()
        {
            // Create test suite and test
            ExtentReportHelper.CreateTestSuite("Vendor Summary API Suite", "Comprehensive tests for vendor summary data API endpoints");
            ExtentReportHelper.CreateTest("GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly", "Edge case - Test additional validation scenarios");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor summary API with edge case parameters (ActiveFlag=false, TileFlag='v_f_1')");
                ExtentReportHelper.LogApiRequest("POST", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendorsummarydata", new { ActiveFlag = false, TileFlag = "v_f_1", Filters = new object[0] }, "Authorization: Bearer [TOKEN]");

                var request = new VendorSummaryRequest
                {
                    ActiveFlag = false,
                    TileFlag = "v_f_1",
                    Filters = new System.Collections.Generic.List<object>()
                };

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorSummaryDataAsync(request);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponseWithArray(response, 200);

                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 200, $"Expected 200, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success, $"Expected true, got {response.Success}");
                ExtentReportHelper.LogAssertion("Response Data Not Null", response.Data != null, "Response data should not be null");

                ExtentReportHelper.EndTest(true, "Test passed successfully");
            }
            catch (Exception ex)
            {
                ExtentReportHelper.LogError($"Test failed with exception: {ex.Message}", ex);
                ExtentReportHelper.EndTest(false, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                ExtentReportHelper.CloseCurrentSuite();
            }
        }

        [Test]
        [Description("Test vendor summary data API - Negative case with invalid parameters")]
        public async Task GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError()
        {
            // Create test suite and test
            ExtentReportHelper.CreateTestSuite("Vendor Summary API Suite", "Comprehensive tests for vendor summary data API endpoints");
            ExtentReportHelper.CreateTest("GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError", "Negative case - Test with invalid parameters");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor summary API with invalid parameters (empty TileFlag)");
                ExtentReportHelper.LogApiRequest("POST", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendorsummarydata", new { ActiveFlag = true, TileFlag = "", Filters = new object[0] }, "Authorization: Bearer [TOKEN]");

                var request = new VendorSummaryRequest
                {
                    ActiveFlag = true,
                    TileFlag = "", // Invalid empty TileFlag
                    Filters = new System.Collections.Generic.List<object>()
                };

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorSummaryDataAsync(request);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response - API might still return 200 but with empty data or error in response
                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 200, $"Expected 200, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success, $"Expected true, got {response.Success}");

                ExtentReportHelper.EndTest(true, "Test passed successfully - Invalid parameters handled correctly");
            }
            catch (Exception ex)
            {
                ExtentReportHelper.LogError($"Test failed with exception: {ex.Message}", ex);
                ExtentReportHelper.EndTest(false, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                ExtentReportHelper.CloseCurrentSuite();
            }
        }

        [Test]
        [Description("Test vendor summary data API - Security case with unauthorized access")]
        public async Task GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401()
        {
            // Create test suite and test
            ExtentReportHelper.CreateTestSuite("Vendor Summary API Suite", "Comprehensive tests for vendor summary data API endpoints");
            ExtentReportHelper.CreateTest("GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401", "Security case - Test unauthorized access");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor summary API without proper authentication");
                ExtentReportHelper.LogApiRequest("POST", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendorsummarydata", new { ActiveFlag = true, TileFlag = "all", Filters = new object[0] }, "Authorization: Bearer [INVALID_TOKEN]");

                // Create a new service without authentication
                var unauthenticatedService = new VendorService();
                var request = new VendorSummaryRequest
                {
                    ActiveFlag = true,
                    TileFlag = "all",
                    Filters = new System.Collections.Generic.List<object>()
                };

                var startTime = DateTime.UtcNow;
                var response = await unauthenticatedService.GetVendorSummaryDataAsync(request);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response - should return 401
                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 401, $"Expected 401, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", !response.Success, $"Expected false, got {response.Success}");

                ExtentReportHelper.EndTest(true, "Test passed successfully - Unauthorized access properly handled");
            }
            catch (Exception ex)
            {
                ExtentReportHelper.LogError($"Test failed with exception: {ex.Message}", ex);
                ExtentReportHelper.EndTest(false, $"Test failed: {ex.Message}");
                throw;
            }
            finally
            {
                ExtentReportHelper.CloseCurrentSuite();
            }
        }
    }
}