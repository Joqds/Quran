namespace Joqds.Identity.Stores
{
    public class ClientConfig
    {
        public ClientConfig(string prefix, int accessTokenLifetime)
        {
            Prefix = prefix;
            AccessTokenLifetime = accessTokenLifetime;
        }

        public string Prefix { get; set; }
        public int AccessTokenLifetime { get; set; }
    }
}