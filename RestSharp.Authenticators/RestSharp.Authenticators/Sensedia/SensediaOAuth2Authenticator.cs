using RestSharp.Authenticators.Token;
using RestSharp.Authenticators.Util;
using System;
using System.Text;

namespace RestSharp.Authenticators
{
    public class SensediaOAuth2Authenticator : SensediaClientAuthenticator
    {
        public string EndPoint { get; set; }
        public string AuthResource { get; set; }
        public string ClientSecret { get; set; }
        private static ICache<TokenAccess> _tokenCache = new Cache<TokenAccess>();
        private string _sensediaKey => Convert.ToBase64String(Encoding.UTF8.GetBytes($"{ClientId}:{ClientSecret}"));

        public SensediaOAuth2Authenticator() { }

        public SensediaOAuth2Authenticator(string endpoint, string authResource, string clientId, string clientSecret)
            : base(clientId)
        {
            EndPoint = endpoint;
            AuthResource = authResource;
            ClientSecret = clientSecret;
        }

        public override void Authenticate(IRestClient client, IRestRequest request)
        {
            base.Authenticate(client, request);
            request.AddHeader("access_token", GetTokenSensedia());
        }

        private string GetTokenSensedia()
        {
            TokenAccess token = _tokenCache.Get(_sensediaKey);

            if (token == null)
            {
                token = GetAcessTokenSensediaInternal();
                if (token != null)
                    _tokenCache.Add(_sensediaKey, token, TimeSpan.FromSeconds(token.expires_in));
            }

            return token?.access_token;
        }

        private TokenAccess GetAcessTokenSensediaInternal()
        {
            var client = new RestClient(EndPoint);
            var request = new RestRequest(AuthResource, Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("Authorization", $"Basic {_sensediaKey}", ParameterType.HttpHeader);
            request.AddParameter("application/x-www-form-urlencoded", $"grant_type=client_credentials", ParameterType.RequestBody);

            var response = client.Execute<TokenAccess>(request);

            return response.IsSuccessful ? response.Data : null;
        }
    }
}
