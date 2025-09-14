using System;

namespace Stavworld_Csharp_Selenium_Specflow_Nunit.Models
{
    /// <summary>
    /// Azure B2C Token Response Model
    /// </summary>
    public class AzureB2CTokenResponse
    {
        public string AccessToken { get; set; }
        public string IdToken { get; set; }
        public string RefreshToken { get; set; }
        public string TokenType { get; set; }
        public int ExpiresIn { get; set; }
        public string Scope { get; set; }
        public string Error { get; set; }
        public string ErrorDescription { get; set; }
    }

    /// <summary>
    /// Azure B2C Token Request Model
    /// </summary>
    public class AzureB2CTokenRequest
    {
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string GrantType { get; set; }
        public string ClientInfo { get; set; }
        public string XClientSku { get; set; }
        public string XClientVer { get; set; }
        public string XMsLibCapability { get; set; }
        public string XClientCurrentTelemetry { get; set; }
        public string XClientLastTelemetry { get; set; }
        public string ClientRequestId { get; set; }
        public string RefreshToken { get; set; }
        public string XAnchorMailbox { get; set; }
    }
}
