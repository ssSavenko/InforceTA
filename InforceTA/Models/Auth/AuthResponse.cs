using DB.DBModels;

namespace InforceTA.Models.Auth
{
    public class AuthResponse
    {
        public User user { get; set; }
        public string secretKey { get; set; }

    }
}
