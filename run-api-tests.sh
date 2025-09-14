#!/bin/bash

echo "🚀 StavWorld API Test Runner"
echo "============================"
echo ""
echo "📋 Available Test Options:"
echo "  1. Complete API Suite (9 tests in one report)"
echo "  2. Vendor Detail API Tests (5 tests)"
echo "  3. Vendor Summary API Tests (4 tests)"
echo "  4. Run Specific Test"
echo "  5. Update Token"
echo "  6. Exit"
echo ""

read -p "Please select an option (1-6): " choice

case $choice in
    1)
        echo ""
        echo "🧪 Running Complete API Suite..."
        PROJECT_DIR="/Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API/Stavworld_Csharp_Selenium_Specflow_Nunit"
        echo "📦 Building project..."
        dotnet build "$PROJECT_DIR" --verbosity quiet
        if [ $? -ne 0 ]; then
            echo "❌ Build failed!"
            exit 1
        fi
        echo "✅ Build successful!"
        echo ""
        echo "🧪 Running Complete API test suite..."
        dotnet test "$PROJECT_DIR" --filter "VendorDetailAPITests|VendorSummaryAPITests" --verbosity normal
        echo ""
        echo "🌐 ExtentReport will be automatically opened in your web browser!"
        echo "🏁 Complete API test suite execution completed!"
        ;;
    2)
        echo ""
        echo "🔍 Running Vendor Detail API Tests..."
        PROJECT_DIR="/Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API/Stavworld_Csharp_Selenium_Specflow_Nunit"
        echo "📦 Building project..."
        dotnet build "$PROJECT_DIR" --verbosity quiet
        if [ $? -ne 0 ]; then
            echo "❌ Build failed!"
            exit 1
        fi
        echo "✅ Build successful!"
        echo ""
        echo "🧪 Running Vendor Detail API test suite..."
        dotnet test "$PROJECT_DIR" --filter "VendorDetailAPITests" --verbosity normal
        echo ""
        echo "🌐 ExtentReport will be automatically opened in your web browser!"
        echo "🏁 Vendor Detail API test suite execution completed!"
        ;;
    3)
        echo ""
        echo "📊 Running Vendor Summary API Tests..."
        PROJECT_DIR="/Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API/Stavworld_Csharp_Selenium_Specflow_Nunit"
        echo "📦 Building project..."
        dotnet build "$PROJECT_DIR" --verbosity quiet
        if [ $? -ne 0 ]; then
            echo "❌ Build failed!"
            exit 1
        fi
        echo "✅ Build successful!"
        echo ""
        echo "🧪 Running Vendor Summary API test suite..."
        dotnet test "$PROJECT_DIR" --filter "VendorSummaryAPITests" --verbosity normal
        echo ""
        echo "🌐 ExtentReport will be automatically opened in your web browser!"
        echo "🏁 Vendor Summary API test suite execution completed!"
        ;;
    4)
        echo ""
        echo "🎯 Running Specific Test..."
        PROJECT_DIR="/Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API/Stavworld_Csharp_Selenium_Specflow_Nunit"
        echo "📋 Available Specific Tests:"
        echo "  Vendor Detail API Tests:"
        echo "    1. GetVendorDetail_PositiveCase_ValidVendorId_ShouldReturnSuccess"
        echo "    2. GetVendorDetail_NegativeCase_InvalidVendorId_ShouldReturnNotFound"
        echo "    3. GetVendorDetail_EdgeCase_BoundaryValues_ShouldHandleCorrectly"
        echo "    4. GetVendorDetail_SecurityCase_UnauthorizedAccess_ShouldReturn401"
        echo "    5. GetVendorDetail_PerformanceCase_ResponseTimeValidation_ShouldMeetSLA"
        echo ""
        echo "  Vendor Summary API Tests:"
        echo "    6. GetVendorSummaryData_PositiveCase_ValidParameters_ShouldReturnSuccess"
        echo "    7. GetVendorSummaryData_EdgeCase_AdditionalValidation_ShouldProcessCorrectly"
        echo "    8. GetVendorSummaryData_NegativeCase_InvalidParameters_ShouldReturnError"
        echo "    9. GetVendorSummaryData_SecurityCase_UnauthorizedAccess_ShouldReturn401"
        echo ""
        read -p "Enter test name (or partial name): " test_name
        if [ -z "$test_name" ]; then
            echo "❌ No test name provided"
            exit 1
        fi
        echo "📦 Building project..."
        dotnet build "$PROJECT_DIR" --verbosity quiet
        if [ $? -ne 0 ]; then
            echo "❌ Build failed!"
            exit 1
        fi
        echo "✅ Build successful!"
        echo ""
        echo "🧪 Running specific test: $test_name"
        dotnet test "$PROJECT_DIR" --filter "$test_name" --verbosity normal
        echo ""
        echo "🌐 ExtentReport will be automatically opened in your web browser!"
        echo "🏁 Specific test execution completed!"
        ;;
    5)
        echo ""
        echo "🔐 Token Update"
        echo "==============="
        echo "To get a new token:"
        echo "1. Go to https://stavpaydemo2.stavtar.com"
        echo "2. Login with your credentials"
        echo "3. Open browser developer tools (F12)"
        echo "4. Go to Network tab"
        echo "5. Make an API call and find the Authorization header"
        echo "6. Copy the Bearer token value"
        echo ""
        read -p "Enter your new token: " new_token
        if [ ! -z "$new_token" ]; then
            ./update-token.sh "$new_token"
        else
            echo "❌ No token provided"
        fi
        ;;
    6)
        echo "👋 Goodbye!"
        exit 0
        ;;
    *)
        echo "❌ Invalid option. Please select 1-6."
        exit 1
        ;;
esac
