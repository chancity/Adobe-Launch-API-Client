using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeJwt;

namespace AdobeReactorApi.Extensions
{
    static class AccountOptionsExtension
    {
        public static JwtPayloadOptions ToJwtPayloadOptions(this AccountOptions accountOptions, TimeSpan jwtExpiresIn)
        {
            return new JwtPayloadOptions()
            {
                Audience = $"https://ims-na1.adobelogin.com/c/{accountOptions.ClientId}",
                Issuer = accountOptions.OrganizationId,
                Subject = accountOptions.TechnicalAccountId,
                NotSure = new KeyValuePair<string, bool>("https://ims-na1.adobelogin.com/s/ent_reactor_developer_sdk",
                    true),
                Expires = jwtExpiresIn
            };
        }
    }
}
