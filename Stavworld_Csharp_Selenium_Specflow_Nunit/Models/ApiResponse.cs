using System;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Models
{
    /// <summary>
    /// Generic API response wrapper
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public class ApiResponse<T>
    {
        /// HTTP status code
        public int StatusCode { get; set; }

        /// Whether the request was successful
        public bool Success { get; set; }

        /// Response data
        public T Data { get; set; }

        /// Error message if any
        public string ErrorMessage { get; set; }

        /// Response headers
        public System.Collections.Generic.Dictionary<string, string> Headers { get; set; }

        /// Response duration
        public TimeSpan? Duration { get; set; }

        /// Raw response content
        public string RawContent { get; set; }

        public ApiResponse()
        {
            Headers = new System.Collections.Generic.Dictionary<string, string>();
        }
    }
}
