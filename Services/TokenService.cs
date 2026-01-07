using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;

namespace WebApp_EMail.Services
{
    public class TokenService
    {
        private readonly string _tenantId = "c00823a3-78a4-4315-8c04-4f1ab7e37bc4";  // Use your actual tenant ID
        private readonly string _clientId = "9def53cf-4804-4ddc-955d-4023d4ea28b6";  // Use your actual client ID

        public async Task<ClaimsPrincipal> ValidateToken(string token)
        {
            var handler = new JwtSecurityTokenHandler();
            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuers = new[]
                {
                    $"https://login.microsoftonline.com/{_tenantId}/v2.0",
                    $"https://sts.windows.net/{_tenantId}/"  // Allow both formats
                },

                ValidateAudience = true,
                ValidAudiences = new[] { "api://" + _clientId },
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,

                ValidateIssuerSigningKey = true,
                IssuerSigningKeys = (await new ConfigurationManager<OpenIdConnectConfiguration>(
                    $"https://login.microsoftonline.com/{_tenantId}/v2.0/.well-known/openid-configuration",
                    new OpenIdConnectConfigurationRetriever()
                ).GetConfigurationAsync()).SigningKeys
            };

            try
            {
                // Validate the token and return the claims principal
                return handler.ValidateToken(token, validationParameters, out _);
            }
            catch (SecurityTokenException ex)
            {
                // Log the exception or handle as needed
                throw new Exception("Token validation failed.", ex);
            }
        }
    }
}
