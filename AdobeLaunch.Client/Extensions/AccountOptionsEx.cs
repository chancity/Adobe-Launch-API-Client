using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AdobeLaunch.JWT;

namespace AdobeLaunch.Client.Extensions
{
    internal static class AccountOptionsEx
    {
        public static Task<JwtPayloadOptions> ToJwtPayloadOptions(this AccountOptions accountOptions)
        {
            return Task.FromResult(new JwtPayloadOptions()
            {
                Audience = $"https://ims-na1.adobelogin.com/c/{accountOptions.ClientId}",
                Issuer = accountOptions.OrganizationId,
                Subject = accountOptions.TechnicalAccountId,
                NotSure = new KeyValuePair<string, bool>("https://ims-na1.adobelogin.com/s/ent_reactor_developer_sdk",
                    true),
                Expires = accountOptions.JwtExpiresIn
            });
        }
    }
}