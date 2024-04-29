namespace InforceTA.Models.Auth
{
    public class JwtBearerTokenSettings
    {
        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; } 
    }
}
