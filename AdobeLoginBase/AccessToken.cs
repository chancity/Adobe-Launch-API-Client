using System;
using Newtonsoft.Json;

namespace AdobeLoginBase
{
    public class AccessToken
    {
        [JsonProperty("token_type")] public string TokenType { get; private set; }
        [JsonProperty("access_token")] public string Token { get; private set; }
        [JsonProperty("expires_in")] public long ExpiresIn { get; private set; }

        [JsonProperty("received_at")] public DateTimeOffset ReceivedAt { get; private set; }

        [JsonIgnore] public bool IsExpired => DateTimeOffset.UtcNow > ReceivedAt.AddMilliseconds(ExpiresIn);

        [JsonConstructor]
        private AccessToken()
        {
            ReceivedAt = DateTimeOffset.UtcNow;
        }

        public AccessToken(string tokenType, string token, long expiresIn, DateTimeOffset receivedAt)
        {
            TokenType = tokenType;
            Token = token;
            ExpiresIn = expiresIn;
            ReceivedAt = receivedAt;
        }
    }
}