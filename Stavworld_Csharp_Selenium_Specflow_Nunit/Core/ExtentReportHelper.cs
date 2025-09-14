using System;
using System.IO;
using System.Text;
using System.Linq;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Core
{
    /// <summary>
    /// Simple HTML Report helper for API testing reports
    /// </summary>
    public static class ExtentReportHelper
    {
        private static string _reportPath;
        private static StringBuilder _reportContent;
        private static string _currentTestName;
        private static int _testCount = 0;
        private static int _passedTests = 0;
        private static int _failedTests = 0;

        /// Reset test counters
        public static void ResetTestCounters()
        {
            _testCount = 0;
            _passedTests = 0;
            _failedTests = 0;
        }

        /// Initialize HTML Report
        public static void InitializeReport(string testName = "API Test Report")
        {
            // Use the project directory instead of current directory
            var projectDir = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            var reportDir = Path.Combine(projectDir, "Reports");
            if (!Directory.Exists(reportDir))
            {
                Directory.CreateDirectory(reportDir);
            }

            _reportPath = Path.Combine(reportDir, $"API_Test_Report_{DateTime.Now:yyyyMMdd_HHmmss}.html");
            _reportContent = new StringBuilder();
            
            // Start HTML document
            _reportContent.AppendLine("<!DOCTYPE html>");
            _reportContent.AppendLine("<html>");
            _reportContent.AppendLine("<head>");
            _reportContent.AppendLine($"<title>{testName}</title>");
            _reportContent.AppendLine("<style>");
            _reportContent.AppendLine("body { font-family: Arial, sans-serif; margin: 20px; }");
            _reportContent.AppendLine(".header { background-color: #f0f0f0; padding: 20px; border-radius: 5px; }");
            _reportContent.AppendLine(".test { margin: 20px 0; padding: 15px; border: 1px solid #ddd; border-radius: 5px; }");
            _reportContent.AppendLine(".passed { background-color: #d4edda; border-color: #c3e6cb; }");
            _reportContent.AppendLine(".failed { background-color: #f8d7da; border-color: #f5c6cb; }");
            _reportContent.AppendLine(".step { margin: 10px 0; padding: 10px; background-color: #f8f9fa; border-left: 4px solid #007bff; }");
            _reportContent.AppendLine(".request { background-color: #e9ecef; padding: 10px; margin: 10px 0; border-radius: 3px; }");
            _reportContent.AppendLine(".response { background-color: #e9ecef; padding: 10px; margin: 10px 0; border-radius: 3px; }");
            _reportContent.AppendLine("pre { background-color: #f8f9fa; padding: 10px; border-radius: 3px; overflow-x: auto; }");
            _reportContent.AppendLine(".collapsible { background-color: #f1f1f1; color: #444; cursor: pointer; padding: 8px; width: 100%; border: none; text-align: left; outline: none; font-size: 14px; border-radius: 3px; margin: 5px 0; }");
            _reportContent.AppendLine(".test-header { background-color: #e3f2fd; border: 2px solid #2196f3; font-weight: bold; padding: 10px; border-radius: 5px; }");
            _reportContent.AppendLine(".test-content { padding: 15px; background-color: #f8f9fa; border: 1px solid #dee2e6; border-radius: 5px; margin-top: 10px; }");
            _reportContent.AppendLine(".test-details { margin-top: 10px; }");
            _reportContent.AppendLine(".test-status { font-size: 16px; font-weight: bold; margin-left: 10px; }");
            _reportContent.AppendLine(".test-suite { margin: 30px 0; padding: 20px; border: 2px solid #007bff; border-radius: 8px; background-color: #f8f9fa; }");
            _reportContent.AppendLine(".suite-tests { margin-top: 15px; }");
            _reportContent.AppendLine(".test-suite h1 { color: #007bff; margin-bottom: 10px; }");
            _reportContent.AppendLine(".collapsible:hover { background-color: #ddd; }");
            _reportContent.AppendLine(".collapsible:after { content: '▼'; color: #777; font-weight: bold; float: right; margin-left: 5px; }");
            _reportContent.AppendLine(".collapsible.active:after { content: '▲'; }");
            _reportContent.AppendLine(".collapsible-content { padding: 0 18px; display: none; overflow: hidden; background-color: #f9f9f9; border-radius: 3px; }");
            _reportContent.AppendLine(".json-summary { background-color: #e3f2fd; padding: 8px; border-radius: 3px; margin: 5px 0; font-size: 12px; color: #1976d2; }");
            _reportContent.AppendLine("</style>");
            _reportContent.AppendLine("<script>");
            _reportContent.AppendLine("function toggleCollapsible(element) {");
            _reportContent.AppendLine("  element.classList.toggle('active');");
            _reportContent.AppendLine("  var content = element.nextElementSibling;");
            _reportContent.AppendLine("  if (content.style.display === 'block') {");
            _reportContent.AppendLine("    content.style.display = 'none';");
            _reportContent.AppendLine("  } else {");
            _reportContent.AppendLine("    content.style.display = 'block';");
            _reportContent.AppendLine("  }");
            _reportContent.AppendLine("}");
            _reportContent.AppendLine("</script>");
            _reportContent.AppendLine("</head>");
            _reportContent.AppendLine("<body>");
            
            // Header
            _reportContent.AppendLine("<div class='header'>");
            _reportContent.AppendLine($"<h1>{testName}</h1>");
            _reportContent.AppendLine($"<p><strong>Environment:</strong> StavWorld Demo</p>");
            _reportContent.AppendLine($"<p><strong>Base URL:</strong> {Configuration.BaseUrl}</p>");
            _reportContent.AppendLine($"<p><strong>Test Framework:</strong> NUnit + Custom HTML Report</p>");
            _reportContent.AppendLine($"<p><strong>Test Date:</strong> {DateTime.Now:yyyy-MM-dd HH:mm:ss}</p>");
            _reportContent.AppendLine("</div>");
            
            // Create main test container
            _reportContent.AppendLine("<div class='test-container'>");
        }

        /// Create a test suite for better organization
        public static void CreateTestSuite(string suiteName, string description = "")
        {
            _reportContent.AppendLine($"<div class='test-suite' id='suite_{suiteName.Replace(" ", "_")}'>");
            _reportContent.AppendLine($"<h1>📋 {suiteName}</h1>");
            if (!string.IsNullOrEmpty(description))
            {
                _reportContent.AppendLine($"<button class='collapsible' onclick='toggleCollapsible(this)'>📝 Description</button>");
                _reportContent.AppendLine($"<div class='collapsible-content'><p>{description}</p></div>");
            }
            _reportContent.AppendLine("<div class='suite-tests'>");
        }

        /// Close current test suite
        public static void CloseCurrentSuite()
        {
            _reportContent.AppendLine("</div>"); // Close suite-tests
            _reportContent.AppendLine("</div>"); // Close test-suite
        }

        /// Create a new test
        public static object CreateTest(string testName, string description = "")
        {
            // Close previous test if exists
            if (!string.IsNullOrEmpty(_currentTestName))
            {
                EndTest();
            }
            
            _currentTestName = testName;
            _testCount++;
            
            _reportContent.AppendLine($"<div class='test' id='test_{_testCount}'>");
            _reportContent.AppendLine($"<div class='test-header'>");
            _reportContent.AppendLine($"<h2>Test {_testCount}: {testName} <span class='test-status' id='status_{_testCount}'>⏳ Running...</span></h2>");
            _reportContent.AppendLine($"</div>");
            
            _reportContent.AppendLine($"<div class='test-content'>");
            
            // Always add description, use test name if empty
            var testDescription = !string.IsNullOrEmpty(description) ? description : $"API test for {testName}";
            _reportContent.AppendLine($"<p><strong>Description:</strong> {testDescription}</p>");
            
            _reportContent.AppendLine("<div class='test-details'>");
            
            return new object(); // Dummy object for compatibility
        }

        /// End current test
        public static void EndTest()
        {
            if (!string.IsNullOrEmpty(_currentTestName))
            {
                _reportContent.AppendLine("</div>"); // Close test-details
                _reportContent.AppendLine("</div>"); // Close test-content
                _reportContent.AppendLine("</div>"); // Close test
                _currentTestName = null;
            }
        }

        /// End test with result
        public static void EndTest(bool passed, string message = "")
        {
            if (!string.IsNullOrEmpty(_currentTestName))
            {
                var statusId = $"status_{_testCount}";
                var statusIcon = passed ? "✅" : "❌";
                var statusText = passed ? "Passed" : "Failed";
                
                _reportContent.AppendLine($"<script>document.getElementById('{statusId}').innerHTML = '{statusIcon} {statusText}';</script>");
                
                if (!string.IsNullOrEmpty(message))
                {
                    _reportContent.AppendLine($"<p><strong>Result:</strong> {message}</p>");
                }
                
                if (passed)
                {
                    _passedTests++;
                }
                else
                {
                    _failedTests++;
                }
                
                EndTest();
            }
        }

        /// Log test step
        public static void LogStep(string step, string details = "")
        {
            _reportContent.AppendLine("<div class='step'>");
            _reportContent.AppendLine($"<h4>📝 {step}</h4>");
            if (!string.IsNullOrEmpty(details))
            {
                _reportContent.AppendLine($"<p>{details}</p>");
            }
            _reportContent.AppendLine("</div>");
        }

        /// Log API request details
        public static void LogApiRequest(string method, string url, object body = null, string headers = "")
        {
            _reportContent.AppendLine("<div class='step'>");
            _reportContent.AppendLine("<h3>📤 API Request</h3>");
            _reportContent.AppendLine("<div class='request'>");
            _reportContent.AppendLine($"<p><strong>Method:</strong> {method}</p>");
            _reportContent.AppendLine($"<p><strong>URL:</strong> {url}</p>");
            _reportContent.AppendLine($"<p><strong>Headers:</strong> {headers}</p>");
            
            if (body != null)
            {
                var jsonBody = Newtonsoft.Json.JsonConvert.SerializeObject(body, Newtonsoft.Json.Formatting.Indented);
                var bodyType = body.GetType().Name;
                var summaryText = $"<strong>Request Body Type:</strong> {bodyType} | <strong>Size:</strong> {jsonBody.Length} characters";
                
                _reportContent.AppendLine($"<div class='json-summary'>{summaryText}</div>");
                _reportContent.AppendLine($"<button class='collapsible' onclick='toggleCollapsible(this)'>📤 View Request Body</button>");
                _reportContent.AppendLine($"<div class='collapsible-content'>");
                _reportContent.AppendLine($"<pre>{jsonBody}</pre>");
                _reportContent.AppendLine($"</div>");
            }
            
            _reportContent.AppendLine("</div>");
            _reportContent.AppendLine("</div>");
        }

        /// Log API response details
        public static void LogApiResponse<T>(ApiResponse<T> response, TimeSpan? duration = null)
        {
            _reportContent.AppendLine("<div class='step'>");
            _reportContent.AppendLine("<h3>📥 API Response</h3>");
            _reportContent.AppendLine("<div class='response'>");
            
            var statusIcon = response.Success ? "✅" : "❌";
            var statusColor = response.Success ? "green" : "red";
            
            _reportContent.AppendLine($"<p><strong>Status:</strong> <span style='color: {statusColor}'>{statusIcon} {response.StatusCode}</span></p>");
            _reportContent.AppendLine($"<p><strong>Success:</strong> {response.Success}</p>");
            
            if (duration.HasValue)
            {
                _reportContent.AppendLine($"<p><strong>Duration:</strong> {duration.Value.TotalMilliseconds:F2}ms</p>");
            }
            
            if (!string.IsNullOrEmpty(response.ErrorMessage))
            {
                _reportContent.AppendLine($"<p><strong>Error:</strong> {response.ErrorMessage}</p>");
            }
            
            if (response.Data != null)
            {
                try
                {
                    var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(response.Data, Newtonsoft.Json.Formatting.Indented);
                    var dataType = response.Data.GetType().Name;
                    var dataCount = 0;
                    
                    // Try to get count for arrays or collections
                    if (response.Data is System.Collections.IEnumerable enumerable && !(response.Data is string))
                    {
                        dataCount = enumerable.Cast<object>().Count();
                    }
                    
                    var summaryText = $"<strong>Response Data Type:</strong> {dataType}";
                    if (dataCount > 0)
                    {
                        summaryText += $" | <strong>Count:</strong> {dataCount} items";
                    }
                    summaryText += $" | <strong>Size:</strong> {jsonData.Length} characters";
                    
                    _reportContent.AppendLine($"<div class='json-summary'>{summaryText}</div>");
                    _reportContent.AppendLine($"<button class='collapsible' onclick='toggleCollapsible(this)'>📄 View Full Response Data</button>");
                    _reportContent.AppendLine($"<div class='collapsible-content'>");
                    _reportContent.AppendLine($"<pre>{jsonData}</pre>");
                    _reportContent.AppendLine($"</div>");
                }
                catch (Exception ex)
                {
                    _reportContent.AppendLine($"<p><strong>Response Data:</strong> {response.Data}</p>");
                    _reportContent.AppendLine($"<p><strong>Serialization Error:</strong> {ex.Message}</p>");
                }
            }
            
            _reportContent.AppendLine("</div>");
            _reportContent.AppendLine("</div>");
        }

        /// Log assertion result
        public static void LogAssertion(string assertion, bool passed, string details = "")
        {
            var icon = passed ? "✅" : "❌";
            var color = passed ? "green" : "red";
            
            _reportContent.AppendLine("<div class='step'>");
            _reportContent.AppendLine($"<h4>{icon} {assertion}</h4>");
            if (!string.IsNullOrEmpty(details))
            {
                _reportContent.AppendLine($"<p style='color: {color}'>{details}</p>");
            }
            _reportContent.AppendLine("</div>");
        }

        /// Log error
        public static void LogError(string error, Exception ex = null)
        {
            _reportContent.AppendLine("<div class='step'>");
            _reportContent.AppendLine($"<h4>❌ Error: {error}</h4>");
            if (ex != null)
            {
                _reportContent.AppendLine($"<p><strong>Exception:</strong> {ex.Message}</p>");
                _reportContent.AppendLine($"<p><strong>Stack Trace:</strong></p>");
                _reportContent.AppendLine($"<pre>{ex.StackTrace}</pre>");
            }
            _reportContent.AppendLine("</div>");
            
            _failedTests++;
        }

        /// Flush and close the report
        public static void FlushReport()
        {
            if (_reportContent != null)
            {
                // Close any open test suites
                _reportContent.AppendLine("</div>"); // Close suite-tests
                _reportContent.AppendLine("</div>"); // Close test-suite
                
                // Close test container
                _reportContent.AppendLine("</div>"); // Close test-container
                
                // Add summary
                _reportContent.AppendLine("<div class='header'>");
                _reportContent.AppendLine("<h2>Test Summary</h2>");
                _reportContent.AppendLine($"<p><strong>Total Tests:</strong> {_testCount}</p>");
                _reportContent.AppendLine($"<p><strong>Passed:</strong> <span style='color: green'>{_passedTests}</span></p>");
                _reportContent.AppendLine($"<p><strong>Failed:</strong> <span style='color: red'>{_failedTests}</span></p>");
                _reportContent.AppendLine($"<p><strong>Success Rate:</strong> {(_testCount > 0 ? (_passedTests * 100.0 / _testCount): 0):F1}%</p>");
                _reportContent.AppendLine("</div>");
                
                _reportContent.AppendLine("</body>");
                _reportContent.AppendLine("</html>");
                
                // Write to file
                File.WriteAllText(_reportPath, _reportContent.ToString());
                
                // Open in browser
                try
                {
                    System.Diagnostics.Process.Start(_reportPath);
                }
                catch
                {
                    // Ignore if can't open browser
                }
                
                Console.WriteLine($"\n🌐 ExtentReport will be automatically opened in your web browser!");
                Console.WriteLine($"📊 The report includes:");
                Console.WriteLine($"  - All {_testCount} API tests in one unified report");
                Console.WriteLine($"  - Azure B2C authentication flow");
                Console.WriteLine($"  - Precondition token validation");
                Console.WriteLine($"  - Collapse sections for JSON responses");
                Console.WriteLine($"  - Interactive HTML report");
            }
        }
    }
}