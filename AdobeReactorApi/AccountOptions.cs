using System;
using Microsoft.IdentityModel.Tokens;

namespace AdobeReactorApi
{
    public class AccountOptions
    {
        public string OrganizationId { get; }
        public string TechnicalAccountId { get; }
        public string ClientId { get; }
        public string ClientSecret { get; }

        public AccountOptions(string organizationId, string technicalAccountId, string clientId, string clientSecret)
        {
            OrganizationId = organizationId;
            TechnicalAccountId = technicalAccountId;
            ClientId = clientId;
            ClientSecret = clientSecret;
        }
    }
}