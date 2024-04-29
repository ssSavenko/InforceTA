using DB;
using InforceTA.Models;
using InforceTA.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace InforceTA.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : APIControllerBase
    {
         
        private IImageService imageService;
        private ILikesService likesService;
        private IDislikesService dislikeService;

        public ImageController(IImageService imageService,
                               ILikesService likesService,
                               IDislikesService dislikeService)
        { 
            this.imageService = imageService;
            this.likesService = likesService;
            this.dislikeService = dislikeService;
        }
         
        [Authorize]
        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ImageInput value)
        { 
            var result = await imageService.AddImage(value, UsersId());
            return Ok(result);
        }

        [Authorize]
        [HttpPost("delete")]
        public async Task<IActionResult> Delete([FromBody] int imageId)
        {
            await imageService.DeleteImage(imageId, UsersId());
            return Ok(new OkObjectResult(""));
        }

        [Authorize]
        [HttpPost("like/add")]
        public async Task<IActionResult> AddLike([FromBody] int imageId)
        {
            await dislikeService.DeleteDislike(imageId, UsersId());
            await likesService.AddLike(imageId, UsersId());
            return Ok(new OkObjectResult(""));
        }

        [Authorize]
        [HttpPost("dislike/add")]
        public async Task<IActionResult> AddDislike([FromBody] int imageId)
        {
            await likesService.DeleteLike(imageId, UsersId());
            await dislikeService.AddDislike(imageId, UsersId());
            return Ok(new OkObjectResult(""));
        }

        [Authorize]
        [HttpPost("like/remove")]
        public async Task<IActionResult> RemoveLike([FromBody] int imageId)
        {
            await likesService.DeleteLike(imageId, UsersId());
            return Ok(new OkObjectResult(""));
        }

        [Authorize]
        [HttpPost("dislike/addremove")]
        public async Task<IActionResult> RemoveDislike([FromBody] int imageId)
        {
            await dislikeService.DeleteDislike(imageId, UsersId()); 
            return Ok(new OkObjectResult(""));
        }

    }
}
