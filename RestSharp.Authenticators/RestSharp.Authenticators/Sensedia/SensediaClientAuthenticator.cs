namespace RestSharp.Authenticators.Sensedia
{
    public class SensediaClientAuthenticator
    {
        public string ClientId { get; }

        public SensediaClientAuthenticator(string clientId)
        {
            ClientId = clientId;
        }
    }
}
