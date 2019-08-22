using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace AdobeLaunch.Client
{
    public class AccountOptions
    {
        public string OrganizationId { get; }
        public string TechnicalAccountId { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }
        public SecurityKey SecurityKey { get; }
        public TimeSpan JwtExpiresIn { get; }

        public AccountOptions(string organizationId, string technicalAccountId, string clientId, string clientSecret,
            SecurityKey securityKey, TimeSpan? jwtExpiresIn = null)
        {
            OrganizationId = organizationId;
            TechnicalAccountId = technicalAccountId;
            ClientId = clientId;
            ClientSecret = clientSecret;
            SecurityKey = securityKey;
            JwtExpiresIn = jwtExpiresIn ?? TimeSpan.FromDays(1);
        }
    }
}