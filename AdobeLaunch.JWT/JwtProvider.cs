using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace AdobeLaunch.JWT
{
    public class JwtProvider
    {
        public Task<string> GenerateJwtToken(JwtPayloadOptions options, SecurityKey securityKey)
        {
            JwtHeader header = CreateJwtHeader(securityKey);
            JwtPayload payload = CreateJwtPayload(options);

            JwtSecurityToken token = new JwtSecurityToken(header, payload);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            return Task.FromResult(handler.WriteToken(token));
        }

        private JwtHeader CreateJwtHeader(SecurityKey securityKey)
        {
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, "RS256");

            JwtHeader header = new JwtHeader(signingCredentials);
            return header;
        }

        private JwtPayload CreateJwtPayload(JwtPayloadOptions options)
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

        private void AddClaims(JwtPayload payload, JwtPayloadOptions options)
        {
            payload.Add(JwtRegisteredClaimNames.Sub, options.Subject);
            payload.Add(options.NotSure.Key, options.NotSure.Value);
        }
    }
}