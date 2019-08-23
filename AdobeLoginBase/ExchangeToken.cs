using Newtonsoft.Json;
using Refit;

namespace AdobeLoginBase
{
    public class ExchangeToken
    {
        [AliasAs("client_id")]
        [JsonProperty("client_id")]
        public string ClientId { get; private set; }

        [AliasAs("client_secret")]
        [JsonProperty("client_secret")]
        public string ClientSecret { get; private set; }

        [AliasAs("jwt_token")]
        [JsonProperty("jwt_token")]
        public string JwtToken { get; private set; }

        [JsonConstructor]
        private ExchangeToken()
        {
        }

        public ExchangeToken(string clientId, string clientSecret, string jwtToken)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;
            JwtToken = jwtToken;
        }
    }
}