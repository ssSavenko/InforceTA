using DB.DBModels;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace InforceTA.Controllers
{
    public abstract class APIControllerBase : ControllerBase
    {
        public APIControllerBase() { }
        protected bool IsAdmin() { return ((System.Security.Claims.ClaimsIdentity)User.Identity).HasClaim(ClaimTypes.Role, "Admin"); }
        protected int UsersId() { return int.Parse(((System.Security.Claims.ClaimsIdentity)User.Identity).Claims.Where(x => x.Type == ClaimTypes.NameIdentifier).FirstOrDefault()?.Value ?? "-1"); }
    }
}
