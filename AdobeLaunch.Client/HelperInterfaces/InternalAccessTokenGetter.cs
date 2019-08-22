using System.Threading.Tasks;
using AdobeLaunch.Client.Extensions;
using AdobeLaunch.Client.Internals;
using AdobeLaunch.Client.Models;
using AdobeLaunch.JWT;

namespace AdobeLaunch.Client.HelperInterfaces
{
    internal class InternalAccessTokenGetter : ITokenHandler<AccessToken>
    {
        private AccessToken _token;
        private readonly JwtProvider _jwtProvider;
        private readonly AccountOptions _accountOptions;
        private readonly LoginClient _loginClient;
        private readonly ITokenHandler<AccessToken> _externalTokenHandler;

        public InternalAccessTokenGetter(LoginClient loginClient, AccountOptions accountOptions, ITokenHandler<AccessToken> externalTokenHandler = null)
        {
            _loginClient = loginClient;
            _accountOptions = accountOptions;
            _jwtProvider = new JwtProvider();
            _externalTokenHandler = externalTokenHandler;
        }
        public Task<AccessToken> GetToken()
        {
            return GetAccessToken();
        }

        public Task<bool> SetToken(AccessToken token)
        {
            _token = token;
            return _externalTokenHandler?.SetToken(token) ?? Task.FromResult(true);
        }

        private async Task<AccessToken> GetAccessToken()
        {
            var token = await InternalGetToken().ConfigureAwait(false);

            if (token != null && !token.IsExpired)
            {
                return token;
            }

            return await Login();
        }

        private Task<AccessToken> InternalGetToken()
        {
            return _externalTokenHandler?.GetToken() ?? Task.FromResult(_token);
        }
        
        private async Task<AccessToken> Login()
        {
            var exchangeToken = await GetExchangeToken().ConfigureAwait(false);
            return await _loginClient.Client.Login(exchangeToken.ClientId, exchangeToken.ClientSecret, exchangeToken.JwtToken).ConfigureAwait(false);
        }
        private async Task<ExchangeToken> GetExchangeToken()
        {
            var jwtPayloadOptions = await _accountOptions.ToJwtPayloadOptions().ConfigureAwait(false);
            var jwtToken = await _jwtProvider.GenerateJwtToken(jwtPayloadOptions, _accountOptions.SecurityKey).ConfigureAwait(false);
            return new ExchangeToken(_accountOptions.ClientId, _accountOptions.ClientSecret, jwtToken);
        }
    }
}