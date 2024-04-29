using DB;
using DB.DBModels;
using InforceTA.Models.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InforceTA.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : APIControllerBase
    { 
        private PhotoGalleryContext dbContext;

        public UserController(PhotoGalleryContext dbContext)
        { 
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await dbContext.Users.AsNoTracking().ToListAsync());
        } 

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(x=>x.Id == id));
        }
          
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] User value)
        {
            dbContext.Users.Add(value);
            await dbContext.SaveChangesAsync();
            return Ok();
        } 
    }
}
