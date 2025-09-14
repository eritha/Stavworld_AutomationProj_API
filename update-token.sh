#!/bin/bash

if [ -z "$1" ]; then
    echo "❌ Please provide a new token as argument"
    echo "Usage: ./update-token.sh YOUR_NEW_TOKEN"
    exit 1
fi

NEW_TOKEN="$1"
PROJECT_DIR="/Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API/Stavworld_Csharp_Selenium_Specflow_Nunit"

echo "🔐 Updating token in complete test suite..."

# Update token in StavWorldApiTests.cs
echo "📝 Updating StavWorldApiTests.cs..."
sed -i '' "s/_accessToken = \"eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.*\"/_accessToken = \"$NEW_TOKEN\"/g" "$PROJECT_DIR/Tests/StavWorldApiTests.cs"

echo "✅ Token updated successfully in complete test suite!"
echo "🧪 Running Vendor Summary API test to verify..."

# Run a quick test
cd /Users/eritha/Documents/WorkSP/Stavworld_AutomationProj_API
dotnet test Stavworld_Csharp_Selenium_Specflow_Nunit --filter "GetVendorSummaryData_WithDefaultParameters_ShouldReturnSuccessfulResponse" --verbosity normal
