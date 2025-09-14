# 🚀 StavWorld API Test Runner Guide

## 📋 Overview
This guide explains how to use the StavWorld API test runners to execute different test suites for Vendor Detail and Vendor Summary APIs.

## 🧪 Available Test Suites

### 1. **VendorDetailAPITests.cs** (5 tests)
- `GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess` - Happy Path
- `GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound` - Negative case
- `GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly` - Edge case
- `GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401` - Security case
- `GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA` - Performance case

### 2. **VendorSummaryAPITests.cs** (4 tests)
- `GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess` - Happy Path
- `GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly` - Edge case
- `GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError` - Negative case
- `GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401` - Security case

## 🎯 Test Runner Options

### Main Runner
```bash
./run-api-tests.sh
```

**Options:**
1. **Complete API Suite** (9 tests in one report)
2. **Vendor Detail API Tests** (5 tests)
3. **Vendor Summary API Tests** (4 tests)
4. **Run Specific Test** (individual test)
5. **Update Token**
6. **Exit**

### Individual Test Runners

#### Run Vendor Detail Tests Only
```bash
./run-vendor-detail-tests.sh
```

#### Run Vendor Summary Tests Only
```bash
./run-vendor-summary-tests.sh
```

#### Run Specific Test
```bash
./run-specific-test.sh
```
Enter test name (or partial name) when prompted.

#### Run Complete Suite
```bash
./test-complete-api-suite.sh
```

#### Run All API Suites
```bash
./test-all-api-suites.sh
```

## 🔐 Authentication

The tests use Azure B2C authentication with refresh tokens. If authentication fails:

1. **Update Token:**
   ```bash
   ./update-token.sh "your_new_token_here"
   ```

2. **Manual Token Update:**
   - Go to https://stavpaydemo2.stavtar.com
   - Login with your credentials
   - Open browser developer tools (F12)
   - Go to Network tab
   - Make an API call and find the Authorization header
   - Copy the Bearer token value

## 📊 ExtentReport

After test execution, an HTML report will be generated with:
- ✅ Test execution results
- 📋 Request/Response details
- 🔐 Authentication status
- ⚡ Performance metrics
- 📈 Interactive charts and graphs

**Report Location:** `Stavworld_Csharp_Selenium_Specflow_Nunit/bin/Debug/net48/Reports/`

## 🛠️ Troubleshooting

### Build Issues
```bash
cd Stavworld_Csharp_Selenium_Specflow_Nunit
dotnet build
```

### Test Execution Issues
```bash
cd Stavworld_Csharp_Selenium_Specflow_Nunit
dotnet test --verbosity detailed
```

### Authentication Issues
- Check if refresh token is expired
- Update token using `./update-token.sh`
- Verify Azure B2C configuration

## 📝 Examples

### Run All Tests
```bash
./run-api-tests.sh
# Select option 1
```

### Run Only Vendor Detail Tests
```bash
./run-vendor-detail-tests.sh
```

### Run Specific Test
```bash
./run-specific-test.sh
# Enter: GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess
```

### Update Token
```bash
./update-token.sh "eyJhbGciOiJSUzI1NiIsImtpZCI6..."
```

## 🎯 Best Practices

1. **Run Complete Suite** for full regression testing
2. **Run Individual Suites** for focused testing
3. **Run Specific Tests** for debugging
4. **Update Token** when authentication fails
5. **Check ExtentReport** for detailed results

## 📞 Support

For issues or questions:
1. Check the ExtentReport for detailed error information
2. Verify authentication token is valid
3. Ensure all dependencies are installed
4. Check network connectivity to API endpoints
