using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace InforceTA.Models.Auth
{

    public class LoginCredentials
    {
        public string Username { get; set; }
        public string Password { get; set; } 
    }

}
