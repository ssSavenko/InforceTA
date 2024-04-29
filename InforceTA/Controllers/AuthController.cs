using DB.DBModels;
using InforceTA.Helpers;
using InforceTA.Models.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;

namespace InforceTA.Controllers
{
    [ApiController]
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : APIControllerBase
    {
        private IOptions<JwtBearerTokenSettings> jwtTokenOptions;
        private IAuthService authService;
        public AuthController(IOptions<JwtBearerTokenSettings> jwtTokenOptions, IAuthService authService)
        {
            this.jwtTokenOptions = jwtTokenOptions;
            this.authService = authService;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginCredentials credentials)
        {
            var result = await authService.Authenticate(credentials);
            if (result == null)
            {
                return new BadRequestObjectResult("Invalid credentials");
            }

            return Ok(result);
        }
    }
}
