# StavWorld API Testing Framework

## 📋 Overview
A comprehensive API testing framework for StavWorld APIs using NUnit, ExtentReports, and Azure B2C authentication.

## 🏗️ Project Structure

### Core Components
- **ExtentReportHelper.cs** - HTML report generation with interactive features
- **AsyncApiClient.cs** - HTTP client for API calls with Bearer token support
- **JsonResponseHelper.cs** - JSON response validation utilities
- **ApiLogger.cs** - Comprehensive logging utilities
- **Configuration.cs** - Configuration settings and constants

### Services
- **VendorService.cs** - Vendor API operations (Summary & Detail)
- **AzureB2CAuthService.cs** - Azure B2C authentication with refresh token

### Models
- **AuthModels.cs** - Authentication models (AzureB2CTokenResponse, etc.)
- **VendorModels.cs** - Vendor data models (Summary & Detail requests/responses)
- **ApiResponse.cs** - Generic API response wrapper

### Tests
- **VendorSummaryAPITests.cs** - Vendor Summary API test suite (4 tests)
- **VendorDetailAPITests.cs** - Vendor Detail API test suite (5 tests)

## 🧪 Test Cases

### Vendor Summary API Tests (4 tests)
1. `GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess` - Happy Path
2. `GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly` - Edge case
3. `GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError` - Negative case
4. `GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401` - Security case

### Vendor Detail API Tests (5 tests)
1. `GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess` - Happy Path (vendor ID = 16)
2. `GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound` - Negative case
3. `GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly` - Edge case
4. `GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401` - Security case
5. `GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA` - Performance case

## 🚀 Running Tests

### Interactive Test Runner (Recommended)
```bash
./run-api-tests.sh
```
**Options:**
1. Complete API Suite (9 tests in one report)
2. Vendor Detail API Tests (5 tests)
3. Vendor Summary API Tests (4 tests)
4. Run Specific Test
5. Update Token
6. Exit

### Direct Test Execution
```bash
# All API tests
dotnet test --filter "VendorDetailAPITests|VendorSummaryAPITests"

# Vendor Summary only
dotnet test --filter "VendorSummaryAPITests"

# Vendor Detail only
dotnet test --filter "VendorDetailAPITests"

# Specific test
dotnet test --filter "GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess"
```

### Token Management
```bash
# Update access token
./update-token.sh YOUR_NEW_TOKEN
```

## 📊 Features

- **9 Test Cases** across 2 comprehensive test suites
- **Hardcoded access token** for reliable testing
- **Bearer token authentication** with automatic header injection
- **Interactive HTML reports** with collapsible JSON responses
- **Azure B2C integration** (ready for dynamic token refresh)
- **Real-time test execution** with detailed progress tracking
- **Comprehensive error handling** with detailed logging
- **Performance validation** and response time monitoring

## 🔧 Configuration

- **Base URL**: https://stavpaydemo2.stavtar.com
- **Authentication**: Bearer token (hardcoded for testing)
- **API Endpoints**:
  - Vendor Summary: `/dotnetcore/api/v1/vendorsummarydata`
  - Vendor Detail: `/dotnetcore/api/v1/vendordetail/{vendorId}`
- **Report Format**: Interactive HTML with ExtentReports
- **Test Framework**: NUnit 3.13.3
- **HTTP Client**: Custom AsyncApiClient with retry logic

## 📈 Report Features

- **Interactive HTML reports** with modern UI
- **Test execution summary** with pass/fail statistics
- **API request/response logging** with full details
- **Collapsible JSON responses** for better readability
- **Error handling** with detailed stack traces
- **Performance metrics** and response time analysis
- **Authentication status** and token validation
- **Automatic browser opening** for immediate review

## 🛠️ Setup & Installation

1. **Prerequisites**:
   - .NET Framework 4.8
   - Visual Studio or VS Code
   - Git

2. **Installation**:
   ```bash
   git clone <repository-url>
   cd Stavworld_Csharp_Selenium_Specflow_Nunit
   dotnet restore
   dotnet build
   ```

3. **Running Tests**:
   ```bash
   chmod +x *.sh
   ./run-api-tests.sh
   ```

## 📝 Notes

- Tests use hardcoded access tokens for reliable execution
- All API calls include proper Bearer token authentication
- Reports are generated in the `bin/Debug/net48/` directory
- Token can be updated using the provided script when needed
