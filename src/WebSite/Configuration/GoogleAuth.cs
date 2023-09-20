namespace WebSite.Configuration
{
    public class GoogleAuth
    {
        public const string SectionName = "GoogleAuth";
        public string Instance { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }

        public string CallbackPath { get; set; }
    }
}
