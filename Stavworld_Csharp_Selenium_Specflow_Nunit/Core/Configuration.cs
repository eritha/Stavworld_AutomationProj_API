using System;
using System.Configuration;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Core
{
    /// <summary>
    /// Centralized configuration management
    /// </summary>
    public static class Configuration
    {
        // API Configuration
        public static string BaseUrl => GetConfigValue("API_BASE_URL", "https://stavpaydemo2.stavtar.com");
        public static int TimeoutSeconds => GetConfigValueAsInt("API_TIMEOUT_SECONDS", 30);
        public static int RetryCount => GetConfigValueAsInt("API_RETRY_COUNT", 3);

        // Azure B2C Configuration
        public static string AzureB2CTenantId => GetConfigValue("AZURE_B2C_TENANT_ID", "stavworld.onmicrosoft.com");
        public static string AzureB2CPolicyName => GetConfigValue("AZURE_B2C_POLICY_NAME", "b2c_1_demo2");
        public static string AzureB2CClientId => GetConfigValue("AZURE_B2C_CLIENT_ID", "092490a5-42e8-4046-9979-3ded18dab7f7");
        public static string AzureB2CScope => GetConfigValue("AZURE_B2C_SCOPE", "offline_access openid profile");
        public static string AzureB2CRedirectUri => GetConfigValue("AZURE_B2C_REDIRECT_URI", "https://stavpaydemo2.stavtar.com");
        public static string AzureB2CRefreshToken => GetConfigValue("AZURE_B2C_REFRESH_TOKEN", "eyJhbGciOiJSUzI1NiIsImtpZCI6Ilg1ZVhrNHh5b2pORnVtMWtsMll0djhkbE5QNC1jNTdkTzZRR1RWQndhTmsiLCJ0eXAiOiJKV1QifQ.eyJleHAiOjE3NTc4NDYxNjAsIm5iZiI6MTc1Nzg0MjU2MCwidmVyIjoiMS4wIiwiaXNzIjoiaHR0cHM6Ly9zdGF2d29ybGQuYjJjbG9naW4uY29tL2UwN2M2NTZkLWExMWEtNDVlNS1hNWQzLTUzZmYzZjI1ZGRiOC92Mi4wLyIsInN1YiI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImF1ZCI6IjA5MjQ5MGE1LTQyZTgtNDA0Ni05OTc5LTNkZWQxOGRhYjdmNyIsIm5vbmNlIjoiMDkxOGQ3ZmItNjVhMC00MDk2LWE1ZjMtYzE2NDkyYzJmMDU5IiwiaWF0IjoxNzU3ODQyNTYwLCJhdXRoX3RpbWUiOjE3NTc4NDI1NTEsIm9pZCI6ImJiYjAyYzI1LTY3MzYtNDc4OC1hMzU3LWQ2NjZkMjE3YjdiZSIsImdpdmVuX25hbWUiOiJDaGluaCIsImZhbWlseV9uYW1lIjoiOCIsIm5hbWUiOiJDaGluaCA4IiwiZW1haWxzIjpbImNoaW5oOEBzdGF2dGFyLmNvbSJdLCJ0ZnAiOiJCMkNfMV9EZW1vMiJ9.sMQ9d7e_Dm5WmlbPlXI5xq5LQcMdj750fUnhEVa7nxM7I3SCaMUR6VZXpzySHweKaEBl9ks79Epqx_LNPTQt1O-OHIjFM9TXPFzwagYfWSOyqA--mITBzYQnSJLI_eZgeAjROKcVD3_lTnXnsQog0x9nf_khDUVWSD2mvRY2ZhkP_t6buJ7u-NbXNj-8CBpZXvCXvDBU67dTA99T8CeGW3OvBQjzGvb832XmCVr9JlFq3P3no1cVt2mMubjXg5jfl5BI65j_-8bT5yzAH494ws0tak4HMRShwDnkfID8lrZsRlDSXqhiVbZW0VUvtdXjMM3J0s6g730t_87X7yxu8g");
        public static int AzureB2CRefreshTokenExpiresIn => GetConfigValueAsInt("AZURE_B2C_REFRESH_TOKEN_EXPIRES_IN", 86400);

        // Test Configuration
        public static string TestEnvironment => GetConfigValue("TEST_ENVIRONMENT", "staging");
        public static bool EnableExtentReports => GetConfigValueAsBool("ENABLE_EXTENT_REPORTS", true);
        public static string ReportPath => GetConfigValue("REPORT_PATH", "TestReports");

        /// Get configuration value with default
        private static string GetConfigValue(string key, string defaultValue = "")
        {
            try
            {
                var value = ConfigurationManager.AppSettings[key];
                return !string.IsNullOrEmpty(value) ? value : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// Get configuration value as integer
        private static int GetConfigValueAsInt(string key, int defaultValue = 0)
        {
            try
            {
                var value = GetConfigValue(key);
                return int.TryParse(value, out var result) ? result : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }

        /// Get configuration value as boolean
        private static bool GetConfigValueAsBool(string key, bool defaultValue = false)
        {
            try
            {
                var value = GetConfigValue(key);
                return bool.TryParse(value, out var result) ? result : defaultValue;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
