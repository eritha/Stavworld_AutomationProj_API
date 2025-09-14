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
    public class VendorDetailAPITests
    {
        private VendorService _vendorService;
        private AzureB2CAuthService _authService;
        private string _accessToken;

        [OneTimeSetUp]
        public async Task OneTimeSetUp()
        {
            // Initialize ExtentReports once for all tests
            ExtentReportHelper.InitializeReport("StavWorld API Test Suite - Complete Report");

            _vendorService = new VendorService();
            _authService = new AzureB2CAuthService();

            // Use hardcoded access token for testing
            _accessToken = "eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.eyJleHAiOjE3NTc4NzUzMTksIm5iZiI6MTc1Nzg3MTcxOSwidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly9zdGF2d29ybGQuYjJjbG9naW4uY29tL2UwN2M2NTZkLWExMWEtNDVlNS1hNWQzLTUzZmYzZjI1ZGRiOC92Mi4wLyIsInN1YiI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImF1ZCI6IjA5MjQ5MGE1LTQyZTgtNDA0Ni05OTc5LTNkZWQxOGRhYjdmNyIsIm5vbmNlIjoiY2NkNGM0Y2MtMTllZi00MGZiLWE3ZGItM2E0NmFiNTYyYTMwIiwiaWF0IjoxNzU3ODcxNzE5LCJhdXRoX3RpbWUiOjE3NTc4NzA5NDgsIm9pZCI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImdpdmVuX25hbWUiOiJDaGluaCIsImZhbWlseV9uYW1lIjoiOCIsIm5hbWUiOiJDaGluaCA4IiwiZW1haWxzIjpbImNoaW5oOEBzdGF2dGFyLmNvbSJdLCJ0ZnAiOiJCMkNfMV9EZW1vMiJ9.VhbbmxXA4sU3zucov2tdXYExb-QJplK97bRpdpppPbWdxgTx4sGXTGMbvaghwcCx1SmU2hCoCC0PPxzug2ad223JZy272m50yN9_hiq4eB7XiLB6xUNy-7ht-2x0VCPXuXAkABGA7INwup36SYvXcPfwQeJlw3uhFncPKIUcDFcYIjrbkv4osXBbgxJmKRIVJg0yDdVPjJzCc_DDCcD57t0x9Uj0IVuSE5EwL-nVK-px0z7KAmuVilpXwXGYqwxzJ8YEm9J3R982ImTFTUXzIwSyqFyqPxPqPrWfM56cGoxCu-m6Pbb-fUYwKNPNRosU32jKiZ8hVK2TXINDj6vlkA";
            _vendorService.SetAuthentication(_accessToken);
            ApiLogger.LogInfo("✅ Using hardcoded access token for testing");

            // Create Vendor Detail API Test Suite
            ExtentReportHelper.CreateTestSuite("🔍 Vendor Detail API Suite (5 tests)", 
                "This suite contains 5 comprehensive test cases for Vendor Detail API functionality including positive, negative, edge, security, and performance scenarios.");
        }

        /// Create all test cases for the report with detailed authentication error
        private void CreateAllTestCasesWithAuthError(string authError)
        {
            // Reset test counters to start from 1
            ExtentReportHelper.ResetTestCounters();
            
            // Vendor Detail API Suite (5 tests)
            ExtentReportHelper.CreateTestSuite("Vendor Detail API Suite", "Comprehensive tests for vendor detail data API endpoints");
            
            // Add authentication error information to the report
            ExtentReportHelper.LogStep("🔐 Authentication Status", $"❌ FAILED - {authError}");
            ExtentReportHelper.LogStep("📋 Test Execution", "All tests skipped due to authentication failure");
            ExtentReportHelper.LogStep("🛠️ Resolution", "Please check refresh token expiry and login again to get a new token");
            
            ExtentReportHelper.CreateTest("GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess", "Happy Path - Test with valid vendor ID (vendor ID = 16)");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor detail API with valid vendor ID (16)");
            ExtentReportHelper.LogStep("Expected Result", "Should return successful response with vendor detail data");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound", "Negative case - Test with invalid vendor ID");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor detail API with invalid vendor ID");
            ExtentReportHelper.LogStep("Expected Result", "Should return 404 Not Found response");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly", "Edge case - Test boundary values");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor detail API with boundary values (min/max vendor IDs)");
            ExtentReportHelper.LogStep("Expected Result", "Should handle boundary values correctly");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401", "Security case - Test unauthorized access");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor detail API without proper authentication");
            ExtentReportHelper.LogStep("Expected Result", "Should return 401 Unauthorized response");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CreateTest("GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA", "Performance case - Test response time validation");
            ExtentReportHelper.LogStep("Test Setup", "Call vendor detail API and measure response time");
            ExtentReportHelper.LogStep("Expected Result", "Should meet SLA requirements for response time");
            ExtentReportHelper.LogStep("❌ Authentication Error", authError);
            ExtentReportHelper.EndTest(false, "Test skipped due to authentication failure");
            
            ExtentReportHelper.CloseCurrentSuite();
        }


        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Close the Vendor Detail API Test Suite
            ExtentReportHelper.CloseCurrentSuite();
            
            // Flush and close the report
            ExtentReportHelper.FlushReport();
            
            // Dispose services
            _vendorService?.Dispose();
            _authService?.Dispose();
        }

        // ===== VENDOR DETAIL API TESTS =====

        [Test]
        [Description("Test vendor detail API - Positive case with valid vendor ID (Happy Path)")]
        public async Task GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess()
        {
            // Create test within the existing suite
            ExtentReportHelper.CreateTest("5. GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess (Happy Path - vendor ID = 16)", "Happy Path - Test with valid vendor ID (vendor ID = 16)");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor detail API with valid vendor ID (16)");
                ExtentReportHelper.LogApiRequest("GET", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendordetail/16/", null, "Authorization: Bearer [TOKEN]");

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorDetailAsync(16);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponse(response, 200);

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
                // Test completed
            }
        }

        [Test]
        [Description("Test vendor detail API - Negative case with invalid vendor ID")]
        public async Task GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound()
        {
            // Create test within the existing suite
            ExtentReportHelper.CreateTest("6. GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound (Negative case)", "Negative case - Test with invalid vendor ID");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor detail API with invalid vendor ID (99999)");
                ExtentReportHelper.LogApiRequest("GET", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendordetail/99999/", null, "Authorization: Bearer [TOKEN]");

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorDetailAsync(99999);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponse(response, 404);

                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 404, $"Expected 404, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success == false, $"Expected false, got {response.Success}");

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
                // Test completed
            }
        }

        [Test]
        [Description("Test vendor detail API - Edge case with boundary values")]
        public async Task GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly()
        {
            // Create test within the existing suite
            ExtentReportHelper.CreateTest("7. GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly (Edge case)", "Edge case - Test boundary values");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor detail API with boundary values (min/max vendor IDs)");
                ExtentReportHelper.LogApiRequest("GET", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendordetail/1/", null, "Authorization: Bearer [TOKEN]");

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorDetailAsync(1);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponse(response, 200);

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
                // Test completed
            }
        }

        [Test]
        [Description("Test vendor detail API - Security case with unauthorized access")]
        public async Task GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401()
        {
            // Create test within the existing suite
            ExtentReportHelper.CreateTest("8. GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401 (Security case)", "Security case - Test unauthorized access");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor detail API without proper authentication");
                ExtentReportHelper.LogApiRequest("GET", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendordetail/16/", null, "Authorization: Bearer [INVALID_TOKEN]");

                // Create a new service without authentication
                var unauthorizedService = new VendorService();
                
                var startTime = DateTime.UtcNow;
                var response = await unauthorizedService.GetVendorDetailAsync(16);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponse(response, 401);

                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 401, $"Expected 401, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success == false, $"Expected false, got {response.Success}");

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
                // Test completed
            }
        }

        [Test]
        [Description("Test vendor detail API - Performance case with response time validation")]
        public async Task GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA()
        {
            // Create test within the existing suite
            ExtentReportHelper.CreateTest("9. GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA (Performance case)", "Performance case - Test response time validation");

            try
            {
                ExtentReportHelper.LogStep("Test Setup", "Calling vendor detail API and measure response time");
                ExtentReportHelper.LogApiRequest("GET", "https://stavpaydemo2.stavtar.com/dotnetcore/api/v1/vendordetail/16/", null, "Authorization: Bearer [TOKEN]");

                var startTime = DateTime.UtcNow;
                var response = await _vendorService.GetVendorDetailAsync(16);
                var duration = DateTime.UtcNow - startTime;

                ExtentReportHelper.LogApiResponse(response, duration);

                // Validate response
                JsonResponseHelper.ValidateApiResponse(response, 200);

                ExtentReportHelper.LogAssertion("Response Status Code", response.StatusCode == 200, $"Expected 200, got {response.StatusCode}");
                ExtentReportHelper.LogAssertion("Response Success", response.Success, $"Expected true, got {response.Success}");
                ExtentReportHelper.LogAssertion("Response Data Not Null", response.Data != null, "Response data should not be null");
                ExtentReportHelper.LogAssertion("Response Time SLA", duration.TotalMilliseconds < 5000, $"Response time {duration.TotalMilliseconds}ms should be less than 5000ms");

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
                // Test completed
            }
        }
    }
}