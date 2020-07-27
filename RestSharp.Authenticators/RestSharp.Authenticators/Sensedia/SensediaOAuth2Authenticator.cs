using RestSharp.Authenticators.Util;
using System;
using System.Text;
using System.Threading.Tasks;

namespace RestSharp.Authenticators.Sensedia
{
    public class SensediaOAuth2Authenticator : SensediaClientAuthenticator
    {
        public string EndPoint { get; }
        public string AuthResource { get; }
        public string SecretId { get; }
        private Cache<TokenAccess> _tokenCache { get; set; }
        private string _sensediaKey => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{SecretId}"));


        public SensediaOAuth2Authenticator(string endpoint, string authResource, string clientId, string secredId)
            : base(clientId)
        {
            EndPoint = endpoint;
            AuthResource = authResource;
            SecretId = secredId;
            _tokenCache = new Cache<TokenAccess>();
        }

        public SensediaOAuth2Authenticator(string endpoint, string authResource, Guid clientId, Guid secredId)
            : this(endpoint, authResource, clientId.ToString(), secredId.ToString()) { }

        public async Task<string> GetTokenSensediaAsync()
        {
            TokenAccess token = _tokenCache.Get(_sensediaKey);

            if (token == null)
            {
                token = await GetAcessTokenSensediaInternalAsync();
                if(token != null)
                    _tokenCache.Add(_sensediaKey, token, TimeSpan.FromSeconds(token.expires_in));
            }

            return token?.access_token;
        }

        public string GetTokenSensedia()
        {
            return GetTokenSensediaAsync().GetAwaiter().GetResult();
        }

        private async Task<TokenAccess> GetAcessTokenSensediaInternalAsync()
        {
            var client = new RestClient(EndPoint);
            var request = new RestRequest(AuthResource, Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("Authorization", $"Basic {_sensediaKey}", ParameterType.HttpHeader);
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials", ParameterType.RequestBody);

            var response = await client.ExecuteAsync<TokenAccess>(request);

            return response.IsSuccessful ? response.Data : null;
        }
    }
}
