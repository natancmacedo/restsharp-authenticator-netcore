using RestSharp.Authenticators.Sensedia;
using System.Threading.Tasks;

namespace RestSharp.Authenticators.Extensions
{

    public static class Authenticators
    { 
        public static async Task<IRestRequest> AuthenticateAsync(this IRestRequest request, SensediaOAuth2Authenticator sensediaAuthenticator)
        {
            request.AddHeader("client_id", sensediaAuthenticator.ClientId);
            request.AddHeader("access_token", await sensediaAuthenticator.GetTokenSensediaAsync());
            return request;
        }

        public static IRestRequest Authenticate(this IRestRequest request, SensediaOAuth2Authenticator sensediaAuthenticator)
        {
            request.AddHeader("client_id", sensediaAuthenticator.ClientId);
            request.AddHeader("access_token", sensediaAuthenticator.GetTokenSensedia());
            return request;
        }

        public static IRestRequest Authenticate(this IRestRequest request, SensediaClientAuthenticator sensediaAuthenticator)
        {
            request.AddHeader("client_id", sensediaAuthenticator.ClientId);
            return request;
        }
    }
}
