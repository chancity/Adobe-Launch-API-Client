using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AdobeJwt
{
    public class JwtProvider
    {
        private readonly JwtPayloadOptions _options;
        private readonly SecurityKey _securityKey;

        public JwtProvider(JwtPayloadOptions options, SecurityKey securityKey)
        {
            _options = options;
            _securityKey = securityKey;
        }

        public Task<string> GenerateJwtToken()
        {
            JwtHeader header = CreateJwtHeader(_securityKey);
            JwtPayload payload = CreateJwtPayload(_options);

            JwtSecurityToken token = new JwtSecurityToken(header, payload);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return Task.FromResult(handler.WriteToken(token));
        }

        private static JwtHeader CreateJwtHeader(SecurityKey securityKey)
        {
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, "RS256");

            JwtHeader header = new JwtHeader(signingCredentials);
            return header;
        }

        private static JwtPayload CreateJwtPayload(JwtPayloadOptions options)
        {
            DateTime currentTime = DateTime.UtcNow;

            JwtPayload payload = new JwtPayload(
                options.Issuer,
                options.Audience,
                null,
                null,
                currentTime.Add(options.Expires));

            AddClaims(payload, options);
            return payload;
        }

        private static void AddClaims(JwtPayload payload, JwtPayloadOptions options)
        {
            payload.Add(JwtRegisteredClaimNames.Sub, options.Subject);
            payload.Add(options.NotSure.Key, options.NotSure.Value);
        }
    }
}