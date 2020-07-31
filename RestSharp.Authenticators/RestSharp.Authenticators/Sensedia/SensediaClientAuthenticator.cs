namespace RestSharp.Authenticators
{
    public class SensediaClientAuthenticator : IAuthenticator
    {
        public string ClientId { get; set; }

        public SensediaClientAuthenticator()
        { }

        public SensediaClientAuthenticator(string clientId)
        {
            ClientId = clientId;
        }

        public virtual void Authenticate(IRestClient client, IRestRequest request)
        {
            request.AddHeader("client_id", ClientId);
        }
    }
}
