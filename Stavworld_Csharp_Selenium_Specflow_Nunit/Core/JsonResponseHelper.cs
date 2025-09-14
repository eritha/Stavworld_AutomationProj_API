using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using FluentAssertions;
using Stavworld_Csharp_Selenium_Specflow_Nunit.Models;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Core
{
    /// <summary>
    /// Helper class for JSON response processing and validation
    /// </summary>
    public static class JsonResponseHelper
    {
        /// Parse JSON response and return JObject for flexible querying
        public static JObject ParseJsonObject(string json)
        {
            try
            {
                return JObject.Parse(json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to parse JSON: {ex.Message}", ex);
            }
        }

        /// Parse JSON response and return JArray for flexible querying
        public static JArray ParseJsonArray(string json)
        {
            try
            {
                return JArray.Parse(json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to parse JSON array: {ex.Message}", ex);
            }
        }

        /// Deserialize JSON to strongly typed object
        public static T DeserializeObject<T>(string json)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(json);
            }
            catch (JsonException ex)
            {
                throw new InvalidOperationException($"Failed to deserialize JSON to {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// Validate JSON response structure using FluentAssertions
        public static void ValidateJsonStructure(JObject json, string[] requiredFields)
        {
            json.Should().NotBeNull("JSON should not be null");
            
            foreach (var field in requiredFields)
            {
                json[field].Should().NotBeNull($"JSON should contain field '{field}'");
            }
        }

        /// Validate JSON array response structure
        public static void ValidateJsonArrayStructure(JArray jsonArray, string[] requiredFields = null)
        {
            jsonArray.Should().NotBeNull("JSON array should not be null");
            jsonArray.Should().NotBeEmpty("JSON array should not be empty");
            
            if (requiredFields != null && jsonArray.Count > 0)
            {
                var firstItem = jsonArray[0] as JObject;
                firstItem.Should().NotBeNull("First array item should be an object");
                
                foreach (var field in requiredFields)
                {
                    firstItem[field].Should().NotBeNull($"Array items should contain field '{field}'");
                }
            }
        }

        /// Extract specific field value from JSON object
        public static T GetFieldValue<T>(JObject json, string fieldPath)
        {
            var token = json.SelectToken(fieldPath);
            token.Should().NotBeNull($"Field '{fieldPath}' should exist");
            
            try
            {
                return token.Value<T>();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to extract field '{fieldPath}' as {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        /// Extract array of values from JSON array
        public static List<T> GetArrayValues<T>(JArray jsonArray, string fieldPath = null)
        {
            jsonArray.Should().NotBeNull("JSON array should not be null");
            
            var values = new List<T>();
            
            foreach (var item in jsonArray)
            {
                if (fieldPath != null)
                {
                    var token = item.SelectToken(fieldPath);
                    if (token != null)
                    {
                        values.Add(token.Value<T>());
                    }
                }
                else
                {
                    values.Add(item.Value<T>());
                }
            }
            
            return values;
        }

        /// Count items in JSON array
        public static int CountArrayItems(JArray jsonArray)
        {
            jsonArray.Should().NotBeNull("JSON array should not be null");
            return jsonArray.Count;
        }

        /// Check if JSON array contains specific value
        public static bool ArrayContainsValue<T>(JArray jsonArray, string fieldPath, T expectedValue)
        {
            jsonArray.Should().NotBeNull("JSON array should not be null");
            
            return jsonArray.Any(item =>
            {
                var token = item.SelectToken(fieldPath);
                return token != null && token.Value<T>().Equals(expectedValue);
            });
        }

        /// Pretty print JSON for debugging
        public static string PrettyPrint(string json)
        {
            try
            {
                var parsedJson = JToken.Parse(json);
                return parsedJson.ToString(Formatting.Indented);
            }
            catch
            {
                return json; // Return original if parsing fails
            }
        }
        /// Debug print JSON response with formatting
        public static void DebugPrintJson(string json, string title = "JSON Response")
        {
            Console.WriteLine($"\n{'='.ToString().PadRight(80, '=')}");
            Console.WriteLine($"🔍 {title}");
            Console.WriteLine($"{'='.ToString().PadRight(80, '=')}");
            Console.WriteLine(PrettyPrint(json));
            Console.WriteLine($"{'='.ToString().PadRight(80, '=')}\n");
        }

        /// Debug print API response with details
        public static void DebugPrintApiResponse<T>(ApiResponse<T> response, string title = "API Response")
        {
            Console.WriteLine($"\n{'='.ToString().PadRight(80, '=')}");
            Console.WriteLine($"🔍 {title}");
            Console.WriteLine($"{'='.ToString().PadRight(80, '=')}");
            Console.WriteLine($"✅ Success: {response.Success}");
            Console.WriteLine($"📊 Status Code: {response.StatusCode}");
            Console.WriteLine($"❌ Error Message: {response.ErrorMessage ?? "None"}");
            Console.WriteLine($"📦 Data Type: {response.Data?.GetType().Name ?? "Null"}");
            
            if (response.Data != null)
            {
                try
                {
                    var json = JsonConvert.SerializeObject(response.Data, Formatting.Indented);
                    Console.WriteLine($"📄 Data Content:");
                    Console.WriteLine(json);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"📄 Data Content (Raw): {response.Data}");
                    Console.WriteLine($"⚠️  Serialization Error: {ex.Message}");
                }
            }
            Console.WriteLine($"{'='.ToString().PadRight(80, '=')}\n");
        }

        /// Validate API response structure
        public static void ValidateApiResponse<T>(ApiResponse<T> response, int expectedStatusCode = 200)
        {
            response.Should().NotBeNull("API response should not be null");
            response.Success.Should().BeTrue($"API call should be successful. Error: {response.ErrorMessage}");
            response.StatusCode.Should().Be(expectedStatusCode, $"Status code should be {expectedStatusCode}");
            response.Data.Should().NotBeNull("Response data should not be null");
        }

        /// Validate API response with JSON array
        public static void ValidateApiResponseWithArray<T>(ApiResponse<T> response, int expectedStatusCode = 200)
        {
            ValidateApiResponse(response, expectedStatusCode);
            
            if (response.Data is JArray jsonArray)
            {
                jsonArray.Count.Should().BeGreaterThan(0, "Response should contain data");
            }
            else if (response.Data is System.Collections.IEnumerable enumerable)
            {
                enumerable.Should().NotBeNull("Response should contain data");
            }
        }
    }
}
