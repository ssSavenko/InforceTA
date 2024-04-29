using DB;
using InforceTA.Models.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InforceTA.Helpers
{
    public interface IAuthService
    {
        Task<AuthResponse> Authenticate(LoginCredentials loginData);
    }

    public class AuthService : IAuthService
    {
        private IOptions<JwtBearerTokenSettings> jwtTokenOptions;
        private PhotoGalleryContext dbContext;

        public AuthService(PhotoGalleryContext dbContext, IOptions<JwtBearerTokenSettings> jwtTokenOptions)
        {
            this.jwtTokenOptions = jwtTokenOptions;
            this.dbContext = dbContext;
        }

        public async Task<AuthResponse?> Authenticate(LoginCredentials loginData)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var keyBytes = Encoding.UTF8.GetBytes(jwtTokenOptions.Value.Key);
            var signingKey = new SymmetricSecurityKey(keyBytes);

            var user = dbContext.Users.FirstOrDefault(x => x.Login == loginData.Username && x.PasswordHash == loginData.Password);
            AuthResponse result = null!;

            if (user != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                if (user.isAdmin)
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    Issuer = jwtTokenOptions.Value.Issuer,
                    Audience = jwtTokenOptions.Value.Audience,
                    Expires = DateTime.UtcNow.AddDays(1),
                    SigningCredentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                result = new AuthResponse();
                result.user = user;
                result.secretKey = tokenHandler.WriteToken(token);
            }
            return result;
        }
    }
}
