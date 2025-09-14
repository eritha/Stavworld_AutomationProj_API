using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Core
{
    /// <summary>
    /// API Logger for detailed request/response logging
    /// </summary>
    public static class ApiLogger
    {
        private static bool _isEnabled = true;
        private static bool _logToConsole = true;
        private static bool _logToFile = true;
        private static string _logFilePath = "api-logs.txt";

        /// Enable or disable logging
        public static void SetEnabled(bool enabled)
        {
            _isEnabled = enabled;
        }

        /// Set logging options
        public static void SetLoggingOptions(bool logToConsole = true, bool logToFile = false, string logFilePath = "api-logs.txt")
        {
            _logToConsole = logToConsole;
            _logToFile = logToFile;
            _logFilePath = logFilePath;
        }

        /// Log API request details
        public static void LogRequest(HttpMethod method, string url, HttpRequestMessage request, object body = null)
        {
            if (!_isEnabled) return;

            var logBuilder = new StringBuilder();
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            logBuilder.AppendLine($"\n{'='.ToString().PadRight(100, '=')}");
            logBuilder.AppendLine($"🚀 API REQUEST - {timestamp}");
            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}");
            
            // Method and URL
            logBuilder.AppendLine($"📡 Method: {method}");
            logBuilder.AppendLine($"🌐 URL: {url}");
            
            // Headers
            logBuilder.AppendLine($"\n📋 REQUEST HEADERS:");
            logBuilder.AppendLine($"   Content-Type: {request.Content?.Headers?.ContentType?.ToString() ?? "Not set"}");
            logBuilder.AppendLine($"   Content-Length: {request.Content?.Headers?.ContentLength?.ToString() ?? "Not set"}");
            
            foreach (var header in request.Headers)
            {
                logBuilder.AppendLine($"   {header.Key}: {string.Join(", ", header.Value)}");
            }
            
            // Body
            if (body != null)
            {
                logBuilder.AppendLine($"\n📦 REQUEST BODY:");
                try
                {
                    var jsonBody = JsonConvert.SerializeObject(body, Formatting.Indented);
                    logBuilder.AppendLine(jsonBody);
                }
                catch (Exception ex)
                {
                    logBuilder.AppendLine($"   Raw: {body}");
                    logBuilder.AppendLine($"   Serialization Error: {ex.Message}");
                }
            }
            else
            {
                logBuilder.AppendLine($"\n📦 REQUEST BODY: None");
            }

            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}\n");

            var logContent = logBuilder.ToString();
            
            if (_logToConsole)
            {
                Console.WriteLine(logContent);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, logContent);
            }
        }

        /// Log API response details
        public static void LogResponse(HttpResponseMessage response, string responseContent, TimeSpan? duration = null)
        {
            if (!_isEnabled) return;

            var logBuilder = new StringBuilder();
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            logBuilder.AppendLine($"\n{'='.ToString().PadRight(100, '=')}");
            logBuilder.AppendLine($"📥 API RESPONSE - {timestamp}");
            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}");
            
            // Status
            logBuilder.AppendLine($"📊 Status Code: {response.StatusCode} ({(int)response.StatusCode})");
            logBuilder.AppendLine($"✅ Success: {response.IsSuccessStatusCode}");
            
            if (duration.HasValue)
            {
                logBuilder.AppendLine($"⏱️  Duration: {duration.Value.TotalMilliseconds:F2}ms");
            }
            
            // Headers
            logBuilder.AppendLine($"\n📋 RESPONSE HEADERS:");
            foreach (var header in response.Headers)
            {
                logBuilder.AppendLine($"   {header.Key}: {string.Join(", ", header.Value)}");
            }
            
            foreach (var header in response.Content.Headers)
            {
                logBuilder.AppendLine($"   {header.Key}: {string.Join(", ", header.Value)}");
            }
            
            // Content
            logBuilder.AppendLine($"\n📄 RESPONSE CONTENT:");
            logBuilder.AppendLine($"   Content Length: {responseContent?.Length ?? 0} characters");
            
            if (!string.IsNullOrEmpty(responseContent))
            {
                try
                {
                    // Try to format as JSON
                    var jsonObject = JsonConvert.DeserializeObject(responseContent);
                    var formattedJson = JsonConvert.SerializeObject(jsonObject, Formatting.Indented);
                    logBuilder.AppendLine($"   Formatted JSON:");
                    logBuilder.AppendLine(formattedJson);
                }
                catch
                {
                    // If not JSON, show raw content (truncated if too long)
                    var displayContent = responseContent.Length > 1000 
                        ? responseContent.Substring(0, 1000) + "... [TRUNCATED]"
                        : responseContent;
                    logBuilder.AppendLine($"   Raw Content:");
                    logBuilder.AppendLine(displayContent);
                }
            }
            else
            {
                logBuilder.AppendLine($"   Empty response");
            }

            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}\n");

            var logContent = logBuilder.ToString();
            
            if (_logToConsole)
            {
                Console.WriteLine(logContent);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, logContent);
            }
        }

        /// Log API error details
        public static void LogError(Exception exception, string context = "API Call")
        {
            if (!_isEnabled) return;

            var logBuilder = new StringBuilder();
            var timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");

            logBuilder.AppendLine($"\n{'='.ToString().PadRight(100, '=')}");
            logBuilder.AppendLine($"❌ API ERROR - {timestamp}");
            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}");
            logBuilder.AppendLine($"🔍 Context: {context}");
            logBuilder.AppendLine($"💥 Exception Type: {exception.GetType().Name}");
            logBuilder.AppendLine($"📝 Message: {exception.Message}");
            logBuilder.AppendLine($"📍 Stack Trace:");
            logBuilder.AppendLine(exception.StackTrace);
            logBuilder.AppendLine($"{'='.ToString().PadRight(100, '=')}\n");

            var logContent = logBuilder.ToString();
            
            if (_logToConsole)
            {
                Console.WriteLine(logContent);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, logContent);
            }
        }

        /// Log an info message
        public static void LogInfo(string message)
        {
            if (!_isEnabled) return;
            
            var logMessage = $"ℹ️ INFO: {message}";
            
            if (_logToConsole)
            {
                Console.WriteLine(logMessage);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {logMessage}\n");
            }
        }

        /// Log a warning message
        public static void LogWarning(string message)
        {
            if (!_isEnabled) return;
            
            var logMessage = $"⚠️ WARNING: {message}";
            
            if (_logToConsole)
            {
                Console.WriteLine(logMessage);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {logMessage}\n");
            }
        }

        /// Log API call summary
        public static void LogSummary(string method, string url, int statusCode, TimeSpan duration, bool success)
        {
            if (!_isEnabled) return;

            var statusIcon = success ? "✅" : "❌";
            var logMessage = $"{statusIcon} {method} {url} -> {statusCode} ({duration.TotalMilliseconds:F2}ms)";
            
            if (_logToConsole)
            {
                Console.WriteLine(logMessage);
            }
            
            if (_logToFile)
            {
                System.IO.File.AppendAllText(_logFilePath, $"{DateTime.Now:yyyy-MM-dd HH:mm:ss.fff} - {logMessage}\n");
            }
        }

        /// Clear log file
        public static void ClearLogFile()
        {
            if (System.IO.File.Exists(_logFilePath))
            {
                System.IO.File.WriteAllText(_logFilePath, string.Empty);
            }
        }

        /// Get log file path
        public static string GetLogFilePath()
        {
            return _logFilePath;
        }
    }
}
